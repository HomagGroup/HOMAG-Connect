using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.IntelliDivide.Contracts.Common;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of a machine as shown in the intelliDivide "Machine parameters" dialog.
/// </summary>
/// <remarks>
/// The concrete type depends on <see cref="OptimizationType" />. For <see cref="OptimizationType.Cutting" /> the value is a
/// <see cref="CuttingMachineParameters" />.
/// </remarks>
/// <example>
/// { "optimizationType": "Cutting", "unitSystem": "Metric", "general": { "maxCuttingLength": 4300 } }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(OptimizationType))]
[JsonSubtypes.KnownSubType(typeof(CuttingMachineParameters), OptimizationType.Cutting)]
public class MachineParameters : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// The <see cref="OptimizationType" /> of the machine. Determines which parameters are available.
    /// </summary>
    /// <example>Cutting</example>
    [JsonProperty(Order = 1)]
    public virtual OptimizationType OptimizationType { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 2)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}
