#nullable enable

using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Extension methods for creating <see cref="SolutionCandidate" /> arrays.
/// </summary>
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

        var solutionCandidates = new List<SolutionCandidate>();

        foreach (var sd in solutionDetails)
        {
            var production = sd.KeyFigures?.Production?.Output;
            var material = sd.KeyFigures?.Material?.BoardsAndOffcuts;
            var costs = sd.Overview?.Figures?.Costs;

            var solutionCandidate = new SolutionCandidate
            {
                Id = sd.Id,
                CalculationTime = 20
            };

            solutionCandidate.KeyFigures[SolutionKeyFigure.MaterialCosts] = costs?.MaterialCosts ?? null;

            solutionCandidate.KeyFigures[SolutionKeyFigure.ProductionCosts] = 0;
            solutionCandidate.KeyFigures[SolutionKeyFigure.TotalCosts] = 0;
            solutionCandidate.KeyFigures[SolutionKeyFigure.ProductionTime] = production?.ProductionTime.TotalSeconds ?? 0;
            solutionCandidate.KeyFigures[SolutionKeyFigure.PartsQuantity] = production?.QuantityOfParts ?? 0;

            solutionCandidate.KeyFigures[SolutionKeyFigure.OffcutsTotal] = material?.OffcutsTotal ?? 0;
            solutionCandidate.KeyFigures[SolutionKeyFigure.Cuts] = production?.Cuts ?? 0;
            solutionCandidate.KeyFigures[SolutionKeyFigure.WastePercentage] = material?.Waste ?? 0;

            solutionCandidates.Add(solutionCandidate);
        }

        return solutionCandidates.ToArray();
    }
}