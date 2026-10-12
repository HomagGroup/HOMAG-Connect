using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Common;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the saw material configuration (e.g. trims, offcuts, saw blade thickness).
/// </summary>
/// <example>
/// { "optimizationType": "Cutting", "parameters": { "SaegeblattStaerke": "4.4", "ResteLaenge": "300" } }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(OptimizationType))]
[JsonSubtypes.KnownSubType(typeof(CuttingSawMaterialConfiguration), OptimizationType.Cutting)]
public class SawMaterialConfiguration
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
    /// <example>{ "SaegeblattStaerke": "4.4", "ResteLaenge": "300" }</example>
    [JsonProperty(Order = 2)]
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}
