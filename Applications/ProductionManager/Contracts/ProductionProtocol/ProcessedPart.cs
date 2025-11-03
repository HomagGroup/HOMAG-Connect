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
public class ProcessedPart : ProcessedItem, IDimensionProperties, IMaterialProperties
{
    /// <summary>
    /// Gets or sets the id of the processed part
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the processed part
    /// </summary>
    [JsonProperty(Order = 11)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the quantity processed.
    /// </summary>
    [JsonProperty(Order = 12)]
    public int? Quantity { get; set; }

    /// <inheritdoc />
    public override ProcessedItemType ItemType
    {
        get
        {
            return ProcessedItemType.Part;
        }
    }

    /// <inheritdoc />
    public override ProductionItemType ProductionItemType
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
    [JsonProperty(Order = 14)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 16)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 17)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    #endregion

    #region IMaterialProperties
    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    [JsonProperty(Order = 18)]
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 19)]
    public Grain Grain { get; set; }
    #endregion

    /// <summary>
    /// Gets or sets the name of the Order in which the part was defined.
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? OrderName { get; set; }

    /// <summary>
    /// Gets or sets the OrderId in which the part was defined.
    /// </summary>
    [JsonProperty(Order = 22)]
    public Guid? OrderId { get; set; }

    /// <summary>
    /// Gets or sets the name of the Customer name.
    /// </summary>
    [JsonProperty(Order = 23)]
    public string? CustomerName { get; set; }

}