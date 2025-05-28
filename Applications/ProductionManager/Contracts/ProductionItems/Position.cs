using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Order item position.
/// </summary>
public class Position : ProductionItemBase
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
    public Collection<ProductionItemBase>? ProductionEntities { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 1)]
    public override ProductionItemType Type { get; set; } = ProductionItemType.Position;
}