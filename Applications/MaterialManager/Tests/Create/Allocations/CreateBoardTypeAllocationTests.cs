using FluentAssertions;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.TestBase.Attributes;

namespace HomagConnect.MaterialManager.Tests.Create.Allocations;

/// <summary />
[TestClass]
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

        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// BoardTypeAllocationCreation_ValidRequest_CreatesAllocation
    /// </summary>
    [TemporaryDisabledOnServer(2025, 8, 10, "DF-Material | Enable this once GW goes to prod | Peter")]
    [TestMethod]
    [DataRow("TestBoardCode", "comments", "createdBy", "Test_Allocation", 1, "source", "workstation")]
    public async Task BoardTypeAllocationCreation_ValidRequest_CreatesAllocation(string boardCode, string comments, string createdBy,
        string name, int quantity, string source, string workstation)
    {
        await BoardType_CreateBoardTypeAllocation_Cleanup(name);
        var requestBoardTypeAllocation = CreateBoardTypeAllocationRequest(boardCode, comments, createdBy,
            name, quantity, source, workstation);
        var allocationResult = await MaterialManagerClientMaterialBoards.CreateBoardTypeAllocation(requestBoardTypeAllocation);
        allocationResult.Should().NotBeNull();
        //To-do Uncomment once allocation repository is implemented
        //allocationResult.BoardCode.Should().Be(boardCode);
        //allocationResult.Comments.Should().Be(comments);
        //allocationResult.CreatedBy.Should().Be(createdBy);
        //allocationResult.Name.Should().Be(name);
        //allocationResult.Quantity.Should().Be(quantity);
        //allocationResult.Source.Should().Be(source);
        //allocationResult.Workstation.Should().Be(workstation);
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
        var allocations = await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames([name], 1000);
        if (allocations != null && allocations.Any())
            await MaterialManagerClientMaterialBoards.DeleteBoardTypeAllocations(allocations.Select(a => a.Name));
        allocations = await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames([name], 1000);
        Assert.IsFalse(allocations != null && allocations.Any());
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