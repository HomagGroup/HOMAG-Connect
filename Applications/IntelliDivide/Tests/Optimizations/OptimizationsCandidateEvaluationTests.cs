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
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized,1)!.FirstOrDefaultAsync();

        if (optimization == null)
        {
            Assert.Inconclusive("No optimization found");
        }

        var solutionDetails = new List<SolutionDetails>();
        var solutions = await intelliDivide.GetSolutions(optimization.Id);

        Assert.IsNotNull(solutions);

        foreach (var solution in solutions)
        {
            var details = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);

            Assert.IsNotNull(details);
            solutionDetails.Add(details);
        }

        var solutionCharacteristicsAndDisplayOrder = solutionDetails.DetermineCharacteristicsAndDisplayOrder();

        solutionCharacteristicsAndDisplayOrder.ShouldNotBeEmpty();
        solutionCharacteristicsAndDisplayOrder.Count().ShouldBe(solutions.Count());

        solutionCharacteristicsAndDisplayOrder.Trace(nameof(solutionCharacteristicsAndDisplayOrder));
        
    }
}