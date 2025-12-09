using FluentAssertions;

using HomagConnect.MaterialManager.Samples.Update.Edgebands;

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

        checkEdgeband.Should().NotBeNull(
            $"because edgeband type with edgeband code '{EdgebandCode}' should exist after update");
        checkEdgeband!.DefaultLength.Should().Be(value,
            $"because edgeband type '{EdgebandCode}' was updated to default length {value}");
        return;

        double RandomBetween(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateEdgebandTypeTests();
        await classInstance.EnsureEdgebandTypeExist(EdgebandCode, 2);
    }
}