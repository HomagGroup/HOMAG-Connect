using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the configuration of a machine.
/// </summary>
/// <example>
/// {
///   "machineConfiguration": { "optimizationType": "Cutting", "configurationOptions": { "conf_saegeart": "WS" } },
///   "sawMaterialConfiguration": { "optimizationType": "Cutting", "parameters": { "SaegeblattStaerke": "4.4" } },
///   "sawConfiguration": { "optimizationType": "Cutting", "parameters": { "SchnittlaengeMaximal_LS": "4300" } }
/// }
/// </example>
public class Configuration
{
    /// <summary>
    /// Gets or sets the machine configuration.
    /// </summary>
    [JsonProperty(Order = 1)]
    public MachineConfiguration MachineConfiguration { get; set; }

    /// <summary>
    /// Gets or sets the saw material configuration.
    /// </summary>
    [JsonProperty(Order = 2)]
    public SawMaterialConfiguration SawMaterialConfiguration { get; set; }

    /// <summary>
    /// Gets or sets the saw configuration.
    /// </summary>
    [JsonProperty(Order = 3)]
    public SawConfiguration SawConfiguration { get; set; }
}
