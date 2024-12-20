using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
[TestCategory("IntelliDivide.Requests.Cutting.Common")]
[TemporaryDisabledOnServer(2025, 02, 13)]
public class CuttingOptimizationRequestTests : IntelliDivideTestBase
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
    [TemporaryDisabledOnServer(2025, 01, 13)]
    public async Task CuttingRequest_Common_SpecificBoards()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationCommonSamples.CuttingRequest_SpecificBoards(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_Common_SendSpecificSolution()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationCommonSamples.CuttingRequest_SendSpecificSolution(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 01, 13)]
    public async Task CuttingRequest_Common_RetrieveResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationCommonSamples.CuttingRequest_RetrieveResults(intelliDivide);
    }
}

#pragma warning restore S2699 // Tests should include assertions