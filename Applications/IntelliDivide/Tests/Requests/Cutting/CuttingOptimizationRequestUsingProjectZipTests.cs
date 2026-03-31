using HomagConnect.Base.TestBase.Attributes;
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
[DeploymentTest("IntelliDivide.Requests.Cutting.ProjectZip")]
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
    [TemporaryDisabledOnServer(2026, 04, 06, "DF-Optimization")]
    public async Task CuttingRequest_ProjectZip_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingProjectZip.CuttingRequest_ProjectZip_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2026, 04, 06, "DF-Optimization")]
    public async Task CuttingRequest_ProjectZip_StackingGroups_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingProjectZip.CuttingRequest_ProjectZip_ImportOnly(intelliDivide, CommonSampleSettings.CuttingStackingZipFile, CommonSampleSettings.CuttingStackingOptimizationParameters);
    }
}

#pragma warning restore S2699 // Tests should include assertions