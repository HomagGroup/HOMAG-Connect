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
    public Collection<ProductionItemBase>? Items { get; set; }

    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.Group;
}