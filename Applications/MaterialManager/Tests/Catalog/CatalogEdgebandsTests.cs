using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Catalog;

/// <summary>
/// Tests for the edgeband types catalog endpoint.
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
[TestCategory("MaterialManager.Catalog")]
public class CatalogEdgebandsTests : MaterialManagerTestBase
{
    /// <summary>
    /// Verifies that edgeband types can be retrieved from the Tadamo catalog.
    /// </summary>
    [TestMethod]
    public async Task Edgebands_GetEdgebandTypesFromCatalog_ReturnsResults()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var edgebandTypes = await materialManagerClient.Material.Edgebands.GetEdgebandTypesFromCatalog(50);

        edgebandTypes.ShouldNotBeNull();
        edgebandTypes.ShouldNotBeEmpty();
    }

    /// <summary>
    /// Verifies that paging works correctly for the catalog endpoint.
    /// </summary>
    [TestMethod]
    public async Task Edgebands_GetEdgebandTypesFromCatalog_WithSkip_ReturnsResults()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var firstPage = await materialManagerClient.Material.Edgebands.GetEdgebandTypesFromCatalog(10, 0);
        var secondPage = await materialManagerClient.Material.Edgebands.GetEdgebandTypesFromCatalog(10, 10);

        firstPage.ShouldNotBeNull();
        secondPage.ShouldNotBeNull();

        var firstPageList = firstPage.ToList();
        var secondPageList = secondPage.ToList();

        if (firstPageList.Count > 0 && secondPageList.Count > 0)
        {
            firstPageList.First()!.ArticleNumber.ShouldNotBe(secondPageList.First()!.ArticleNumber);
        }
    }
   
}