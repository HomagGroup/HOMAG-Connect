using HomagConnect.IntelliDivide.Samples.Results;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Results;

[TestClass]
[TestCategory("IntelliDivide.Optimization.Result")]
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
    public async Task GetOptimizationStatus()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationStatusSample(intelliDivide);
    }


    [TestMethod]
    public async Task GetOptimizations()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsSample(intelliDivide);
    }

    [TestMethod]
    public async Task GetOptimizationSolutions()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationSolutionsSample(intelliDivide);
    }
}