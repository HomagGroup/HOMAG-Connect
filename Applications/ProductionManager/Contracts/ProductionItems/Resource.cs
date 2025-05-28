namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity resource.
/// </summary>
public class Resource : ProductionItemBase
{
    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.Resource;
}