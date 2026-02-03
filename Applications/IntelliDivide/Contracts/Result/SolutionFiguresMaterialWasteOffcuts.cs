#nullable enable

using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material waste and offcuts per material code.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialWasteOffcuts
{
    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MaterialCode { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on board area.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double WasteWithOffcutsByBoard { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on parts area.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double WasteWithOffcutsByParts { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}