using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Specifies the characteristics used to evaluate or select a solution. Each member has a localized display name and
/// description via DisplayAttribute and resources.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(SolutionCharacteristicDisplayNames))]
public enum SolutionCharacteristic
{
    /// <summary>
    /// Indicates that the characteristic is unknown or not specified.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(Unknown), Description = "UnknownDescription")]
    Unknown = 0,

    /// <summary>
    /// Solution with the lowest total costs per part.
    /// </summary>
    /// <remarks>Recommended when both production and material costs are available.</remarks>
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.TotalCostsPerPart, 1000)]
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestTotalCosts), Description = "LowestTotalCostsDescription")]
    LowestTotalCosts,

    /// <summary>
    /// Cuts most parts in automatic mode, reducing manual effort.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(HighestAutomationLevel), Description = "HighestAutomationLevelDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityAutomaticMode, 1000)]
    HighestAutomationLevel,

    /// <summary>
    /// Solution with the lowest material costs per part.
    /// </summary>
    /// <remarks>Recommended when production time is not a relevant factor.</remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestMaterialCosts), Description = "LowestMaterialCostsDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.MaterialCostsPerPart, 1000)]
    LowestMaterialCosts,

    /// <summary>
    /// Balanced solution across waste, offcuts, and production time.
    /// </summary>
    /// <remarks>Recommended when total costs cannot be calculated.</remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(BalancedSolution), Description = "BalancedSolutionDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePlusOffcutsArea, 1000,
        SolutionKeyFigure.ProductionTimePerPart, 500,
        SolutionKeyFigure.OffcutsTotal, 500,
        SolutionKeyFigure.WasteArea, 500
    )]
    BalancedSolution,

    /// <summary>
    /// Requires the fewest whole boards.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestWholeBoardsQuantity), Description = "LowestWholeBoardsQuantityDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.WholeBoardsRequired, 1000)]
    LowestWholeBoardsQuantity,

    /// <summary>
    /// Achieves the shortest production time.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(FastestProduction), Description = "FastestProductionDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.ProductionTime, 1000)]
    FastestProduction,

    /// <summary>
    /// Maximizes average book height across patterns.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(MaximumAverageBookHeight), Description = "MaximumAverageBookHeightDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.BookHeightAverage, 1000)]
    MaximumAverageBookHeight,

    /// <summary>
    /// Minimizes total small offcuts.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestOffcutsSmallTotalQuantity), Description = "LowestOffcutsSmallTotalQuantityDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsSmallTotal, 1000,
        SolutionKeyFigure.OffcutsSmallProduced, 100
    )]
    LowestOffcutsSmallTotalQuantity,

    /// <summary>
    /// Minimizes total offcuts.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestOffcutsTotalQuantity), Description = "LowestOffcutsTotalQuantityDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsTotal, 1000,
        SolutionKeyFigure.OffcutsProduced, 100
    )]
    LowestOffcutsTotalQuantity,

    /// <summary>
    /// Minimizes complexity by reducing recuts, headcuts, and max book weight.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestComplexity), Description = "LowestComplexityDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.Recuts, 1000,
        SolutionKeyFigure.Headcuts, 500,
        SolutionKeyFigure.BookWeightMax, 200
    )]
    LowestComplexity,

    /// <summary>
    /// Minimizes combined waste and offcuts percentage.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestWastePlusOffcutsPercentage), Description = "LowestWastePlusOffcutsPercentageDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePlusOffcutsPercentage, 1000,
        SolutionKeyFigure.WastePercentage, 100
    )]
    LowestWastePlusOffcutsPercentage,

    /// <summary>
    /// Minimizes waste percentage relative to total.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestWastePercentage), Description = "LowestWastePercentageDescription")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePercentage, 1000,
        SolutionKeyFigure.WastePlusOffcutsPercentage, 100
    )]
    LowestWastePercentage,

    /// <summary>
    /// Maximizes the number of plus parts produced.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(HighestNumberOfPlusParts), Description = "HighestNumberOfPlusPartsDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityPlusParts, 1000)]
    HighestNumberOfPlusParts,

    /// <summary>
    /// Minimizes the number of machine cycles.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestNumberOfCycles), Description = "LowestNumberOfCyclesDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.Cycles, 1000)]
    LowestNumberOfCycles,

    /// <summary>
    /// Produces patterns with similar quantities.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(SimilarPatterns), Description = "SimilarPatternsDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.QuantityPerPatternAverage, 1000)]
    SimilarPatterns,

    /// <summary>
    /// Minimizes the number of cutting patterns.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestNumberOfPatterns), Description = "LowestNumberOfPatternsDescription")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PatternCount, 1000)]
    LowestNumberOfPatterns,

    /// <summary>
    /// Indicates that the solution has no special characteristic.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(None), Description = "NoneDescription")]
    None = 99
}