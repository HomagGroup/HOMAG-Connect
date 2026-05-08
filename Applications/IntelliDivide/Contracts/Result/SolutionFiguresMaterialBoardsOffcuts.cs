#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material boards and offcuts.
/// </summary>
/// <example>
/// {
///   "wholeBoards": 16,
///   "requiredBoardArea": 84.723,
///   "waste": 14.02,
///   "wasteWithOffcutsByBoard": 30.99,
///   "offcutsProduced": 10,
///   "offcutsRequired": 0
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialBoardsOffcuts
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
    /// Gets the number of produced large offcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 10)]
    [Range(0, int.MaxValue)]
    public int OffcutsLargeProduced { get; set; }

    /// <summary>
    /// Gets the number of required large offcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 10)]
    [Range(0, int.MaxValue)]
    public int OffcutsLargeRequired { get; set; }

    /// <summary>
    /// Gets the total number of large offcuts.
    /// Calculated as <see cref="OffcutsLargeProduced"/> - <see cref="OffcutsLargeRequired"/>.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 10)]
    [Range(0, int.MaxValue)]
    public int OffcutsLargeTotal
    {
        get
        {
            return OffcutsLargeProduced - OffcutsLargeRequired;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // needed for deserialization
        }
    }

    /// <summary>
    /// Gets the number of produced offcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>10</example>
    [JsonProperty(Order = 11)]
    [Range(0, int.MaxValue)]
    public int OffcutsProduced { get; set; }

    /// <summary>
    /// Gets the number of required offcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 12)]
    [Range(0, int.MaxValue)]
    public int OffcutsRequired { get; set; }

    /// <summary>
    /// Gets the number of produced small offcuts.
    /// Calculated as <see cref="OffcutsProduced"/> - <see cref="OffcutsLargeProduced"/>.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>10</example>
    [JsonProperty(Order = 9)]
    [Range(0, int.MaxValue)]
    public int OffcutsSmallProduced
    {
        get
        {
            return OffcutsProduced - OffcutsLargeProduced;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // needed for deserialization
        }
    }

    /// <summary>
    /// Gets the number of required small offcuts.
    /// Calculated as <see cref="OffcutsRequired"/> - <see cref="OffcutsLargeRequired"/>.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 9)]
    [Range(0, int.MaxValue)]
    public int OffcutsSmallRequired
    {
        get
        {
            return OffcutsRequired - OffcutsLargeRequired;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // needed for deserialization
        }
    }

    /// <summary>
    /// Gets the total number of small offcuts.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 9)]
    [Range(0, int.MaxValue)]
    public int OffcutsSmallTotal
    {
        get
        {
            return OffcutsSmallProduced - OffcutsSmallRequired;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // needed for deserialization
        }
    }

    /// <summary>
    /// Gets the total number of offcuts.
    /// Calculated as <see cref="OffcutsProduced"/> - <see cref="OffcutsRequired"/>.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>10</example>
    [JsonProperty(Order = 10)]
    public int OffcutsTotal
    {
        get
        {
            return OffcutsProduced - OffcutsRequired;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // needed for deserialization
        }
    }

    /// <summary>
    /// Gets the required board area.
    /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>84.723</example>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, int.MaxValue)]
    public double RequiredBoardArea { get; set; }

    /// <summary>
    /// Gets the total percentage of waste.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>14.02</example>
    [JsonProperty(Order = 1)]
    [Range(0, 100)]
    public double Waste { get; set; }

    /// <summary>
    /// Gets the waste area.
    /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>11.88</example>
    [JsonProperty(Order = 1)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double? WasteArea { get; set; }

    /// <summary>
    /// Gets the waste area including offcuts.
    /// Unit: square meters for <see cref="UnitSystem.Metric"/> and square feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>26.26</example>
    [JsonProperty(Order = 1)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double WastePlusOffcutsArea { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on board area.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>30.99</example>
    [JsonProperty(Order = 2)]
    [Range(0, 100)]
    public double WasteWithOffcutsByBoard { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on parts area.
    /// Unit for metric and imperial unit systems: percent (%).
    /// </summary>
    /// <example>44.91</example>
    [JsonProperty(Order = 3)]
    //[Range(0, 100)]
    public double WasteWithOffcutsByParts { get; set; }

    /// <summary>
    /// Gets the number of whole boards used.
    /// Unit for metric and imperial unit systems: count.
    /// </summary>
    /// <example>16</example>
    [JsonProperty(Order = 5)]
    [Range(0, int.MaxValue)]
    public int WholeBoards { get; set; }
}