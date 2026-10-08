using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the details of an intelliDivide machine (the tabs of the "Machine parameters" dialog).
/// </summary>
/// <example>
/// {
///   "name": "SAWTEQ S-300",
///   "configuration": {
///     "machineConfiguration": { "optimizationType": "Cutting", "configurationOptions": { "conf_saegeart": "WS", "option_dreht": "1" }, "programFences": [ { "number": 1, "clamps": [ { "number": 1, "position": 120.5, "group": 1, "clampFingers": [ { "offset": 0, "width": 40 } ], "width": 40 } ] } ] },
///     "sawMaterialConfiguration": { "optimizationType": "Cutting", "parameters": { "SaegeblattStaerke": "4.4", "ResteLaenge": "300" } },
///     "sawConfiguration": { "optimizationType": "Cutting", "parameters": { "SchnittlaengeMaximal_LS": "4300", "SchnitthoeheMaximal": "95" } }
///   }
/// }
/// </example>
public class MachineDetails
{
    /// <summary>
    /// Gets or sets the name of the machine.
    /// </summary>
    /// <example>SAWTEQ S-300</example>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [JsonProperty(Order = 1)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the configuration of the machine.
    /// </summary>
    [JsonProperty(Order = 2)]
    public Configuration Configuration { get; set; }
}
