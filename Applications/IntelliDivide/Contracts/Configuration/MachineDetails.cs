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
///   },
///   "cuttingParameters": {
///     "unitSystem": "Metric",
///     "general": { "maxCuttingLength": 4300, "maxTravelProgramFence": 4300, "ripCutBladeThickness": 4.8, "crossCutBladeThickness": 4.8, "downloadOption": "Download", "productionCosts": 120 },
///     "offcuts": { "useMaterialManagerOffcuts": false, "smallOffcutsEnabled": true, "smallOffcutMinimumLength": 300, "smallOffcutMinimumWidth": 200, "smallOffcutMinimumArea": 0.1, "smallOffcutRating": 30, "largeOffcutsEnabled": true, "largeOffcutMinimumLength": 2000, "largeOffcutMinimumWidth": 800, "largeOffcutMinimumArea": 1.6, "largeOffcutRating": 80 },
///     "trimCuts": { "useMaterialManagerTrims": false, "trimFront": 15, "trimBack": 15, "trimLeft": 15, "trimRight": 15, "trimRecutsFront": 0, "trimRecutsBack": 0, "reducibleTrims": true },
///     "tensionTrims": { "useMaterialManagerTensionTrims": false, "useTensionTrims": true, "tensionTrimType": "SimpleTensionTrim", "tensionTrimWidth": 30, "tensionTrimMinDistance": 150, "tensionTrimMinCutLength": 2000, "tensionTrimSlotDistance": 20, "tensionTrimMiddleBridge": 30 },
///     "bookHeight": { "useMaterialManagerBookHeights": false, "maximumBookHeight": 95 },
///     "allocations": { "edgebandAllocations": false, "boardAllocations": true, "offcutAllocations": true }
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

    /// <summary>
    /// The machine parameters of a cutting machine, grouped like the sections of the "Machine parameters" dialog:
    /// In general, Offcuts, Trim cuts, Tension trims, Book height and Allocations.
    /// </summary>
    /// <remarks>
    /// <c>null</c> for nesting machines. A section or property is <c>null</c> (and omitted from the JSON) when the machine
    /// type, the machine capabilities or the license don't provide it in the dialog. The values don't replace
    /// <see cref="Configuration" />, which stays unchanged.
    /// </remarks>
    [JsonProperty(Order = 3)]
    public CuttingMachineParameters? CuttingParameters { get; set; }
}
