using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

using Shouldly;

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
    public async Task OffcutParameters_RequestForMultipleMaterials_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        var materialCodes = new[] { "P2_Gold_Craft_Oak_19.0", "P2_White_19.0", "MDF_19.0", "VP_Fichte_19.0", "HPL_F638_10.0" };

        var offcutParameterSets = (await client.Processing.Optimization.GetOffcutParameterSetsAsync(materialCodes).ConfigureAwait(false) ?? Array.Empty<OffcutParameterSet>()).ToList();

        offcutParameterSets.ShouldNotBeNull(
            "because GetOffcutParameterSetsAsync should return a collection of offcut parameter sets");
        offcutParameterSets.Count.ShouldBeGreaterThan(0,
            $"because offcut parameter sets should exist for the requested material codes: {string.Join(", ", materialCodes)}");

        offcutParameterSets.Trace();

        foreach (var offcutParameterSet in offcutParameterSets)
        {
            var isValid = DataAnnotationsValidator.TryValidateObjectRecursive(offcutParameterSet, out var validationResults);

            isValid.ShouldBeTrue(
                $"because offcut parameter set for material group '{offcutParameterSet.MaterialGroupName}' should be valid, " +
                $"but validation failed with: {(validationResults.Any() ? validationResults[0].ErrorMessage : "unknown error")}");
        }
    }

    /// <summary />
    [TestMethod]
    public async Task OffcutParameters_RequestForOneMaterial_ConfigValid()
    {
        var client = GetMaterialManagerClient();
        const string materialCode = "P2_Gold_Craft_Oak_19.0";

        var offcutParameterSet = await client.Processing.Optimization.GetOffcutParameterSetAsync(materialCode);

        offcutParameterSet.ShouldNotBeNull(
            $"because offcut parameter set should be retrieved for material code '{materialCode}'");

        offcutParameterSet!.MaterialGroupName.ShouldNotBeNullOrEmpty(
            $"because offcut parameter set for material code '{materialCode}' should have a material group name");
        offcutParameterSet.MaterialCodes.ShouldContain(materialCode,
            $"because offcut parameter set should contain the requested material code '{materialCode}'");

        offcutParameterSet.Trace();

        var isValid = DataAnnotationsValidator.TryValidateObjectRecursive(offcutParameterSet, out var validationResults);

        isValid.ShouldBeTrue(
            $"because offcut parameter set for material code '{materialCode}' should be valid, " +
            $"but validation failed with: {(validationResults.Any() ? validationResults[0].ErrorMessage : "unknown error")}");
    }
}