using System.ComponentModel.DataAnnotations;

using Shouldly;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests.Create.Allocations;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
public class CreateBoardTypeAllocationTests : MaterialManagerTestBase
{
    /// <summary>
    /// MaterialManagerClientMaterialBoards
    /// </summary>
    protected MaterialManagerClientMaterialBoards MaterialManagerClientMaterialBoards { get; private set; } = null!;

    /// <summary />
    [TestMethod]
    [DataRow(null, "comments", "createdBy", "name", 1000, "source", "workstation")] //boardCode not set
    [DataRow("boardCode", "comments", null, "name", 1000, "source", "workstation")] //createdBy not set
    [DataRow("boardCode", "comments", "createdBy", null, 1000, "source", "workstation")] //name not set
    [DataRow("boardCode", "comments", "createdBy", "name", 1, "source", null)] //workstation not set
    public async Task BoardTypeAllocationCreation_RequiredPropertiesMissing_ThrowsException(string boardCode, string comments, string createdBy,
        string name, int quantity, string source, string workstation)
    {
        var requestBoardTypeAllocation = CreateBoardTypeAllocationRequest(boardCode, comments, createdBy,
            name, quantity, source, workstation);

        var act = async () => await MaterialManagerClientMaterialBoards.CreateBoardTypeAllocation(requestBoardTypeAllocation);

        await Should.ThrowAsync<ValidationException>(act,
            "because creating a board type allocation with missing required properties should throw a ValidationException");
    }

    /// <summary>
    /// BoardTypeAllocationCreation_ValidRequest_CreatesAllocation
    /// </summary>
    [TestMethod]
    [DataRow("comments", "createdBy", "Test_Allocation", 1, "source", "workstation")]
    public async Task BoardTypeAllocationCreation_ValidRequest_CreatesAllocation(string comments, string createdBy,
        string name, int quantity, string source, string workstation)
    {
        await BoardType_CreateBoardTypeAllocation_Cleanup(name);

        var materials = (await MaterialManagerClientMaterialBoards.GetBoardTypes(1).ConfigureAwait(false) ?? Array.Empty<BoardType>()).ToArray();

        materials.ShouldNotBeNull(
            "because GetBoardTypes should return a collection of board types");

        var firstMaterial = materials.FirstOrDefault();
        firstMaterial.ShouldNotBeNull(
            "because at least one board type should exist in the system");

        var boardCode = firstMaterial!.BoardCode;
        var requestBoardTypeAllocation = CreateBoardTypeAllocationRequest(boardCode, comments, createdBy,
            name, quantity, source, workstation);

        var allocationResult = await MaterialManagerClientMaterialBoards.CreateBoardTypeAllocation(requestBoardTypeAllocation);

        allocationResult.ShouldNotBeNull(
            $"because board type allocation '{name}' should be created successfully");
        allocationResult.BoardCode.ShouldBe(boardCode,
            $"because board type allocation '{name}' was created for board code '{boardCode}'");
        allocationResult.Comments.ShouldBe(comments,
            $"because board type allocation '{name}' was created with comments '{comments}'");
        allocationResult.CreatedBy.ShouldBe(createdBy,
            $"because board type allocation '{name}' was created by '{createdBy}'");
        allocationResult.Name.ShouldBe(name,
            $"because board type allocation was created with name '{name}'");
        allocationResult.Quantity.ShouldBe(quantity,
            $"because board type allocation '{name}' was created with quantity {quantity}");
        allocationResult.Source.ShouldBe(source,
            $"because board type allocation '{name}' was created with source '{source}'");
        allocationResult.Workstation.ShouldBe(workstation,
            $"because board type allocation '{name}' was created with workstation '{workstation}'");

        await BoardType_CreateBoardTypeAllocation_Cleanup(name);
    }

    /// <summary>
    /// Setup method to initialize the MaterialManagerClientMaterialBoards client.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        var materialManagerClient = GetMaterialManagerClient();
        MaterialManagerClientMaterialBoards = materialManagerClient.Material.Boards;
    }

    private async Task BoardType_CreateBoardTypeAllocation_Cleanup(string name)
    {
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames([name], 100)).ToArray();

        if (allocations.Length > 0)
        {
            await MaterialManagerClientMaterialBoards.DeleteBoardTypeAllocations(allocations.Select(a => a.Name));
            allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames([name], 100)).ToArray();
        }

        if (allocations.Length > 0)
        {
            await MaterialManagerClientMaterialBoards.DeleteBoardTypeAllocations(allocations.Select(a => a.Name));
            allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames([name], 100)).ToArray();
        }
        allocations.ShouldBeEmpty(
            $"because board type allocation '{name}' should be deleted during cleanup");
    }

    private static BoardTypeAllocationRequest CreateBoardTypeAllocationRequest(string boardCode, string comments, string createdBy,
        string name, int quantity, string source, string workstation)
    {
        var boardTypeAllocationRequest = new BoardTypeAllocationRequest();
        boardTypeAllocationRequest.BoardTypeCode = boardCode;
        boardTypeAllocationRequest.Comments = comments;
        boardTypeAllocationRequest.CreatedBy = createdBy;
        boardTypeAllocationRequest.Name = name;
        boardTypeAllocationRequest.Quantity = quantity;
        boardTypeAllocationRequest.Source = source;
        boardTypeAllocationRequest.Workstation = workstation;
        return boardTypeAllocationRequest;
    }
}