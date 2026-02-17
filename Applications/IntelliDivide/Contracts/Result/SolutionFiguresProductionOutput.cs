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
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProductionOutput
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the number of cuts.
    /// </summary>
    [JsonProperty(Order = 8)]
    [Range(0, int.MaxValue)]
    public int Cuts { get; set; }

    /// <summary>
    /// Gets the value of the cutting length in m or inch.
    /// </summary>
    [JsonProperty(Order = 9)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, double.MaxValue)]
    public double CuttingLength { get; set; }

    /// <summary>
    /// Gets the quantity of cutting cycles.
    /// </summary>
    [JsonProperty(Order = 7)]
    [Range(0, int.MaxValue)]
    public int Cycles { get; set; }

    /// <summary>
    /// Gets the value of the area of the parts in m² or ft².
    /// </summary>
    [JsonProperty(Order = 3)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double PartArea { get; set; }

    /// <summary>
    /// Gets or sets the quantity of parts to use when operating in automatic mode.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public double PartsQuantityAutomaticMode { get; set; }

    /// <summary>
    /// Gets the percentage of parts to use when operating in automatic mode. This is calculated as the ratio of the quantity
    /// of parts in automatic mode to the total quantity of parts, multiplied by 100 to express it as a percentage.
    /// </summary>
    [Range(0, 100)]
    public double PartsQuantityAutomaticModePercentage
    {
        get
        {
            return Math.Round(PartsQuantityAutomaticMode / QuantityOfPartsTotal * 100, 2);
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // Required for serialization
        }
    }

    /// <summary>
    /// Gets the quantity of parts to use when operating in manual mode. This is calculated as the difference between the total
    /// quantity of parts and the quantity of parts in automatic mode.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public double PartsQuantityManualMode
    {
        get
        {
            return QuantityOfPartsTotal - PartsQuantityAutomaticMode;
        } // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // Required for serialization
        }
    }

    /// <summary>
    /// Gets the value of the area of the plus parts (optional parts) in m² or ft².
    /// </summary>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double PlusPartsArea { get; set; }

    /// <summary>
    /// Gets the estimated production time.
    /// </summary>
    [JsonProperty(Order = 5)]
    public TimeSpan ProductionTime { get; set; }

    /// <summary>
    /// Gets the average production time per part in seconds.
    /// </summary>
    [JsonProperty(Order = 6)]
    [Range(0, double.MaxValue)]
    public double ProductionTimePerPart { get; set; }

    /// <summary>
    /// Gets the quantity of parts.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Range(0, int.MaxValue)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Gets the total quantity of parts, including plus parts (optional parts).
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int QuantityOfPartsTotal
    {
        get
        {
            return QuantityOfPlusParts + QuantityOfParts;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // Required for serialization
        }
    }

    /// <summary>
    /// Gets the quantity of plus parts (optional parts).
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, int.MaxValue)]
    public int QuantityOfPlusParts { get; set; }
}