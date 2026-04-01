using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Integration.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateUpdateStoreAllocateBoardsV1 : MaterialAssistTestBase
{
    private readonly List<string> _createdBoardEntityIds = [];
    private readonly List<string> _createdBoardTypeCodes = [];
    private readonly List<string> _createdAllocationNames = [];

    [TestMethod]
    public async Task Boards_CreateUpdateStoreAllocate()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var materialManagerClient = GetMaterialManagerClient().Material.Boards;

        // board data
        var board1 = new { id = "91", boardCode = "HPL_Anthracite_Marble_12.0_2800_2070" };
        var board2 = new { id = "92", boardCode = "P2_F204_75_38.0_4100_600" };
        var board3 = new { id = "93", boardCode = "HPL_Natural_4.0_2790_2060" };

        // Ensure board types exist first, tracking them for cleanup
        await EnsureBoardTypeExist("HPL_Anthracite_Marble_12.0", 2800, 2070, 12);
        _createdBoardTypeCodes.Add(board1.boardCode);

        await EnsureBoardTypeExist("P2_F204_75_38.0", 4100, 600, 38);
        _createdBoardTypeCodes.Add(board2.boardCode);

        await EnsureBoardTypeExist("HPL_Natural_4.0", 2790, 2060, 4);
        _createdBoardTypeCodes.Add(board3.boardCode);

        // Ensure board entities exist (idempotent), tracking IDs for cleanup
        await EnsureBoardEntityExist(board1.id, board1.boardCode, ManagementType.Single, 1);
        _createdBoardEntityIds.Add(board1.id);

        await EnsureBoardEntityExist(board2.id, board2.boardCode, ManagementType.Stack, 10);
        _createdBoardEntityIds.Add(board2.id);

        await EnsureBoardEntityExist(board3.id, board3.boardCode, ManagementType.GoodsInStock, 2);
        _createdBoardEntityIds.Add(board3.id);

        // update board type 1 - thickness
        var boardTypeUpdate = new MaterialManagerUpdateBoardType
        {
            Thickness = 13.0,
        };
        await materialManagerClient.UpdateBoardType(board1.boardCode, boardTypeUpdate);
        var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board1.boardCode);

        updatedBoardType1.ShouldNotBeNull(
            $"because board type with board code '{board1.boardCode}' should exist after update");
        updatedBoardType1.Thickness.ShouldNotBeNull();
        updatedBoardType1.Thickness.Value.ShouldBe(boardTypeUpdate.Thickness.Value, 0.0001, "because the value should be updated");

        // update board type 2 - board code, length, width, thickness
        var updatedBoard2Code = "P2_F204_75_38.0_4200_610";
        var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
        {
            BoardCode = updatedBoard2Code,
            Length = 4200.0,
            Width = 610.0,
            Thickness = 38.0
        };
        await materialManagerClient.UpdateBoardType(board2.boardCode, boardTypeUpdate2);

        
        _createdBoardTypeCodes.Remove(board2.boardCode);
        _createdBoardTypeCodes.Add(updatedBoard2Code);

        var updatedBoardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(updatedBoard2Code);

        updatedBoardType2.ShouldNotBeNull(
            $"because board type with board code '{updatedBoard2Code}' should exist after update");
        updatedBoardType2.Thickness.ShouldNotBeNull();
        updatedBoardType2.Thickness.Value.ShouldBe(38.0, 0.0001, "because the update value should be 38.0");
        updatedBoardType2.Length.ShouldNotBeNull();
        updatedBoardType2.Length.Value.ShouldBe(4200.0, 0.0001, "because the update value should be 4200.0");
        updatedBoardType2.Width.ShouldNotBeNull();
        updatedBoardType2.Width.Value.ShouldBe(610.0, 0.0001, "because the update value should be 610.0");

        // update board type 3 - material category
        var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
        {
            Thickness = 4.0,
            MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
        };
        await materialManagerClient.UpdateBoardType(board3.boardCode, boardTypeUpdate3);
        var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);

        updatedBoardType3.ShouldNotBeNull(
            $"because board type with board code '{board3.boardCode}' should exist after update");
        updatedBoardType3!.MaterialCategory.ShouldBe(BoardMaterialCategory.CompactPanels_HPL,
            $"because board type '{board3.boardCode}' was updated to material category CompactPanels_HPL");

        // create board type allocations
        var allocationName1 = "DeploymentTestAllocation1";
        var allocationName2 = "DeploymentTestAllocation2";
        var allocationName3 = "DeploymentTestAllocation3";

        await materialManagerClient.CreateBoardTypeAllocation(new BoardTypeAllocationRequest
        {
            BoardTypeCode = board1.boardCode,
            CreatedBy = nameof(Boards_CreateUpdateStoreAllocate),
            Name = allocationName1,
            Quantity = 1,
            Source = "HOMAG Connect",
            Workstation = "001"
        });
        _createdAllocationNames.Add(allocationName1);

        await materialManagerClient.CreateBoardTypeAllocation(new BoardTypeAllocationRequest
        {
            BoardTypeCode = updatedBoard2Code,
            CreatedBy = nameof(Boards_CreateUpdateStoreAllocate),
            Name = allocationName2,
            Quantity = 1,
            Source = "HOMAG Connect",
            Workstation = "001"
        });
        _createdAllocationNames.Add(allocationName2);

        await materialManagerClient.CreateBoardTypeAllocation(new BoardTypeAllocationRequest
        {
            BoardTypeCode = board3.boardCode,
            CreatedBy = nameof(Boards_CreateUpdateStoreAllocate),
            Name = allocationName3,
            Quantity = 2,
            Source = "HOMAG Connect",
            Workstation = "001"
        });
        _createdAllocationNames.Add(allocationName3);

        var allAllocationNames = (await materialManagerClient.GetBoardTypeAllocationsByAllocationNames(
            [allocationName1, allocationName2, allocationName3], 1000) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        allAllocationNames.ShouldNotBeNull(
            "because GetBoardTypeAllocationsByAllocationNames should return a collection of allocations");
        allAllocationNames.Any(a => a.Name == allocationName1).ShouldBeTrue();
        allAllocationNames.Any(a => a.Name == allocationName2).ShouldBeTrue();
        allAllocationNames.Any(a => a.Name == allocationName3).ShouldBeTrue();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var materialManagerClient = GetMaterialManagerClient().Material.Boards;

        // Delete allocations first (they reference board types)
        if (_createdAllocationNames.Count > 0)
        {
            try
            {
                await materialManagerClient.DeleteBoardTypeAllocations(_createdAllocationNames);
            }
            catch
            {
                // Ignore — allocations may not have been created if the test failed early
            }
        }

        // Delete board entities
        foreach (var id in _createdBoardEntityIds)
        {
            try
            {
                await materialAssistClient.DeleteBoardEntity(id);
            }
            catch
            {
                // Ignore — entity may not have been created if the test failed early
            }
        }

        // Delete board types
        foreach (var boardCode in _createdBoardTypeCodes)
        {
            try
            {
                await materialManagerClient.DeleteBoardType(boardCode);
            }
            catch
            {
                // Ignore — board type may not have been created or may have already been deleted
            }
        }
    }
}