#nullable enable
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

public static class SolutionCandidates
{
    /// <summary>
    /// Creates an array of <see cref="SolutionCandidate" /> from a collection of <see cref="SolutionDetails" />.
    /// Also computes normalized scores (0–1000) for each numeric metric where lower values score higher.
    /// Returns an empty array for null or empty input.
    /// </summary>
    /// <param name="solutionDetails">Source details used to construct candidates.</param>
    /// <returns>Constructed candidates with computed scores.</returns>
    public static SolutionCandidate[] From(SolutionDetails[] solutionDetails)
    {
        if (solutionDetails.Length == 0)
        {
            return [];
        }

        var candidates = new SolutionCandidate[solutionDetails.Length];

        for (var i = 0; i < solutionDetails.Length; i++)
        {
            var sd = solutionDetails[i];
            var production = sd.KeyFigures?.Production?.Output;
            var material = sd.KeyFigures?.Material?.BoardsAndOffcuts;
            var costs = sd.Overview?.Figures?.Costs;

            candidates[i] = new SolutionCandidate
            {
                Id = sd.Id,
                CalculationTime = 20,
                MaterialCosts = costs?.MaterialCosts ?? 0,
                // Source currently does not expose ProductionCosts/TotalCosts; set to 0
                ProductionCosts = 0,
                TotalCosts = 0,
                ProductionTime = production?.ProductionTime.TotalSeconds ?? 0,
                QuantityOfParts = production?.QuantityOfParts ?? 0,
                OffcutsTotal = material?.OffcutsTotal ?? 0,
                Cuts = production?.Cuts ?? 0,
                Waste = material?.Waste ?? 0
            };
        }

        // Compute and assign scores per candidate using extension method
        candidates.CalculateAndSetScores();

        return candidates;
    }
}