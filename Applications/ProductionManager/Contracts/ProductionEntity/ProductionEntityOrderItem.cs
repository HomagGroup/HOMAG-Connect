using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity order item.
/// </summary>
public class ProductionEntityOrderItem : ProductionEntity
{
    /// <summary>
    /// Gets or sets the order item.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? OrderItem { get; set; }

    /// <summary>
    /// Gets or sets the production entities.
    /// </summary>
    [JsonProperty(Order = 999)]
    public Collection<ProductionEntity>? ProductionEntities { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 1)]
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.OrderItem;
}