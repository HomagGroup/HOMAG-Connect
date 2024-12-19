using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.ProjectZip;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.ProjectZip")]
[TemporaryDisabledOnServer(2025, 02, 13)]
public class NestingOptimizationRequestUsingProjectZipTests : IntelliDivideTestBase
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
    public async Task NestingRequest_ProjectZip_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingProjectZip.NestingRequest_ProjectZip_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 01, 13)]
    public async Task NestingRequest_ProjectZip_Optimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingProjectZip.NestingRequest_ProjectZip_Optimize(intelliDivide);
    }
}