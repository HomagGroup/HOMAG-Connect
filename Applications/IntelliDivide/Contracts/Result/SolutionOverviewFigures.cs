#nullable enable
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFigures 
{
    /// <summary>
    /// Gets the overview figures for costs.
    /// </summary>
    [JsonProperty(Order = 30)]
    public SolutionOverviewFiguresCosts Costs { get; set; } = new();

    /// <summary>
    /// Gets the overview figures for material.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionOverviewFiguresMaterial Material { get; set; } = new();

    /// <summary>
    /// Gets the overview figures for the production.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionOverviewFiguresProduction Production { get; set; } = new();

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}