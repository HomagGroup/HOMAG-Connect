using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Samples.Update.Edgebands;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Edgebands;

/// <summary />
[TestClass]
[TestCategory("DeploymentTests.MaterialManager")]
[TestCategory("DeploymentTests.MaterialManager.Edgebands")]
public class UpdateEdgebandTypeTests : MaterialManagerTestBase
{

    private MaterialManagerClientMaterialEdgebands _MaterialManagerClient = null!;

    /// <summary>
    /// Initializes the test by setting up the <see cref="MaterialManagerClient"/> and ensuring the board type exists.
    /// </summary>
    [TestInitialize]
    public async Task Init()
    {
        _MaterialManagerClient = GetMaterialManagerClient().Material.Edgebands;
        await EnsureEdgebandTypeExist(_MaterialManagerClient, EdgebandCode, 2);
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsUpdateEdgebandType()
    {
        var value = Math.Round(RandomBetween(50.0, 100.0), 2);

        var materialManagerClient = GetMaterialManagerClient();
        
        await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands, EdgebandCode, value);

        var checkEdgeband = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(EdgebandCode);

        checkEdgeband.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{EdgebandCode}' should exist after update");
        checkEdgeband.DefaultLength.ShouldBe(value,
            $"because edgeband type '{EdgebandCode}' was updated to default length {value}");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandTypeUpdate_WithAdditionalData_Succeeds()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var act = async () => await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType_AdditionalData(
            materialManagerClient.Material.Edgebands, EdgebandCode);

        await act.ShouldNotThrowAsync(
            $"because creating edgeband type with edgeband code '{EdgebandCode}' and additional data should complete successfully");
    }
}