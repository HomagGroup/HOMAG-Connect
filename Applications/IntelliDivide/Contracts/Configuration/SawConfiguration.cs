using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Common;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the saw configuration (e.g. cutting length, waste flap size).
/// </summary>
/// <example>
/// { "optimizationType": "Cutting", "parameters": { "SchnittlaengeMaximal_LS": "4300", "SchnitthoeheMaximal": "95" } }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(OptimizationType))]
[JsonSubtypes.KnownSubType(typeof(CuttingSawConfiguration), OptimizationType.Cutting)]
public class SawConfiguration
{
    /// <summary>
    /// Gets or sets the <see cref="OptimizationType" /> of the machine.
    /// </summary>
    /// <example>Cutting</example>
    [JsonProperty(Order = 1)]
    public virtual OptimizationType OptimizationType { get; set; }

    /// <summary>
    /// Gets or sets the parameters, keyed by their original (German) intelliDivide names.
    /// </summary>
    /// <example>{ "SchnittlaengeMaximal_LS": "4300", "SchnitthoeheMaximal": "95" }</example>
    [JsonProperty(Order = 2)]
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}
