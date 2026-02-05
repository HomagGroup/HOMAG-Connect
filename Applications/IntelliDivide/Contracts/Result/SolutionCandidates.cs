#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Factory and helpers for creating <see cref="SolutionCandidate" /> instances from solution data.
/// </summary>
public static class SolutionCandidates
{
    /// <summary>
    /// Creates a <see cref="SolutionCandidate" /> with provided key figures and calculation time.
    /// </summary>
    /// <param name="solutionId">The unique identifier of the solution.</param>
    /// <param name="solutionKeyFigures">Key figures used for scoring and evaluation.</param>
    /// <param name="calculationTime">Calculation time in seconds.</param>
    /// <returns>A populated <see cref="SolutionCandidate" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="solutionKeyFigures" /> is null or empty.</exception>
    public static SolutionCandidate From(Guid solutionId, Dictionary<SolutionKeyFigure, double?> solutionKeyFigures, double calculationTime)
    {
        if (solutionKeyFigures == null || solutionKeyFigures.Count == 0)
        {
            throw new ArgumentNullException(nameof(solutionKeyFigures));
        }

        return new SolutionCandidate
        {
            Id = solutionId,
            KeyFigures = solutionKeyFigures,
            CalculationTime = calculationTime
        };
    }

    /// <summary>
    /// Creates an array of <see cref="SolutionCandidate" /> from a collection of <see cref="SolutionDetails" />.
    /// Returns an empty array for empty input.
    /// </summary>
    /// <param name="solutionDetails">Source details used to construct candidates.</param>
    /// <returns>Constructed candidates.</returns>
    public static SolutionCandidate[] From(SolutionDetails[]? solutionDetails)
    {
        if (solutionDetails == null || solutionDetails.Length == 0)
        {
            return [];
        }

        return solutionDetails.Select(sd => new SolutionCandidate
        {
            Id = sd.Id,
            CalculationTime = 20,
            KeyFigures = GetSolutionKeyFigures(sd)
        }).ToArray();
    }

    /// <summary>
    /// Extracts key figures from <see cref="SolutionDetails" /> for evaluation.
    /// </summary>
    /// <param name="solutionDetails">Source solution details.</param>
    /// <returns>Dictionary of key figures and their values.</returns>
    public static Dictionary<SolutionKeyFigure, double?> GetSolutionKeyFigures(this SolutionDetails solutionDetails)
    {
        var keyFigures = new Dictionary<SolutionKeyFigure, double?>();
        var production = solutionDetails.KeyFigures.Production.Output;
        var material = solutionDetails.KeyFigures.Material.BoardsAndOffcuts;
        var costs = solutionDetails.Overview.Figures.Costs;

        // Costs
        keyFigures[SolutionKeyFigure.MaterialCosts] = costs.MaterialCosts ?? null;
        keyFigures[SolutionKeyFigure.ProductionCosts] = null; // Source currently unknown
        keyFigures[SolutionKeyFigure.TotalCosts] = null; // Source currently unknown

        // Production
        keyFigures[SolutionKeyFigure.ProductionTime] = production.ProductionTime.TotalSeconds;
        keyFigures[SolutionKeyFigure.PartsQuantity] = production.QuantityOfParts;
        keyFigures[SolutionKeyFigure.Cuts] = production.Cuts;

        // Material
        keyFigures[SolutionKeyFigure.OffcutsTotal] = material.OffcutsTotal;
        keyFigures[SolutionKeyFigure.WastePercentage] = material.Waste;

        return keyFigures;
    }
}