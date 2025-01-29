namespace HomagConnect.OrderManager.Contracts.Items;

public class OrderItem : OrderItemBase
{
    public string Position { get; set; }

    /// <inheritdoc cref="OrderItemBase"/>
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.OrderItem;
        }
    } 
}