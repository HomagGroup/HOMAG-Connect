using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// A group of production items, for example items that are produced or handled together.
/// </summary>
/// <example>
/// { "type": "Group", "id": "G-01", "description": "Base cabinets", "productionStatus": "New", "quantity": 1, "items": [] }
/// </example>
public class Group : ProductionItemBase
{
    /// <summary>
    /// Production items contained in the group. Optional.
    /// </summary>
    [JsonProperty(Order = 999)]
    public Collection<ProductionItemBase>? Items { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 1)]
    public override ProductionItemType Type { get; set; } = ProductionItemType.Group;
}