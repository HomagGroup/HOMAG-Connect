using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed board in a production protocol, including its material code, dimensions and unit system.
/// </summary>
public class ProcessedBoard : ProcessedItem, IDimensionProperties, IMaterialProperties
{
    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the board type.
    /// </summary>
    [JsonProperty(Order = 11)]
    public BoardTypeType? BoardType { get; set; } = BoardTypeType.Board;
    
    /// <inheritdoc />
    public override ProcessedItemType ItemType
    {
        get
        {
            return ProcessedItemType.Board;
        }
    }

    #region IMaterialProperties
    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 12)]
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 13)]
    public Grain Grain { get; set; } = Grain.None;
    #endregion 

    /// <summary>
    /// Gets or sets number of boards which have been processed together.
    /// </summary>
    [JsonProperty(Order = 14)]
    public int? Quantity { get; set; } = 1;

    #region IDimensionProperties
    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 16)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 17)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 18)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    #endregion
}