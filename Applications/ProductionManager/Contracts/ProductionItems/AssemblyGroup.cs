using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity component.
/// </summary>
public class AssemblyGroup : ProductionItemBase
{
    /// <summary>
    /// Gets or sets the production entities.
    /// </summary>
    public Collection<ProductionItemBase>? ProductionEntities { get; set; }

    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.AssemblyUnit;
}