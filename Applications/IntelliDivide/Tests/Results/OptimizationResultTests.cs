using HomagConnect.IntelliDivide.Samples.Results;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Results;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Results")]
[TestCategory("UserTestInteractionNeeded")]
public class OptimizationResultTests : IntelliDivideTestBase
{
    [TestMethod]
    public async Task GetOptimization()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetOptimizationsHavingStatusOptimized()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsHavingStatusOptimized(intelliDivide);
    }

    [TestMethod]
    public async Task GetOptimizationsOfTypeCutting()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsOfTypeCuttingSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetOptimizations()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetOptimizationStatus()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationStatusSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetSolutionDetails()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionDetailsSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetSolutions()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionsSample(intelliDivide);
    }
}