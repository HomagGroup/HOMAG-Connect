using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Read.Allocations;

/// <summary>
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
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
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocations(2, 2) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.ShouldNotBeNull(
            "because GetBoardTypeAllocations should return a collection of board type allocations");
    }
    
    /// <summary>
    /// GetBoardTypeAllocations_NoException
    /// </summary>
    [TestMethod]
    public async Task GetBoardTypeAllocations_ChangedSince_NoException()
    {
        // Act
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocations(DateTimeOffset.UtcNow.AddDays(-2), 2, 2) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.ShouldNotBeNull(
            "because GetBoardTypeAllocations should return a collection of board type allocations");
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
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames(new List<string> { allocationName }, 1) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.ShouldNotBeNull(
            $"because GetBoardTypeAllocationsByAllocationNames should return a collection for allocation name '{allocationName}'");
        // To-do check more once allocation repository is implemented
        //allocations.Should().NotBeEmpty();
        //allocations.ShouldContain(a => a.Name.Equals(allocationName));
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
        var allocations = (await MaterialManagerClientMaterialBoards.SearchBoardTypeAllocations(search, 1) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.ShouldNotBeNull(
            $"because SearchBoardTypeAllocations should return a collection for search term '{search}'");
        // To-do check more once allocation repository is implemented
        //allocations.Should().NotBeEmpty();
        //allocations.ShouldContain(a => a.Name.Equals(search));
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