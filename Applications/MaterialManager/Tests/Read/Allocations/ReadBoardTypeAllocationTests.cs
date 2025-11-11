using FluentAssertions;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

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
    [TemporaryDisabledOnServer(2025, 10, 01, "DF-Material | Enable this once GW goes to prod | Peter")]
    [TestMethod]
    public async Task GetBoardTypeAllocations_NoException()
    {
        // Act
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocations(2, 2) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.Should().NotBeNull(
            "because GetBoardTypeAllocations should return a collection of board type allocations");
    }
    
    /// <summary>
    /// GetBoardTypeAllocations_NoException
    /// </summary>
    [TemporaryDisabledOnServer(2025, 10, 01, "DF-Material | Enable this once GW goes to prod | Peter")]
    [TestMethod]
    public async Task GetBoardTypeAllocations_ChangedSince_NoException()
    {
        // Act
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocations(DateTimeOffset.UtcNow.AddDays(-2), 2, 2) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.Should().NotBeNull(
            "because GetBoardTypeAllocations should return a collection of board type allocations");
    }

    /// <summary>
    /// GetBoardTypeAllocationsByNames_ReturnsAllocations
    /// </summary>
    /// <param name="allocationName"></param>
    [TemporaryDisabledOnServer(2025, 10, 01, "DF-Material | Enable this once GW goes to prod | Peter")]
    [TestMethod]
    [DataRow("AllocationName")]
    public async Task GetBoardTypeAllocationsByNames_ReturnsAllocations(string allocationName)
    {
        // Act
        var allocations = (await MaterialManagerClientMaterialBoards.GetBoardTypeAllocationsByAllocationNames(new List<string> { allocationName }, 1) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.Should().NotBeNull(
            $"because GetBoardTypeAllocationsByAllocationNames should return a collection for allocation name '{allocationName}'");
        // To-do check more once allocation repository is implemented
        //allocations.Should().NotBeEmpty();
        //allocations.Should().Contain(a => a.Name.Equals(allocationName));
    }

    /// <summary>
    /// SearchBoardTypeAllocation_ReturnsAllocations
    /// </summary>
    /// <param name="search"></param>
    [TemporaryDisabledOnServer(2025, 10, 01, "DF-Material | Enable this once GW goes to prod | Peter")]
    [TestMethod]
    [DataRow("name")]
    public async Task SearchBoardTypeAllocation_ReturnsAllocations(string search)
    {
        // Act
        var allocations = (await MaterialManagerClientMaterialBoards.SearchBoardTypeAllocations(search, 1) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        // Assert
        allocations.Should().NotBeNull(
            $"because SearchBoardTypeAllocations should return a collection for search term '{search}'");
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