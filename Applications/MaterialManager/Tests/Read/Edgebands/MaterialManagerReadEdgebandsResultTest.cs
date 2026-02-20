using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Samples.Read.Edgebands;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Read.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class ReadEdgebandTypeTests : MaterialManagerTestBase
{
    private MaterialManagerClientMaterialEdgebands materialManagerClient;

    /// <summary>
    /// Initializes the test by setting up the <see cref="MaterialManagerClient"/> and ensuring the board type exists.
    /// </summary>
    [TestInitialize]
    public async Task Init()
    {
        materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
        await EnsureEdgebandTypeExist(materialManagerClient, "ABS_Abruzzo_colore_1.00_100.0_HM", 1.0, 100.0);
        await EnsureEdgebandTypeExist(materialManagerClient, "ABS_Black_1.20_23.0_ZJ", 1.2);
    }


    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCode()
    {
        const string edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCode(materialManagerClient, edgebandCode);

        result.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{edgebandCode}' should exist");
        result!.EdgebandCode.ShouldBe(edgebandCode,
            $"because we retrieved edgeband type by edgeband code '{edgebandCode}'");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCodeIncludingDetails()
    {
        const string edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(materialManagerClient, edgebandCode);

        result.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{edgebandCode}' should exist including details");
        result!.EdgebandCode.ShouldBe(edgebandCode,
            $"because we retrieved edgeband type by edgeband code '{edgebandCode}' including details");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeInventoryHistoryAsync()
    {        

        var act = async () => await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeInventoryHistoryAsync(materialManagerClient);

        await Should.NotThrowAsync(act,
            "because GetEdgebandTypeInventoryHistoryAsync should retrieve inventory history successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypes()
    {
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypes(materialManagerClient) ?? Array.Empty<EdgebandType>()).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandTypes should return a collection of edgeband types");
        result.Length.ShouldBeGreaterThanOrEqualTo(2,
            "because at least 2 edgeband types (ABS_Abruzzo_colore_1.00_100.0_HM, ABS_Black_1.20_23.0_ZJ) should exist");
    }
    
    /// <summary />
    [TestMethod]
    public async Task GetEdgebandTypes_ChangedSince_NoException()
    {
        var result = await materialManagerClient.GetEdgebandTypes(DateTimeOffset.UtcNow.AddDays(-2), 2);
        Assert.IsNotNull(result, "because GetEdgebandTypes should return a result for changed since filter");
    }
    
    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodes()
    {        
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodes(materialManagerClient, edgebandCodes)).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandTypesByEdgebandCodes should return a collection of edgeband types");
        result.Length.ShouldBe(2,
            $"because we requested 2 specific edgeband codes: {string.Join(", ", edgebandCodes)}");
        result.Any(e => e != null && e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM").ShouldBeTrue();
        result.Any(e => e != null && e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ").ShouldBeTrue();
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodesIncludingDetails()
    {
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(materialManagerClient, edgebandCodes)).ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandTypesByEdgebandCodesIncludingDetails should return a collection of edgeband types with details");
        result.Length.ShouldBe(2,
            $"because we requested 2 specific edgeband codes with details: {string.Join(", ", edgebandCodes)}");
        result.Any(e => e != null && e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM").ShouldBeTrue();
        result.Any(e => e != null && e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ").ShouldBeTrue();
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesIncludingDetails()
    {
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesIncludingDetails(materialManagerClient) ?? Array.Empty<EdgebandTypeDetails>())
            .ToArray();

        result.ShouldNotBeNull(
            "because GetEdgebandTypesIncludingDetails should return a collection of edgeband types with details");
        result.Length.ShouldBeGreaterThanOrEqualTo(2,
            "because at least 2 edgeband types (ABS_Abruzzo_colore_1.00_100.0_HM, ABS_Black_1.20_23.0_ZJ) should exist with details");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetLicensedMachines()
    {
                var act = async () => await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetLicensedMachines(materialManagerClient);

        await Should.NotThrowAsync(act,
            "because GetLicensedMachines should retrieve licensed machines successfully");
    }
}