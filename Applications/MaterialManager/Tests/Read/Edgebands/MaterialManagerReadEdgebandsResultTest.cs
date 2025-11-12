using FluentAssertions;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Samples.Read.Edgebands;

namespace HomagConnect.MaterialManager.Tests.Read.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class ReadEdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCode()
    {
        var materialManagerClient = GetMaterialManagerClient();
        const string edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCode(materialManagerClient.Material.Edgebands, edgebandCode);

        result.Should().NotBeNull(
            $"because edgeband type with edgeband code '{edgebandCode}' should exist");
        result?.EdgebandCode.Should().Be(edgebandCode,
            $"because we retrieved edgeband type by edgeband code '{edgebandCode}'");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeByEdgebandCodeIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        const string edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
        var result = await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCode);

        result.Should().NotBeNull(
            $"because edgeband type with edgeband code '{edgebandCode}' should exist including details");
        result?.EdgebandCode.Should().Be(edgebandCode,
            $"because we retrieved edgeband type by edgeband code '{edgebandCode}' including details");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypeInventoryHistoryAsync()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypeInventoryHistoryAsync(materialManagerClient.Material.Edgebands);

        await act.Should().NotThrowAsync(
            "because GetEdgebandTypeInventoryHistoryAsync should retrieve inventory history successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypes(materialManagerClient.Material.Edgebands) ?? Array.Empty<EdgebandType>()).ToArray();

        result.Should().NotBeNull(
            "because GetEdgebandTypes should return a collection of edgeband types");
        result.Should().HaveCountGreaterOrEqualTo(2,
            "because at least 2 edgeband types (ABS_Abruzzo_colore_1.00_100.0_HM, ABS_Black_1.20_23.0_ZJ) should exist");
    }
    
    /// <summary />
    [TestMethod]
    public async Task GetEdgebandTypes_ChangedSince_NoException()
    {
        var materialManager = GetMaterialManagerClient();
        var result = await materialManager.Material.Edgebands.GetEdgebandTypes(DateTimeOffset.UtcNow.AddDays(-2), 2);
        Assert.IsNotNull(result, "because GetEdgebandTypes should return a result for changed since filter");
    }
    
    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodes()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodes(materialManagerClient.Material.Edgebands, edgebandCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetEdgebandTypesByEdgebandCodes should return a collection of edgeband types");
        result.Should().HaveCount(2,
            $"because we requested 2 specific edgeband codes: {string.Join(", ", edgebandCodes)}");
        result.Should().Contain(e => e != null && e.EdgebandCode != null && e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM",
            "because edgeband type 'ABS_Abruzzo_colore_1.00_100.0_HM' was requested");
        result.Should().Contain(e => e != null && e.EdgebandCode != null && e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ",
            "because edgeband type 'ABS_Black_1.20_23.0_ZJ' was requested");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesByEdgebandCodesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ABS_Black_1.20_23.0_ZJ" };
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(materialManagerClient.Material.Edgebands, edgebandCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetEdgebandTypesByEdgebandCodesIncludingDetails should return a collection of edgeband types with details");
        result.Should().HaveCount(2,
            $"because we requested 2 specific edgeband codes with details: {string.Join(", ", edgebandCodes)}");
        result.Should().Contain(e => e != null && e.EdgebandCode != null && e.EdgebandCode == "ABS_Abruzzo_colore_1.00_100.0_HM",
            "because edgeband type 'ABS_Abruzzo_colore_1.00_100.0_HM' was requested with details");
        result.Should().Contain(e => e != null && e.EdgebandCode != null && e.EdgebandCode == "ABS_Black_1.20_23.0_ZJ",
            "because edgeband type 'ABS_Black_1.20_23.0_ZJ' was requested with details");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetEdgebandTypesIncludingDetails()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var result = (await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetEdgebandTypesIncludingDetails(materialManagerClient.Material.Edgebands) ?? Array.Empty<EdgebandTypeDetails>())
            .ToArray();

        result.Should().NotBeNull(
            "because GetEdgebandTypesIncludingDetails should return a collection of edgeband types with details");
        result.Should().HaveCountGreaterOrEqualTo(2,
            "because at least 2 edgeband types (ABS_Abruzzo_colore_1.00_100.0_HM, ABS_Black_1.20_23.0_ZJ) should exist with details");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsGetLicensedMachines()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadEdgebandResultsSamples.Edgebands_GetLicensedMachines(materialManagerClient.Material.Edgebands);

        await act.Should().NotThrowAsync(
            "because GetLicensedMachines should retrieve licensed machines successfully");
    }

    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new ReadEdgebandTypeTests();
        await classInstance.EnsureEdgebandTypeExist("ABS_Abruzzo_colore_1.00_100.0_HM", 1.0, 100.0);
        await classInstance.EnsureEdgebandTypeExist("ABS_Black_1.20_23.0_ZJ", 1.2);
    }
}