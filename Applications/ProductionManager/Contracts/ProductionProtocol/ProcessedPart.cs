using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol, including its id, description and quantity processed.
/// </summary>
public class ProcessedPart : ProcessedOrderItem, IDimensionProperties, IMaterialProperties
{
    /// <inheritdoc />
    public override ProductionItemType ItemType
    {
        get
        {
            return ProductionItemType.Part;
        }
    }

    #region IDimensionProperties
    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 21)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 22)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 23)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 24)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    #endregion

    #region IMaterialProperties
    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    [JsonProperty(Order = 25)]
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 26)]
    public Grain Grain { get; set; }
    #endregion
    
}