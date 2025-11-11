using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.Template;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.Template")]
public class NestingOptimizationRequestUsingTemplateTests : IntelliDivideTestBase
{
    /// <summary />
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, CommonSampleSettings.TimeoutDuration);
    }

    /// <summary />
    [TestMethod]
    public async Task NestingRequest_Template_CSV_MPR_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Nesting, CommonSampleSettings.NestingImportTemplateName);

        await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Nesting, CommonSampleSettings.NestingImportTemplateName);

        await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportAndOptimize(intelliDivide);
    }
}