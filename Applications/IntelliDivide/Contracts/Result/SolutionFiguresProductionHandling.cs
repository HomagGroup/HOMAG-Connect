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
/// <example>
/// {
///   "recuts": 25,
///   "headCuts": 0,
///   "averageBookHeight": 26.56,
///   "maxBookHeight": 38.0,
///   "quantityPerPatternAverage": 12.44
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProductionHandling
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
    /// Gets the average book height.
    /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>26.56</example>
    [JsonProperty(Order = 3)]
    //[Range(0.01, int.MaxValue)]
    public double AverageBookHeight { get; set; }

    /// <summary>
    /// Gets the average book weight.
    /// Unit: kilograms for <see cref="UnitSystem.Metric"/> and pounds for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>18.40</example>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.Kilogram)]
    [Range(0.01, double.MaxValue)]
    public double? BookWeightAverage { get; set; }

    /// <summary>
    /// Gets the maximum book weight.
    /// Unit: kilograms for <see cref="UnitSystem.Metric"/> and pounds for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>24.10</example>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.Kilogram)]
    [Range(0.01, double.MaxValue)]
    public double? BookWeightMax { get; set; }

    /// <summary>
    /// Gets the quantity of headcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int HeadCuts { get; set; }

    /// <summary>
    /// Gets the maximum book height.
    /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>38.0</example>
    [JsonProperty(Order = 3)]
    [Range(0.01, 999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double MaxBookHeight { get; set; }

    /// <summary>
    /// Gets the quantity of patterns.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>9</example>
    [Range(1, int.MaxValue)]
    public int PatternCount { get; set; }

    /// <summary>
    /// Gets or sets the average quantity per pattern.
    /// Unit for metric and imperial unit systems: count per pattern.
    /// </summary>
    /// <example>12.44</example>
    [JsonProperty(Order = 5)]
    [Range(1, int.MaxValue)]
    public double QuantityPerPatternAverage { get; set; }

    /// <summary>
    /// Gets the quantity of recuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>25</example>
    [JsonProperty(Order = 1)]
    [Range(0, int.MaxValue)]
    public int Recuts { get; set; } 
}