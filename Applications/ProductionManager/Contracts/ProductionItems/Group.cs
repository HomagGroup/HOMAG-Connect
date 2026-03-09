using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents a group of production items.
/// </summary>
public class Group : ProductionItemBase
{
    /// <summary>
    /// Gets or sets the production entities.
    /// </summary>
    [JsonProperty(Order = 999)]
    public Collection<ProductionItemBase>? Items { get; set; }

    [JsonProperty(Order = 1)]
    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.Group;
}