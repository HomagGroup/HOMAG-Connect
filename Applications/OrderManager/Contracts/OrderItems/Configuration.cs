using Type = HomagConnect.OrderManager.Contracts.OrderItems.Type;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Configuration : OrderItems.Base
{
    /// <inheritdoc />
    public override Type Type
    {
        get
        {
            return Type.Configuration;
        }
    }
}