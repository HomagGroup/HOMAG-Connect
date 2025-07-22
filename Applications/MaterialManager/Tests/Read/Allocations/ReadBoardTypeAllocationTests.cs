using FluentAssertions;

using HomagConnect.MaterialManager.Client;

namespace HomagConnect.MaterialManager.Tests.Read.Allocations;

/// <summary>
/// </summary>
[TestClass]
public class ReadBoardTypeAllocationTests : MaterialManagerTestBase
{
    /// <summary>
    /// MaterialManagerClientMaterialBoards
    /// </summary>
    protected MaterialManagerClientMaterialBoards MaterialManagerClientMaterialBoards { get; private set; } = null!;

    /// <summary>
    /// GetBoardTypeAllocations_NoException
    /// </summary>
    [TestMethod]
    public async Task GetBoardTypeAllocations_NoException()
    {
        // Act
        var allocations = await MaterialManagerClientMaterialBoards.GetBoardTypeAllocations(2, 2);
        // Assert
        Assert.IsNotNull(allocations);
        //To-do check more once allocation repository is implemented
    }

    /// <summary>
    /// GetBoardTypeAllocationsByNames_ReturnsAllocations
    /// </summary>
    /// <param name="allocationName"></param>
    [TestMethod]
    [DataRow("AllocationName")]
    public async Task GetBoardTypeAllocationsByNames_ReturnsAllocations(string allocationName)
    {
        // Act
        var allocations = await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames(new List<string> { allocationName }, 1);
        // Assert
        allocations.Should().NotBeNull();
        // To-do check more once allocation repository is implemented
        //allocations.Should().NotBeEmpty();
        //allocations.Should().Contain(a => a.Name.Equals(allocationName));
    }

    /// <summary>
    /// SearchBoardTypeAllocation_ReturnsAllocations
    /// </summary>
    /// <param name="search"></param>
    [TestMethod]
    [DataRow("name")]
    public async Task SearchBoardTypeAllocation_ReturnsAllocations(string search)
    {
        // Act
        var allocations = await MaterialManagerClientMaterialBoards.SearchBoardTypeAllocations(search, 1);
        // Assert
        allocations.Should().NotBeNull();
        // To-do check more once allocation repository is implemented
        //allocations.Should().NotBeEmpty();
        //allocations.Should().Contain(a => a.Name.Equals(search));
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
}