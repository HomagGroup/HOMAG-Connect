#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures for costs.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFiguresCosts
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the costs of boards and offcuts in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0, double.MaxValue)]
    public double? CostsOfBoardsPlusOffcuts { get; set; }

    /// <summary>
    /// Gets the costs of edgebands in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0, double.MaxValue)]
    public double? CostsOfEdgebands { get; set; }

    /// <summary>
    /// Gets the total material costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Range(0, double.MaxValue)]
    public double? MaterialCosts { get; set; }

    /// <summary>
    /// Gets the average material costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, double.MaxValue)]
    public double? MaterialCostsPerPart { get; set; }

    /// <summary>
    /// Gets the production costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Range(0, double.MaxValue)]
    public double? ProductionCosts { get; set; }

    /// <summary>
    /// Gets the average production costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Range(0, double.MaxValue)]
    public double? ProductionCostsPerPart { get; set; }

    /// <summary>
    /// Gets the total costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Range(0, double.MaxValue)]
    public double? TotalCosts { get; set; }

    /// <summary>
    /// Gets the total costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 6)]
    [Range(0, double.MaxValue)]
    public double? TotalCostsPerPart { get; set; }
}