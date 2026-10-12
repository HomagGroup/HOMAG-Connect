using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the configuration of an MSM component (e.g. stacking station, buffer) of a FlexTec (HPS) saw.
/// </summary>
public class MsmComponentConfiguration
{
    /// <summary>
    /// Gets or sets the name of the component.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the component.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the parameters, keyed by their original (German) intelliDivide names.
    /// </summary>
    [JsonProperty(Order = 3)]
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}
