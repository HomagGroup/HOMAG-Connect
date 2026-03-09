#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material waste and offcuts per material code.
/// </summary>
/// <example>
/// {
///   "materialCode": "P2_Gold_Craft_Oak_19.0",
///   "wasteWithOffcutsByBoard": 32.41,
///   "wasteWithOffcutsByParts": 47.96
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialWasteOffcuts
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    /// <example>
    /// { "customProperty": "custom value" }
    /// </example>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    /// <example>P2_Gold_Craft_Oak_19.0</example>
    [JsonProperty(Order = 1)]
    [StringLength(50, MinimumLength = 1)]
    public string MaterialCode
    {
        get;
        set
        {
            field = value.Trimmed();
        }
    } = string.Empty;

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on board area.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>32.41</example>
    [JsonProperty(Order = 2)]
    [Range(0,100)]
    public double WasteWithOffcutsByBoard { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on parts area.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>47.96</example>
    [JsonProperty(Order = 3)]
    //[Range(0, 100)]
    public double WasteWithOffcutsByParts { get; set; }
}