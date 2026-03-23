#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for production output.
/// </summary>
/// <example>
/// {
///   "quantityOfParts": 112,
///   "quantityOfPlusParts": 0,
///   "quantityOfPartsTotal": 112,
///   "partArea": 58.47,
///   "plusPartsArea": 0.0,
///   "productionTime": "02:15:16",
///   "productionTimePerPart": 72.46,
///   "cycles": 9,
///   "cuts": 150,
///   "cuttingLength": 148.95
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProductionOutput
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
    /// Gets the number of cuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>150</example>
    [JsonProperty(Order = 8)]
    [Range(0, int.MaxValue)]
    public int Cuts { get; set; }

    /// <summary>
    /// Gets the total cutting length.
    /// Unit: meters for <see cref="UnitSystem.Metric"/> and feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>148.95</example>
    [JsonProperty(Order = 9)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, double.MaxValue)]
    public double CuttingLength { get; set; }

    /// <summary>
    /// Gets the quantity of cutting cycles.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>9</example>
    [JsonProperty(Order = 7)]
    [Range(0, int.MaxValue)]
    public int Cycles { get; set; }

    /// <summary>
    /// Gets the total area of required parts.
    /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>58.47</example>
    [JsonProperty(Order = 3)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double PartArea { get; set; }

    /// <summary>
    /// Gets or sets the quantity of parts to use when operating in automatic mode.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public double PartsQuantityAutomaticMode { get; set; }

    /// <summary>
    /// Gets the percentage of parts to use when operating in automatic mode. This is calculated as the ratio of the quantity
    /// of parts in automatic mode to the total quantity of parts, multiplied by 100 to express it as a percentage.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>0.0</example>
    [JsonProperty(Order = 2)]
    [Range(0, 100)]
    public double PartsQuantityAutomaticModePercentage
    {
        get
        {
            return Math.Round(PartsQuantityAutomaticMode / QuantityOfPartsTotal * 100, 2);
        }
        private set => _ = value;
    }

    /// <summary>
    /// Gets the quantity of parts to use when operating in manual mode. This is calculated as the difference between the total
    /// quantity of parts and the quantity of parts in automatic mode.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>112.0</example>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public double PartsQuantityManualMode
    {
        get
        {
            return QuantityOfPartsTotal - PartsQuantityAutomaticMode;
        }
        private set => _ = value;
    }

    /// <summary>
    /// Gets the area of plus parts (optional parts).
    /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>0.0</example>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double PlusPartsArea { get; set; }

    /// <summary>
    /// Gets the estimated production time.
    /// Unit for metric and imperial unit systems: duration (<see cref="TimeSpan"/>).
    /// </summary>
    /// <example>02:15:16</example>
    [JsonProperty(Order = 5)]
    public TimeSpan ProductionTime { get; set; }

    /// <summary>
    /// Gets the average production time per part in seconds.
    /// Unit for metric and imperial unit systems: seconds.
    /// </summary>
    /// <example>72.46</example>
    [JsonProperty(Order = 6)]
    [Range(0, double.MaxValue)]
    public double ProductionTimePerPart { get; set; }

    /// <summary>
    /// Gets the quantity of parts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>112</example>
    [JsonProperty(Order = 1)]
    [Range(0, int.MaxValue)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Gets the total quantity of parts, including plus parts (optional parts).
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>112</example>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int QuantityOfPartsTotal
    {
        get
        {
            return QuantityOfPlusParts + QuantityOfParts;
        }
        private set => _ = value;
    }

    /// <summary>
    /// Gets the quantity of plus parts (optional parts).
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int QuantityOfPlusParts { get; set; }
}