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
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialBoardsOffcuts
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets large offcuts produced.
    /// </summary>
    [JsonProperty(Order = 10)]
    [Range(0, int.MaxValue)]
    public int OffcutsLargeProduced { get; set; }

    /// <summary>
    /// Gets large offcuts required.
    /// </summary>
    [JsonProperty(Order = 10)]
    [Range(0, int.MaxValue)]
    public int OffcutsLargeRequired { get; set; }

    /// <summary>
    /// Gets large offcuts total.
    /// </summary>
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
    /// Gets offcuts produced.
    /// </summary>
    [JsonProperty(Order = 11)]
    [Range(0, int.MaxValue)]
    public int OffcutsProduced { get; set; }

    /// <summary>
    /// Gets offcuts required.
    /// </summary>
    [JsonProperty(Order = 12)]
    [Range(0, int.MaxValue)]
    public int OffcutsRequired { get; set; }

    /// <summary>
    /// Offcuts small produced.
    /// </summary>
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
    /// Offcuts small required.
    /// </summary>
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
    /// Offcuts small total.
    /// </summary>
    [JsonProperty(Order = 9)]
    [Range(0, int.MaxValue)]
    public int OffcutsSmallTotal
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
    /// Gets the total number of offcuts.
    /// </summary>
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
    /// Gets the required board area in m² or ft².
    /// </summary>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, int.MaxValue)]
    public double RequiredBoardArea { get; set; }

    /// <summary>
    /// Gets the total percentage of waste.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Range(0, 100)]
    public double Waste { get; set; }

    /// <summary>
    /// Gets the waste area in m² or ft².
    /// </summary>
    [JsonProperty(Order = 1)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double? WasteArea { get; set; }

    /// <summary>
    /// Gets the waste area including offcuts in m² or ft².
    /// </summary>
    [JsonProperty(Order = 1)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [Range(0, double.MaxValue)]
    public double WastePlusOffcutsArea { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on board area.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, 100)]
    public double WasteWithOffcutsByBoard { get; set; }

    /// <summary>
    /// Gets the percentage of waste, including offcuts, based on parts area.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0, 100)]
    public double WasteWithOffcutsByParts { get; set; }

    /// <summary>
    /// Gets the number of whole boards used.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Range(0, int.MaxValue)]
    public int WholeBoards { get; set; }
}