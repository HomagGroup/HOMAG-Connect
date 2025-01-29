namespace HomagConnect.OrderManager.Contracts.Items;

public class ItemPrice : ItemBase
{
    /// <summary>
    /// The total price of the item
    /// </summary>
    public decimal? TotalPrice { get; set; }

    /// <summary>
    /// The total price of one item
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// The currency of the price
    /// </summary>
    public string? Currency { get; set; }
}