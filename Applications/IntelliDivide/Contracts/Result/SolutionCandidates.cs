#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

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

        // Material
        keyFigures[SolutionKeyFigure.WastePercentage] = solutionDetails.Overview.Figures.Material.Waste;
        keyFigures[SolutionKeyFigure.WastePlusOffcutsPercentage] = solutionDetails.Overview.Figures.Material.WastePlusOffcuts;
        keyFigures[SolutionKeyFigure.WholeBoardsRequired] = solutionDetails.Overview.Figures.Material.WholeBoards;

        keyFigures[SolutionKeyFigure.WasteArea] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.WasteArea;

        keyFigures[SolutionKeyFigure.OffcutsRequired] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsRequired;
        keyFigures[SolutionKeyFigure.OffcutsProduced] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsProduced;
        keyFigures[SolutionKeyFigure.OffcutsTotal] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsTotal;

        keyFigures[SolutionKeyFigure.OffcutsSmallRequired] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsSmallRequired;
        keyFigures[SolutionKeyFigure.OffcutsSmallProduced] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsSmallProduced;
        keyFigures[SolutionKeyFigure.OffcutsSmallTotal] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsSmallTotal;

        keyFigures[SolutionKeyFigure.OffcutsLargeRequired] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsLargeRequired;
        keyFigures[SolutionKeyFigure.OffcutsLargeProduced] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsLargeProduced;
        keyFigures[SolutionKeyFigure.OffcutsLargeTotal] = solutionDetails.KeyFigures.Material.BoardsAndOffcuts.OffcutsLargeTotal;

        keyFigures[SolutionKeyFigure.EdgebandLength] = solutionDetails.KeyFigures.Material.Edgebands.EdgebandLength;

        // Costs
        keyFigures[SolutionKeyFigure.MaterialCosts] = solutionDetails.Overview.Figures.Costs.MaterialCosts;
        keyFigures[SolutionKeyFigure.MaterialCostsPerPart] = solutionDetails.Overview.Figures.Costs.MaterialCostsPerPart;

        keyFigures[SolutionKeyFigure.BoardsPlusOffcutsCosts] = solutionDetails.Overview.Figures.Costs.CostsOfBoardsPlusOffcuts;
        keyFigures[SolutionKeyFigure.EdgebandCosts] = solutionDetails.Overview.Figures.Costs.CostsOfEdgebands;

        keyFigures[SolutionKeyFigure.ProductionCosts] = solutionDetails.Overview.Figures.Costs.ProductionCosts;
        keyFigures[SolutionKeyFigure.ProductionCostsPerPart] = solutionDetails.Overview.Figures.Costs.ProductionCostsPerPart;

        keyFigures[SolutionKeyFigure.TotalCosts] = solutionDetails.Overview.Figures.Costs.TotalCosts;
        keyFigures[SolutionKeyFigure.TotalCostsPerPart] = solutionDetails.Overview.Figures.Costs.TotalCostsPerPart;

        // Production
        keyFigures[SolutionKeyFigure.ProductionTime] = solutionDetails.KeyFigures.Production.Output.ProductionTime.TotalSeconds;
        keyFigures[SolutionKeyFigure.ProductionTimePerPart] = solutionDetails.KeyFigures.Production.Output.ProductionTimePerPart;

        keyFigures[SolutionKeyFigure.BookHeightAverage] = solutionDetails.KeyFigures.Production.Handling.AverageBookHeight;
        keyFigures[SolutionKeyFigure.BookHeightMax] = solutionDetails.KeyFigures.Production.Handling.MaxBookHeight;

        keyFigures[SolutionKeyFigure.PartsQuantity] = solutionDetails.KeyFigures.Production.Output.QuantityOfParts;
        keyFigures[SolutionKeyFigure.PartsQuantityPlusParts] = solutionDetails.KeyFigures.Production.Output.QuantityOfPlusParts;
        keyFigures[SolutionKeyFigure.PartsQuantityTotal] = solutionDetails.KeyFigures.Production.Output.QuantityOfPartsTotal;

        keyFigures[SolutionKeyFigure.PartsQuantityAutomaticMode] = solutionDetails.KeyFigures.Production.Output.PartsQuantityAutomaticMode;
        keyFigures[SolutionKeyFigure.PartsQuantityManualMode] = solutionDetails.KeyFigures.Production.Output.PartsQuantityManualMode;

        keyFigures[SolutionKeyFigure.Cuts] = solutionDetails.KeyFigures.Production.Output.Cuts;
        keyFigures[SolutionKeyFigure.Cycles] = solutionDetails.KeyFigures.Production.Output.Cycles;
        keyFigures[SolutionKeyFigure.CuttingLength] = solutionDetails.KeyFigures.Production.Output.CuttingLength;

        keyFigures[SolutionKeyFigure.Headcuts] = solutionDetails.KeyFigures.Production.Handling.HeadCuts;
        keyFigures[SolutionKeyFigure.Recuts] = solutionDetails.KeyFigures.Production.Handling.Recuts;

        keyFigures[SolutionKeyFigure.BookWeightAverage] = solutionDetails.KeyFigures.Production.Handling.BookWeightAverage;
        keyFigures[SolutionKeyFigure.BookWeightMax] = solutionDetails.KeyFigures.Production.Handling.BookWeightMax;

        #region Missing key figures

        #region QuantityPerPatternAverage

        var patterns = solutionDetails.Overview.Pattern.ToArray();

        double patternsCount = patterns.Length;
        double patternsQuantityTotal = patterns.Sum(p => p.Quantity);

        if (patternsCount > 0)
        {
            keyFigures.Add(SolutionKeyFigure.QuantityPerPatternAverage, patternsQuantityTotal / patternsCount);
        }

        solutionDetails.KeyFigures.Production.Handling.QuantityPerPatternAverage = patternsQuantityTotal / patternsCount;

        #endregion

        #endregion

        return keyFigures;
    }
}