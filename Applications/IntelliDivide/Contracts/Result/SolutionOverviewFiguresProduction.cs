#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures for production.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFiguresProduction
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the average book height in mm or inch.
    /// </summary>
    [JsonProperty(Order = 6)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.01, double.MaxValue)]
    public double? AverageBookHeight
    {
        get;
        set
        {
            field = value == 0 ? null : value;
        }
    }

    /// <summary>
    /// Gets the quantity of cutting cycles.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Range(1, int.MaxValue)]
    public int Cycles { get; set; }

    /// <summary>
    /// Gets the estimated production time.
    /// </summary>
    [JsonProperty(Order = 1)]
    public TimeSpan ProductionTime { get; set; }

    /// <summary>
    /// Gets the average production time per part in seconds.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(1, double.MaxValue)]
    public double ProductionTimePerPart { get; set; }

    /// <summary>
    /// Gets the quantity of parts.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0, int.MaxValue)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Gets the quantity of plus parts (optional parts).
    /// </summary>
    [JsonProperty(Order = 4)]
    [Range(0, int.MaxValue)]
    public int QuantityOfPlusParts { get; set; }
}