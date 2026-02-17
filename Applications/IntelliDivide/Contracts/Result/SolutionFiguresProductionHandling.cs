#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for production handling.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProductionHandling
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
    [JsonProperty(Order = 3)]
    //[Range(0.01, int.MaxValue)]
    public double AverageBookHeight { get; set; }

    /// <summary>
    /// Gets the average book weight in kg or lbs.
    /// </summary>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
    [Range(0.01, double.MaxValue)]
    public double? BookWeightAverage { get; set; }

    /// <summary>
    /// Gets the average book weight in kg or lbs.
    /// </summary>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
    [Range(0.01, double.MaxValue)]
    public double? BookWeightMax { get; set; }

    /// <summary>
    /// Gets the quantity of headcuts.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int HeadCuts { get; set; }

    /// <summary>
    /// Gets the maximum book height in mm or inch.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0.01, 999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double MaxBookHeight { get; set; }

    /// <summary>
    /// Gets the quantity of patterns.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PatternCount { get; set; }

    /// <summary>
    /// Gets or sets the average quantity per pattern.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Range(1, int.MaxValue)]
    public double QuantityPerPatternAverage { get; set; }

    /// <summary>
    /// Gets the quantity of recuts.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Range(0, int.MaxValue)]
    public int Recuts { get; set; } 
}