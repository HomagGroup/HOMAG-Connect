using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Evaluation;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Optimizations;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Optimizations")]
public class OptimizationsCandidateEvaluationTests : IntelliDivideTestBase
{
    [TestMethod]
    public async Task Optimizations_GetFirstOptimizationAndEvaluate()
    {
        var solutionDetails = await GetSampleSolutionDetails();

        var solutionCharacteristicsAndDisplayOrder = solutionDetails.DetermineCharacteristicsAndDisplayOrder();

        solutionCharacteristicsAndDisplayOrder.ShouldNotBeEmpty();
        solutionCharacteristicsAndDisplayOrder.Length.ShouldBe(solutionDetails.Count());

        solutionCharacteristicsAndDisplayOrder.Trace(nameof(solutionCharacteristicsAndDisplayOrder));

        TestContext?.AddResultFile(solutionCharacteristicsAndDisplayOrder.TraceToFile(nameof(solutionCharacteristicsAndDisplayOrder)).FullName);
    }

    [TestMethod]
    public async Task Optimizations_GetFirstOptimizationAndEvaluateAllProperties()
    {
        var solutionDetails = await GetSampleSolutionDetails();
        var solutionCandidates = SolutionCandidates.From(solutionDetails.ToArray());

        solutionCandidates.Trace(nameof(solutionCandidates));

    }

    private async Task<List<SolutionDetails>> GetSampleSolutionDetails()
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1)!.FirstOrDefaultAsync();

        if (optimization == null)
        {
            Assert.Inconclusive("No optimization found");
        }

        optimization.Link.Trace(nameof(optimization.Link));

        var solutionDetails = new List<SolutionDetails>();
        var solutions = await intelliDivide.GetSolutions(optimization.Id);

        Assert.IsNotNull(solutions);

        foreach (var solution in solutions)
        {
            var details = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);

            Assert.IsNotNull(details);
            solutionDetails.Add(details);
        }

        return solutionDetails;
    }
}