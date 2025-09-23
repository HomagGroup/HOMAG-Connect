using HomagConnect.MaterialManager.Samples.Read.Edgebands;

namespace HomagConnect.MaterialManager.Tests.Read.Edgebands;

// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class ReadEdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new ReadEdgebandTypeTests();
        await classInstance.EnsureEdgebandTypeExist("ABS_Abruzzo_colore_1.00_100.0_HM", 1.0, 100.0);
        await classInstance.EnsureEdgebandTypeExist("ABS_Black_1.20_23.0_ZJ", 1.2, 23.0);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCode()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCode(materialManagerClient.Material.Edgebands, edgebandCode);
        Assert.AreEqual(edgebandCode, result.EdgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCodeIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCode);
        Assert.AreEqual(edgebandCode, result.EdgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypes(materialManagerClient.Material.Edgebands);
        Assert.IsTrue(result.Count() >= 2);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesIncludingDetails(materialManagerClient.Material.Edgebands);
        Assert.IsTrue(result.Count() >= 2);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodes(materialManagerClient.Material.Edgebands, edgebandCodes);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.Any(e => e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM"));
        Assert.IsTrue(result.Any(e => e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ"));
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCodes);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.Any(e => e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM"));
        Assert.IsTrue(result.Any(e => e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ"));
    }
    
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeInventoryHistoryAsync()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeInventoryHistoryAsync(materialManagerClient.Material.Edgebands);
    }
    
    [TestMethod]
    public async Task EdgebandsGetLicensedMachines()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetLicensedMachines(materialManagerClient.Material.Edgebands);
    }
}