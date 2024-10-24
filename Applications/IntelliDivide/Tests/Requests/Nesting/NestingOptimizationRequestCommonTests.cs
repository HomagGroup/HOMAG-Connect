using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.Common")]
public class NestingOptimizationRequestCommonTests : IntelliDivideTestBase
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
    [TemporaryDisabledOnServer(2024, 11, 20)]
    public async Task NestingRequest_Common_RetrieveResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationCommonSamples.NestingRequest_Common_RetrieveResults(intelliDivide);
    }
}