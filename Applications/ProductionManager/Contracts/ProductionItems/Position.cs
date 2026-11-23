using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// A position (order item) of an order, for example a piece of furniture, which contains the production items it consists of.
/// </summary>
/// <example>
/// { "type": "Position", "id": "1", "description": "Base cabinet", "productionStatus": "New", "quantity": 2, "items": [] }
/// </example>
public class Position : ProductionItemBase
{
    /// <summary>
    /// Production items contained in the position. Optional.
    /// </summary>
    [JsonProperty(Order = 999)]
    public Collection<ProductionItemBase>? Items { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 1)]
    public override ProductionItemType Type { get; set; } = ProductionItemType.Position;
}