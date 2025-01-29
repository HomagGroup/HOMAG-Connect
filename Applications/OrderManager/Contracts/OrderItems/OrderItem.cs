using Type = HomagConnect.OrderManager.Contracts.OrderItems.Type;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Position : Base
{
    public string Name { get; set; }

    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Position;
        }
    }
}