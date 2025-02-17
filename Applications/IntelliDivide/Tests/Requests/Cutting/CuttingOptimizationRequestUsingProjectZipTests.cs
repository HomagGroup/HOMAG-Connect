using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting.Project;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
[TestCategory("IntelliDivide.Requests.Cutting.ProjectZip")]
[TemporaryDisabledOnServer(2025, 02, 28, "DF-Optimization")]
public class CuttingOptimizationRequestUsingProjectZipTests : IntelliDivideTestBase
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
    public async Task CuttingRequest_ProjectZip_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingProjectZip.CuttingRequest_ProjectZip_ImportOnly(intelliDivide);
    }
}

#pragma warning restore S2699 // Tests should include assertions