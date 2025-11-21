using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

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
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// The total price of the item
    /// </summary>
    [JsonProperty(Order = 2)]
    public double? TotalPrice { get; set; }

    /// <summary>
    /// The total price of one item
    /// </summary>
    [JsonProperty(Order = 1)]
    public double? UnitPrice { get; set; }

    /// <summary>
    /// The currency of the price
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? Currency { get; set; } = "EUR";

    /// <summary>
    /// An optional type of the price
    /// </summary>
    /// <example>
    /// Total, GrossTotal, NetTotal, Shipping, Discount, Tax, SubTotal, etc.
    /// </example>
    public string? PriceType { get; set; }

    /// <summary>
    /// The article which is used for calculating the price
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? SalesArticleNumber { get; set; }
}