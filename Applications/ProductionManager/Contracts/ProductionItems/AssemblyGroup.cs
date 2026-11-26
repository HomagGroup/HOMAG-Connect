using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// A group of production items that are assembled together.
/// </summary>
/// <example>
/// { "type": "AssemblyGroup", "id": "AG-1", "description": "Cabinet carcass", "productionStatus": "New", "quantity": 1, "items": [] }
/// </example>
public class AssemblyGroup : ProductionItemBase
{
    /// <summary>
    /// Production items that are assembled in the group. Optional.
    /// </summary>
    public Collection<ProductionItemBase>? Items { get; set; }

    /// <inheritdoc />
    public override ProductionItemType Type { get; set; } = ProductionItemType.AssemblyGroup;
}