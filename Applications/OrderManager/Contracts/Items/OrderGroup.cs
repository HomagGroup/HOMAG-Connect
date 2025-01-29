using System.Collections.ObjectModel;

namespace HomagConnect.OrderManager.Contracts.Items;

public class OrderGroup : ItemBase
{
    /// <inheritdoc />
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.OrderGroup;
        }
    }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the source of the group.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the items.
    /// </summary>
    public Collection<ItemBase>? Items { get; set; }
}