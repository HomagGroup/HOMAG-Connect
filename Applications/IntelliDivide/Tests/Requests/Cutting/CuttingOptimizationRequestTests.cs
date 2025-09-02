using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
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
    [Ignore("This test does currently not work or works only sometimes.")]
    public async Task CuttingRequest_Common_RetrieveResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationCommonSamples.CuttingRequest_RetrieveResults(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task BoardMaterial_GetBoardMaterialCategoryDisplayNames()
    {
        var intelliDivide = GetIntelliDivideClient();

        var displayNames = await intelliDivide.GetBoardMaterialCategoryDisplayNames();

        Assert.IsNotNull(displayNames);
        Assert.IsTrue(displayNames.Any());

        displayNames.Trace();
    }
}

#pragma warning restore S2699 // Tests should include assertions