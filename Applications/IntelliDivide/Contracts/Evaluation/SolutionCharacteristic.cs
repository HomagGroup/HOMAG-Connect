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
    LowestTotalCosts,

    /// <summary>
    /// Solution with the lowest material cost among all evaluated options.
    /// </summary>
    /// <remarks>
    /// This characteristic is available only if the material costs are known.
    /// It is recommended when production time is not a relevant factor.
    /// </remarks>
    LowestMaterialCosts
}