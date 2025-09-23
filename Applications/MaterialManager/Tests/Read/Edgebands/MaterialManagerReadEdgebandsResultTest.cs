using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
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
        await classInstance.EnsureEdgebandTypeExist("Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM", 1.0, 100.0);
        await classInstance.EnsureEdgebandTypeExist("Test_Data_ABS_Black_1.20_23.0_ZJ", 1.2, 23.0);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCode()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCode(materialManagerClient.Material.Edgebands, edgebandCode);
        Assert.AreEqual(edgebandCode, result.EdgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCodeIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM";
        var result1 = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCode);
        Assert.AreEqual(edgebandCode, result1.EdgebandCode);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result2 = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypes(materialManagerClient.Material.Edgebands);
        Assert.IsTrue(result2.Count() >= 2);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result3 = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesIncludingDetails(materialManagerClient.Material.Edgebands);
        Assert.IsTrue(result3.Count() >= 2);
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM", "Test_Data_ABS_Black_1.20_23.0_ZJ" };
        var result4 = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodes(materialManagerClient.Material.Edgebands, edgebandCodes);
        Assert.AreEqual(2, result4.Count());
        Assert.IsTrue(result4.Any(e => e.EdgebandCode == "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM"));
        Assert.IsTrue(result4.Any(e => e.EdgebandCode == "Test_Data_ABS_Black_1.20_23.0_ZJ"));
    }

    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM", "Test_Data_ABS_Black_1.20_23.0_ZJ" };
        var result5 = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCodes);
        Assert.AreEqual(2, result5.Count());
        Assert.IsTrue(result5.Any(e => e.EdgebandCode == "Test_Data_ABS_Abruzzo_colore_1.00_100.0_HM"));
        Assert.IsTrue(result5.Any(e => e.EdgebandCode == "Test_Data_ABS_Black_1.20_23.0_ZJ"));
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