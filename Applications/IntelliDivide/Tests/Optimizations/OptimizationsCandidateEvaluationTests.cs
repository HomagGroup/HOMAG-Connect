using System.Globalization;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;
using HomagConnect.IntelliDivide.Contracts.Extensions;

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
        var cultureInfo = new CultureInfo("de");
        var solutionDetails = await GetSampleSolutionDetails();
        var solutionCandidateEvaluationResults = solutionDetails.DetermineCharacteristicsAndDisplayOrder();

        solutionCandidateEvaluationResults.ShouldNotBeEmpty();
        solutionCandidateEvaluationResults.Length.ShouldBe(solutionDetails.Count());

        solutionCandidateEvaluationResults.Select(s => new
        {
            s.Id,
            s.Characteristic,
            DisplayName = s.Characteristic.GetLocalizedName(cultureInfo),
            Description = s.Characteristic.GetLocalizedDescription(s.CharacteristicsInAddition, cultureInfo)
        }).Trace();

        
        solutionCandidateEvaluationResults.Trace();

        TestContext?.AddResultFile(solutionCandidateEvaluationResults.TraceToFile(nameof(solutionCandidateEvaluationResults)).FullName);
    }

    private async Task<List<SolutionDetails>> GetSampleSolutionDetails()
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        var optimization = await intelliDivide.GetOptimizations(OptimizationType.Nesting, OptimizationStatus.Optimized, 1)!.FirstOrDefaultAsync();

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