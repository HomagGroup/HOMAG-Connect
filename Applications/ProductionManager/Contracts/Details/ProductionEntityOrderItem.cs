using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Details;

/// <summary>
/// Production entity order item.
/// </summary>
public class ProductionEntityOrderItem : ProductionEntity
{
    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? Description { get; set; }

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