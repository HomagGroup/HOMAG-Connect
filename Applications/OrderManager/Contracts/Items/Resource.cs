namespace HomagConnect.OrderManager.Contracts.Items;

public class Resource : Component
{
    /// <inheritdoc cref="OrderItemBase"/>
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.Resource;
        }
    }
}