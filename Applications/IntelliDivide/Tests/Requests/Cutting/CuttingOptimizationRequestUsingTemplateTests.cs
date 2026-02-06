using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting.Template;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
[TestCategory("IntelliDivide.Requests.Cutting.Template")]
public class CuttingOptimizationRequestUsingTemplateTests : IntelliDivideTestBase
{
    /// <summary />
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Cutting, CommonSampleSettings.TimeoutDuration);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_Template_Excel_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Cutting, CommonSampleSettings.CuttingImportTemplateName);

        await CuttingRequestUsingTemplateSamples.CuttingRequest_Template_Excel_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_Template_Excel_ImportAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Cutting, CommonSampleSettings.CuttingImportTemplateName);

        await CuttingRequestUsingTemplateSamples.CuttingRequest_Template_Excel_ImportAndOptimize(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_Template_Excel_StackingGroups_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Cutting, CommonSampleSettings.CuttingImportTemplateName);
        await EnsureOptimizationParametersExists(intelliDivide, OptimizationType.Cutting, CommonSampleSettings.CuttingStackingOptimizationParameters);

        await CuttingRequestUsingTemplateSamples.CuttingRequest_Template_Excel_ImportOnly(intelliDivide, CommonSampleSettings.CuttingStackingOptimizationParameters);
    }
}

#pragma warning restore S2699 // Tests should include assertions