namespace HomagConnect.OrderManager.Contracts.Items;

public class ItemPrice : OrderItemBase
{
    /// <inheritdoc cref="OrderItemBase"/>
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.Price;
        }
    }

    /// <summary>
    /// The total price of the item
    /// </summary>
    public double? TotalPrice { get; set; }

    /// <summary>
    /// The total price of one item
    /// </summary>
    public double? UnitPrice { get; set; }

    /// <summary>
    /// The currency of the price
    /// </summary>
    public string? Currency { get; set; }
}