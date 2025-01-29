using System.Collections.ObjectModel;

using Type = HomagConnect.OrderManager.Contracts.OrderItems.Type;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Group : Base
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Group;
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
    public Collection<Base>? Items { get; set; }
}