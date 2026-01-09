using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Samples.Get.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Get.Edgebands;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Edgebands")]
[TemporaryDisabledOnServer(2026, 01, 31, "DF-Material")]
public class GetEdgebandsTests : MaterialAssistTestBase
{
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new GetEdgebandsTests();
        await classInstance.EnsureEdgebandTypeExist("ABS_White_1mm");

        await classInstance.EnsureEdgebandEntityExist("33", "ABS_White_1mm");
        await classInstance.EnsureEdgebandEntityExist("34", "ABS_White_1mm");
        await classInstance.EnsureEdgebandEntityExist("35", "ABS_White_1mm");
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandEntities()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntities(materialAssistClient);

        result.ShouldNotBeNull(
            "because GetEdgebandEntities should return a collection of edgeband entities");
        result.Count.ShouldBeGreaterThanOrEqualTo(3,
            "because at least 3 edgeband entities (33, 34, 35) should exist in the system");
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandEntityById()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntityById(materialAssistClient);

        result.ShouldNotBeNull(
            "because edgeband entity with ID '33' should exist");
        result?.Id.ShouldBe("33",
            "because we retrieved edgeband entity by ID '33'");
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandEntitiesByIds()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = (await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByIds(materialAssistClient)).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandEntitiesByIds should return a collection of edgeband entities");
        result.Length.ShouldBe(3,
            "because we requested 3 specific edgeband entity IDs: 33, 34, and 35");
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandEntitiesByEdgebandCode()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = (await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCode(materialAssistClient) ?? Array.Empty<EdgebandEntity>()).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandEntitiesByEdgebandCode should return a collection of edgeband entities");
        result.Length.ShouldBeGreaterThanOrEqualTo(3,
            "because at least 3 edgeband entities with edgeband code 'ABS_White_1mm' should exist");
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandEntitiesByEdgebandCodes()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = (await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCodes(materialAssistClient)).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandEntitiesByEdgebandCodes should return a collection of edgeband entities");
        result.Length.ShouldBeGreaterThanOrEqualTo(3,
            "because at least 3 edgeband entities with the requested edgeband codes should exist");
    }

    [TestMethod]
    public async Task EdgebandsGetStorageLocations()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = (await GetEndgebandEntitiesSamples.Edgebands_GetStorageLocations(materialAssistClient)).ToArray();

        result.ShouldNotBeNull(
            "because GetStorageLocations should return a collection of storage locations");
        result.Length.ShouldBeGreaterThanOrEqualTo(1,
            "because at least 1 storage location should be available for edgebands");
    }

    [TestMethod]
    public async Task EdgebandsGetWorkstations()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        var result = (await GetEndgebandEntitiesSamples.Edgebands_GetWorkstations(materialAssistClient)).ToArray();

        result.ShouldNotBeNull(
            "because GetWorkstations should return a collection of workstations");
        result.Length.ShouldBeGreaterThanOrEqualTo(1,
            "because at least 1 workstation should be available for edgebands");
    }
}