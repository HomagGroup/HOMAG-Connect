using FluentAssertions;

using HomagConnect.Base.Extensions;

namespace HomagConnect.MaterialManager.Tests.Processing.Optimization;

[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Processing")]
[TestCategory("MaterialManager.Processing.Optimization")]
public class ProcessingOptimizationTests : MaterialManagerTestBase
{
    [TestMethod]
    public async Task OffcutParameter_RequestForMultipleMaterials_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        var materialCodes = new[] { "P2_Gold Craft Oak_19.0", "P2_White_19.0", "MDF_19.0", "VP_Fichte_19.0" };

        var offcutParameterSets = await client.Processing.Optimization.GetOffcutParameterSetsAsync(materialCodes).ToListAsync();

        offcutParameterSets.Should().NotBeNull();
        offcutParameterSets.Count.Should().BeGreaterThan(0);

        Trace(offcutParameterSets);
    }

    [TestMethod]
    public async Task OffcutParameter_RequestForOneMaterial_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        const string materialCode = "P2_Gold Craft Oak_19.0";

        var offcutParameterSet = await client.Processing.Optimization.GetOffcutParameterSetAsync(materialCode);

        offcutParameterSet.Should().NotBeNull();

        offcutParameterSet.MaterialGroupName.Should().NotBeNullOrEmpty();
        offcutParameterSet.MaterialCodes.Should().Contain(materialCode);
        offcutParameterSet.Parameters.Should().NotBeNull();

        Trace(offcutParameterSet);
    }
}