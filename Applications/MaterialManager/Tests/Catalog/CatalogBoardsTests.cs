using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Catalog;

/// <summary>
/// Tests for the board types catalog endpoint.
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Boards")]
[TestCategory("MaterialManager.Catalog")]
public class CatalogBoardsTests : MaterialManagerTestBase
{
    /// <summary>
    /// Verifies that the catalog endpoint returns an empty collection when skip exceeds available data.
    /// </summary>
    [TestMethod]
    public async Task Boards_GetBoardTypesFromCatalog_LargeSkip_ReturnsEmpty()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var boardTypes = await materialManagerClient.Material.Boards.GetBoardTypesFromCatalog(10, 100000);

        boardTypes.ShouldNotBeNull();
        boardTypes.ShouldBeEmpty();
    }

    /// <summary>
    /// Verifies that board types can be retrieved from the Tadamo catalog.
    /// </summary>
    [TestMethod]
    public async Task Boards_GetBoardTypesFromCatalog_ReturnsResults()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var boardTypes = await materialManagerClient.Material.Boards.GetBoardTypesFromCatalog(50);

        boardTypes.ShouldNotBeNull();
        boardTypes.ShouldNotBeEmpty();
    }

    /// <summary>
    /// Verifies that paging works correctly for the catalog endpoint.
    /// </summary>
    [TestMethod]
    public async Task Boards_GetBoardTypesFromCatalog_WithSkip_ReturnsResults()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var firstPage = await materialManagerClient.Material.Boards.GetBoardTypesFromCatalog(10, 0);
        var secondPage = await materialManagerClient.Material.Boards.GetBoardTypesFromCatalog(10, 10);

        firstPage.ShouldNotBeNull();
        secondPage.ShouldNotBeNull();

        var firstPageList = firstPage.ToList();
        var secondPageList = secondPage.ToList();

        if (firstPageList.Count > 0 && secondPageList.Count > 0)
        {
            firstPageList.First()!.BoardCode.ShouldNotBe(secondPageList.First()!.BoardCode);
        }
    }
}