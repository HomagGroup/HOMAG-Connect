namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// A price information.
/// </summary>
public class Price : Base
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Price;
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