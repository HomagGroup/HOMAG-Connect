using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Common;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the machine configuration (e.g. machine model, options).
/// </summary>
/// <example>
/// { "optimizationType": "Cutting", "configurationOptions": { "conf_saegeart": "WS", "option_dreht": "1" } }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(OptimizationType))]
[JsonSubtypes.KnownSubType(typeof(CuttingMachineConfiguration), OptimizationType.Cutting)]
public class MachineConfiguration
{
    /// <summary>
    /// Gets or sets the <see cref="OptimizationType" /> of the machine.
    /// </summary>
    /// <example>Cutting</example>
    [JsonProperty(Order = 1)]
    public virtual OptimizationType OptimizationType { get; set; }

    /// <summary>
    /// Gets or sets the configuration options, keyed by their original (German) intelliDivide names.
    /// </summary>
    /// <example>{ "conf_saegeart": "WS", "option_dreht": "1" }</example>
    [JsonProperty(Order = 2)]
    public IDictionary<string, string> ConfigurationOptions { get; set; } = new Dictionary<string, string>();
}
