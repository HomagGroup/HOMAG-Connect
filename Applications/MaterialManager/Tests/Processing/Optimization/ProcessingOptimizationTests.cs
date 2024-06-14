using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests.Attributes;

namespace HomagConnect.MaterialManager.Tests.Processing.Optimization;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Processing")]
[TestCategory("MaterialManager.Processing.Optimization")]
public class ProcessingOptimizationTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2024, 6, 14)] // todo: reenable tests divide!
    public async Task OffcutParameters_RequestForMultipleMaterials_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        var materialCodes = new[] { "P2_Gold Craft Oak_19.0", "P2_White_19.0", "MDF_19.0", "VP_Fichte_19.0", "HPL_F638_10.0" };

        var offcutParameterSets = await client.Processing.Optimization.GetOffcutParameterSetsAsync(materialCodes).ToListAsync();

        offcutParameterSets.Should().NotBeNull();
        offcutParameterSets.Count.Should().BeGreaterThan(0);

        Trace(offcutParameterSets);

        foreach (var offcutParameterSet in offcutParameterSets)
        {
            if (!DataAnnotationsValidator.TryValidateObjectRecursive(offcutParameterSet, out var validationResults))
            {
                Assert.Fail(validationResults[0].ErrorMessage);
            }
        }
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2024, 6, 14)] // todo: reenable tests divide!
    public async Task OffcutParameters_RequestForOneMaterial_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        const string materialCode = "P2_Gold Craft Oak_19.0";

        var offcutParameterSet = await client.Processing.Optimization.GetOffcutParameterSetAsync(materialCode);

        offcutParameterSet.Should().NotBeNull();

        offcutParameterSet.MaterialGroupName.Should().NotBeNullOrEmpty();
        offcutParameterSet.MaterialCodes.Should().Contain(materialCode);

        Trace(offcutParameterSet);

        if (!DataAnnotationsValidator.TryValidateObjectRecursive(offcutParameterSet, out var validationResults))
        {
            Assert.Fail(validationResults[0].ErrorMessage);
        }
    }
}