namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// A resource used in production, for example hardware or a fitting.
/// </summary>
/// <example>
/// { "type": "Resource", "id": "R-1", "articleNumber": "HINGE-110", "description": "Hinge", "productionStatus": "New", "quantity": 4 }
/// </example>
public class Resource : ProductionItemBase
{
    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.Resource;
}