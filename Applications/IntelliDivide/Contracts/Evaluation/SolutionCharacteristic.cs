using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Specifies the characteristics used to evaluate or select a solution in the order to be displayed.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(SolutionCharacteristicDisplayNames))]
public enum SolutionCharacteristic
{
    /// <summary>
    /// The solution has no special characteristic and should not be displayed.
    /// </summary>
    None,

    /// <summary>
    /// Solution with the lowest total cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if both production and material costs are known.
    /// It is generally recommended for typical scenarios.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(nameof(SolutionCandidate.TotalCostsScore), 1000)]
    LowestTotalCosts,

    /// <summary>
    /// Represents a solution that maintains a balanced state according to defined weights
    /// </summary>
    /// <remarks>
    /// This characteristic is recommended when the total costs can't get calculated.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(
        nameof(SolutionCandidate.OffcutsTotalScore), 1000,
        nameof(SolutionCandidate.MaterialCostsScore), 1000,
        nameof(SolutionCandidate.TotalCostsScore), 500)]
    BalancedSolution,

    /// <summary>
    /// Solution with the lowest material cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if the material costs are known.
    /// It is recommended when production time is not a relevant factor.
    /// </remarks>
    [SolutionCharacteristicScoreWeights(nameof(SolutionCandidate.MaterialCostsScore), 1000)]
    LowestMaterialCosts,

    /// <summary>
    /// Compromise of all key figures with stronger weighting of waste.
    /// </summary>
    [SolutionCharacteristicScoreWeights(
            nameof(SolutionCandidate.WasteScore), 1000,
            nameof(SolutionCandidate.ProductionTimeScore), 500
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
        nameof(SolutionCandidate.OffcutsTotalScore), 1000,
        nameof(SolutionCandidate.TotalCostsScore), 500
    )]
    Offcuts
}