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
public class ProcessedPart : ProcessedItem, IContainsUnitSystemDependentProperties
{

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? Description { get; set; }

    /// <inheritdoc />
    public override ProcessedItemType ItemType
    {
        get
        {
            return ProcessedItemType.Part;
        }
    }

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

    /// <summary>
    /// Gets or sets the quantity processed.
    /// </summary>
    [JsonProperty(Order = 17)]
    public int? Quantity { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 18)]
    public string? MaterialCode { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 19)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

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

    /// <summary>
    /// Gets or sets the ProductionItemType such as Part, Position, Assembly, etc
    /// </summary>
    [JsonProperty(Order = 50)]
    public ProductionItemType ProductionItemType { get; set; } = ProductionItemType.Part;

}