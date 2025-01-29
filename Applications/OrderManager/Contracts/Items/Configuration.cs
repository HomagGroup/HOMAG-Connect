namespace HomagConnect.OrderManager.Contracts.Items;

public class Configuration : ItemBase
{
    /// <inheritdoc />
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.Configuration;
        }
    }
}