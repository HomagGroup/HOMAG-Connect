using Type = HomagConnect.OrderManager.Contracts.OrderItems.Type;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Resource : Component
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Resource;
        }
    }
}