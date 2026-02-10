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
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/generic.svg")]
    Unknown = 0,

    /// <summary>
    /// Solution with the lowest total costs per part.
    /// </summary>
    /// <remarks>Recommended when both production and material costs are available.</remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestTotalCosts), Description = "LowestTotalCostsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowesttotalcosts.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.TotalCostsPerPart, 1000)]
    LowestTotalCosts,

    /// <summary>
    /// Cuts most parts in automatic mode, reducing manual effort.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(HighestAutomationLevel), Description = "HighestAutomationLevelDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/highest_automation_level.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityAutomaticModePercentage, 1000)]
    HighestAutomationLevel,

    /// <summary>
    /// Solution with the lowest material costs per part.
    /// </summary>
    /// <remarks>Recommended when production time is not a relevant factor.</remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestMaterialCosts), Description = "LowestMaterialCostsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_material_costs.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.MaterialCostsPerPart, 1000)]
    LowestMaterialCosts,

    /// <summary>
    /// Balanced solution across waste, offcuts, and production time.
    /// </summary>
    /// <remarks>Recommended when total costs cannot be calculated.</remarks>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(BalancedSolution), Description = "BalancedSolutionDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/balanced_solution.svg")]
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
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/minimum_number_of_whole_boards.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.WholeBoardsRequired, 1000)]
    LowestWholeBoardsQuantity,

    /// <summary>
    /// Achieves the shortest production time.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(FastestProduction), Description = "FastestProductionDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/fastest_production.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.ProductionTime, 1000)]
    FastestProduction,

    /// <summary>
    /// Maximizes average book height across patterns.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(MaximumAverageBookHeight), Description = "MaximumAverageBookHeightDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/maximum_average_bookheight.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.BookHeightAverage, 1000)]
    MaximumAverageBookHeight,

    /// <summary>
    /// Minimizes total small offcuts.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestOffcutsSmallTotalQuantity), Description = "LowestOffcutsSmallTotalQuantityDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_total_number_of_small_offcuts.svg")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsSmallTotal, 1000,
        SolutionKeyFigure.OffcutsSmallProduced, 100
    )]
    LowestOffcutsSmallTotalQuantity,

    /// <summary>
    /// Minimizes total offcuts.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestOffcutsTotalQuantity), Description = "LowestOffcutsTotalQuantityDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_total_number_of_offcuts.svg")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsTotal, 1000,
        SolutionKeyFigure.OffcutsProduced, 100
    )]
    LowestOffcutsTotalQuantity,

    /// <summary>
    /// Minimizes complexity by reducing recuts, headcuts, and max book weight.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestComplexity), Description = "LowestComplexityDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_complexity.svg")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.Recuts, 1000,
        SolutionKeyFigure.Headcuts, 500,
        SolutionKeyFigure.BookWeightMax, 200
    )]
    LowestComplexity,

    /// <summary>
    /// Minimizes combined waste and offcuts percentage.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(MinimumWastePlusOffcuts), Description = "MinimumWastePlusOffcutsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/minimal_waste_including_offcuts.svg")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePlusOffcutsArea, 1000,
        SolutionKeyFigure.WasteArea, 100
    )]
    MinimumWastePlusOffcuts,

    /// <summary>
    /// Minimizes waste percentage relative to total.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(MinimumWaste), Description = "MinimumWasteDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/minimum_waste.svg")]
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.WastePercentage, 1000,
        SolutionKeyFigure.WastePlusOffcutsPercentage, 100
    )]
    MinimumWaste,

    /// <summary>
    /// Maximizes the number of plus parts produced.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(HighestNumberOfPlusParts), Description = "HighestNumberOfPlusPartsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/highest_number_of_plus_parts.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PartsQuantityPlusParts, 1000)]
    HighestNumberOfPlusParts,

    /// <summary>
    /// Minimizes the number of machine cycles.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestNumberOfCycles), Description = "LowestNumberOfCyclesDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_number_of_cycles.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.Cycles, 1000)]
    LowestNumberOfCycles,

    /// <summary>
    /// Produces patterns with similar quantities.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(SimilarPatterns), Description = "SimilarPatternsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/similar_patterns.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.QuantityPerPatternAverage, 1000)]
    SimilarPatterns,

    /// <summary>
    /// Minimizes the number of cutting patterns.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestNumberOfPatterns), Description = "LowestNumberOfPatternsDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/lowest_number_of_patterns.svg")]
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.PatternCount, 1000)]
    LowestNumberOfPatterns,

    /// <summary>
    /// Indicates that the solution has no special characteristic.
    /// </summary>
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(None), Description = "NoneDescription")]
    [DisplayIcon("https://core.homag.cloud/cdn/images/intellidivide/characteristics/generic.svg")]
    None = 99
}