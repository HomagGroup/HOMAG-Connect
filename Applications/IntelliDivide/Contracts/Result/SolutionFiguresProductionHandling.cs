using Newtonsoft.Json;
using System.Collections.Generic;

#nullable enable

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for production handling.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProductionHandling
{
    /// <summary>
    /// Gets the quantity of recuts.
    /// </summary>
    [JsonProperty(Order = 1)]
    public int Recuts { get; set; }

    /// <summary>
    /// Gets the quantity of headcuts.
    /// </summary>
    [JsonProperty(Order = 2)]
    public int HeadCuts { get; set; }

    /// <summary>
    /// Gets the average book height in mm or inch.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double AverageBookHeight { get; set; }

    /// <summary>
    /// Gets the maximum book height in mm or inch.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double MaxBookHeight { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}