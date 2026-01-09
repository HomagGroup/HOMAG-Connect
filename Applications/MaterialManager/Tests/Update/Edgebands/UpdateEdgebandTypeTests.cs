using HomagConnect.MaterialManager.Samples.Update.Edgebands;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class UpdateEdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task EdgebandsUpdateEdgebandType()
    {
        var random = new Random();

        var value = Math.Round(RandomBetween(50.0, 100.0), 2);

        var materialManagerClient = GetMaterialManagerClient();
        
        await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands, EdgebandCode, value);

        var checkEdgeband = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(EdgebandCode);

        checkEdgeband.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{EdgebandCode}' should exist after update");
        checkEdgeband!.DefaultLength.ShouldBe(value,
            $"because edgeband type '{EdgebandCode}' was updated to default length {value}");
        return;

        double RandomBetween(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandTypeUpdate_WithAdditionalData_Succeeds()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "EB_White_1mm_AdditionalData";

        var act = async () => await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType_AdditionalData(
            materialManagerClient.Material.Edgebands, edgebandCode);

        await act.ShouldNotThrowAsync(
            $"because creating edgeband type with edgeband code '{edgebandCode}' and additional data should complete successfully");
    }

    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateEdgebandTypeTests();
        await classInstance.EnsureEdgebandTypeExist(EdgebandCode, 2);
    }
}