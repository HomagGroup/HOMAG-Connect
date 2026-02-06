using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Specifies the characteristics used to evaluate or select a solution in the order to be displayed.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(SolutionCharacteristicDisplayNames))]
public enum SolutionCharacteristic
{
    /// <summary>
    /// Indicates that the value is unknown or has not been specified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Solution with the lowest total cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if both production and material costs are known.
    /// It is generally recommended for typical scenarios.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.TotalCostsPerPart, 1000)]
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestTotalCosts), Description = "LowestTotalCostsDescription")]
    LowestTotalCosts,

    /// <summary>
    /// The alternative cuts most parts in automatic mode, thereby reducing manual effort.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(HighestAutomationLevel), Description = "HighestAutomationLevelDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityAutomaticMode, 1000)]
    HighestAutomationLevel,

    /// <summary>
    /// Solution with the lowest material cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if the material costs are known.
    /// It is recommended when production time is not a relevant factor.
    /// </remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestMaterialCosts), Description = "LowestMaterialCostsDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.MaterialCostsPerPart, 1000)]
    LowestMaterialCosts,

    /// <summary>
    /// Represents a solution that maintains a balanced state according to defined weights
    /// </summary>
    /// <remarks>
    /// This characteristic is recommended when the total costs can't get calculated.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePlusOffcutsArea, 1000,
        SolutionKeyFigure.ProductionTimePerPart, 500,
        SolutionKeyFigure.OffcutsTotal, 500,
        SolutionKeyFigure.WasteArea, 500
    )]
    BalancedSolution,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.WholeBoardsRequired, 1000)]
    LowestWholeBoardsQuantity,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.ProductionTime, 1000)]
    FastestProduction,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.BookHeightAverage, 1000)]
    MaximumAverageBookHeight,

    [SolutionCharacteristicScoreWeights(
            SolutionKeyFigure.OffcutsTotal, 1000,
            SolutionKeyFigure.OffcutsProduced, 100
        )
    ]
    LowestOffcutsTotalQuantity,

    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsSmallTotal, 1000,
        SolutionKeyFigure.OffcutsSmallProduced, 100
    )]
    LowestOffcutsSmallTotalQuantity,

    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.Recuts, 1000,
        SolutionKeyFigure.Headcuts, 500,
        SolutionKeyFigure.BookWeightMax, 200
    )]
    LowestComplexity,

    /// <summary>
    /// Compromise of all key figures with stronger weighting of waste.
    /// </summary>
    [SolutionCharacteristicScoreWeights(
            SolutionKeyFigure.WastePercentage, 1000,
            SolutionKeyFigure.WastePlusOffcutsPercentage, 100
        )
    ]
    LowestWastePercentage,

    [SolutionCharacteristicScoreWeights(
            SolutionKeyFigure.WastePlusOffcutsPercentage, 1000,
            SolutionKeyFigure.WastePercentage, 100
        )
    ]
    LowestWastePlusOffcutsPercentage,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityPlusParts, 1000)]
    HighestNumberOfPlusParts,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.Cycles, 1000)]
    LowestNumberOfCycles,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PatternCount, 1000)]
    LowestNumberOfPatterns,

    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.QuantityPerPatternAverage, 1000)]
    SimilarPatterns,

    /// <summary>
    /// The solution has no special characteristic and should not be displayed.
    /// </summary>
    None = 99
}