using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;

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
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.TotalCosts, 1000,
        SolutionKeyFigure.MaterialCosts, 100
    )]
    [Display(ResourceType = typeof(SolutionCharacteristicDisplayNames), Name = nameof(LowestTotalCosts), Description = "LowestTotalCostsDescription")]
    LowestTotalCosts,

    /// <summary>
    /// Represents a solution that maintains a balanced state according to defined weights
    /// </summary>
    /// <remarks>
    /// This characteristic is recommended when the total costs can't get calculated.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsTotal, 1000,
        SolutionKeyFigure.MaterialCosts, 1000,
        SolutionKeyFigure.TotalCosts, 500)]
    BalancedSolution,

    /// <summary>
    /// Solution with the lowest material cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if the material costs are known.
    /// It is recommended when production time is not a relevant factor.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(SolutionKeyFigure.MaterialCosts, 1000)]
    LowestMaterialCosts,

    /// <summary>
    /// Compromise of all key figures with stronger weighting of waste.
    /// </summary>
    [SolutionCharacteristicScoreWeights(
            SolutionKeyFigure.WastePercentage, 1000,
            SolutionKeyFigure.ProductionTime, 500
        )
    ]
    LittleWaste,

    /// <summary>
    /// Gets or sets the offcuts value associated with the solution candidate.
    /// </summary>
    /// <remarks>
    /// This property is used in scoring calculations, with higher offcuts typically resulting in a
    /// lower overall score. The value may influence optimization or selection algorithms that prioritize material
    /// efficiency.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(
        SolutionKeyFigure.OffcutsTotal, 1000,
        SolutionKeyFigure.TotalCosts, 500
    )]
    Offcuts,

    /// <summary>
    /// The solution has no special characteristic and should not be displayed.
    /// </summary>
    None = 99
}