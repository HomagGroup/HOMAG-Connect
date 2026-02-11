using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using Shouldly;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Tests.Integration.Boards;

[TestClass]
[IntegrationTest("MaterialAssist.Boards")]
public class CreateUpdateStoreAllocateBoards : MaterialAssistTestBase
{
    private const int _Iterations = 5;
    private const int _DefaultQuantity = 1;

    [TestMethod]
    public async Task Boards_CreateUpdateStoreAllocate()
    {
        var boardsClient = GetMaterialManagerClient().Material.Boards;
        var assistBoardsClient = GetMaterialAssistClient().Boards;

        var boardCode = await EnsureSampleBoardType(boardsClient);

        var available = 0;
        var inventory = 0;
        var allocated = 0;

        var createdBoardIds = new List<string>(_Iterations);
        var createdAllocationNames = new List<string>(_Iterations);

        var stopwatch = Stopwatch.StartNew();

        // Create boards
        for (var i = 0; i < _Iterations; i++)
        {
            var id = await CreateBoardEntity(assistBoardsClient, boardCode, _DefaultQuantity);
            createdBoardIds.Add(id);

            inventory += _DefaultQuantity;
            available += _DefaultQuantity;

            await ValidateBoardTypeQuantities(boardsClient, boardCode, inventory, available, allocated);
        }

        TestContext!.WriteLine($"Board creation took (#{_Iterations}): {stopwatch.ElapsedMilliseconds} ms, {stopwatch.ElapsedMilliseconds / _Iterations} ms/iteration");

        stopwatch.Restart();

        // Create allocations
        for (var i = 0; i < _Iterations; i++)
        {
            var allocationName = await CreateAllocation(boardCode, _DefaultQuantity, boardsClient);
            createdAllocationNames.Add(allocationName);

            allocated += _DefaultQuantity;
            available -= _DefaultQuantity;

            await ValidateBoardTypeQuantities(boardsClient, boardCode, inventory, available, allocated);
        }

        TestContext.WriteLine($"Allocation creation took (#{_Iterations}): {stopwatch.ElapsedMilliseconds} ms, {stopwatch.ElapsedMilliseconds / _Iterations} ms/iteration");

        stopwatch.Restart();

        // Delete boards
        for (var i = 0; i < _Iterations; i++)
        {
            await assistBoardsClient.DeleteBoardEntity(createdBoardIds[i]);

            inventory -= _DefaultQuantity;
            available -= _DefaultQuantity;

            await ValidateBoardTypeQuantities(boardsClient, boardCode, inventory, available, allocated);
        }

        TestContext.WriteLine($"Board deletion took (#{_Iterations}): {stopwatch.ElapsedMilliseconds} ms, {stopwatch.ElapsedMilliseconds / _Iterations} ms/iteration");

        stopwatch.Restart();

        // Delete allocations
        for (var i = 0; i < _Iterations; i++)
        {
            await boardsClient.DeleteBoardTypeAllocations([createdAllocationNames[i]]);

            available += _DefaultQuantity;
            allocated -= _DefaultQuantity;

            await ValidateBoardTypeQuantities(boardsClient, boardCode, inventory, available, allocated);
        }

        TestContext.WriteLine($"Allocation deletion took (#{_Iterations}): {stopwatch.ElapsedMilliseconds} ms, {stopwatch.ElapsedMilliseconds / _Iterations} ms/iteration");

        await boardsClient.DeleteBoardType(boardCode);

        inventory.ShouldBe(0);
    }

    private async Task<string> EnsureSampleBoardType(MaterialManagerClientMaterialBoards boardsClient, [CallerMemberName] string caller = "")
    {
        var boardTypes = await boardsClient.GetBoardTypes(1000);

        if (boardTypes != null)
        {
            foreach (var boardType in boardTypes.Where(b => b.BoardCode.StartsWith(caller)))
            {
                await boardsClient.DeleteBoardType(boardType.BoardCode);
            }
        }

        var boardCode = $"{caller}_{Guid.NewGuid().ToString().Substring(0, 6)}";

        await boardsClient.CreateBoardType(new MaterialManagerRequestBoardType
        {
            MaterialCode = boardCode,
            BoardCode = boardCode,
            Thickness = 19,
            Grain = Grain.None,
            Width = 2800,
            Length = 2070,
            Type = BoardTypeType.Offcut,
            CoatingCategory = CoatingCategory.Undefined,
            MaterialCategory = BoardMaterialCategory.Undefined
        });

        return boardCode;
    }

    private static async Task<string> CreateAllocation(string boardCode, int quantity, MaterialManagerClientMaterialBoards boardsClient, [CallerMemberName] string caller = "")
    {
        var request = new BoardTypeAllocationRequest
        {
            BoardTypeCode = boardCode,
            CreatedBy = nameof(Boards_CreateUpdateStoreAllocate),
            Name = Guid.NewGuid().ToString(),
            Quantity = quantity,
            Source = caller,
            Workstation = "001"
        };

        var allocation = await boardsClient.CreateBoardTypeAllocation(request);
        return allocation.Name;
    }

    private static async Task<string> CreateBoardEntity(MaterialAssistClientBoards assistBoardsClient, string boardCode, int quantity = _DefaultQuantity)
    {
        var request = new MaterialAssistRequestBoardEntity
        {
            Id = Guid.NewGuid().ToString(),
            BoardCode = boardCode,
            ManagementType = ManagementType.Single,
            Quantity = quantity
        };

        var response = await assistBoardsClient.CreateBoardEntity(request);
        return response.Id;
    }
   
    private static async Task ValidateBoardTypeQuantities(
        MaterialManagerClientMaterialBoards boardsClient,
        string boardCode,
        int? expectedInventory,
        int? expectedAvailable,
        int? expectedAllocated)
    {
        var boardType = await boardsClient.GetBoardTypeByBoardCode(boardCode);

        boardType.ShouldNotBeNull();
        boardType.TotalQuantityInInventory.ShouldBe(expectedInventory);
        boardType.TotalQuantityAvailable.ShouldBe(expectedAvailable);
        boardType.TotalQuantityAllocated.ShouldBe(expectedAllocated);
    }
}