#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material waste and offcuts per material code.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialWasteOffcuts
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
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
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0,100)]
    public double WasteWithOffcutsByBoard { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on parts area.
    /// </summary>
    [JsonProperty(Order = 3)]
    //[Range(0, 100)]
    public double WasteWithOffcutsByParts { get; set; }
}