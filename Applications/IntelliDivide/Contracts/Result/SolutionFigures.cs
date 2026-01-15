#nullable enable
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Represents the key figures available in a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFigures
{
    /// <summary>
    /// Gets the production key figures for the solution.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresProduction Production { get; set; } = new();

    /// <summary>
    /// Gets the material key figures for the solution.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionFiguresMaterial Material { get; set; } = new();

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}