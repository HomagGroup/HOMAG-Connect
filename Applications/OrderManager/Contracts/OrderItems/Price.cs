using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Price information of an order item.
/// </summary>
/// <example>
/// { "type": "Price", "id": "PR1", "unitPrice": 625.0, "priceType": "NetTotal", "totalPrice": 1250.0, "currency": "EUR" }
/// </example>
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
    /// Total price of the item. Optional.
    /// </summary>
    /// <example>1250.0</example>
    [JsonProperty(Order = 2)]
    public double? TotalPrice { get; set; }

    /// <summary>
    /// Price of one unit of the item. Optional.
    /// </summary>
    /// <example>625.0</example>
    [JsonProperty(Order = 1)]
    public double? UnitPrice { get; set; }

    /// <summary>
    /// ISO 4217 currency code of the price. Defaults to <c>EUR</c>.
    /// </summary>
    /// <example>EUR</example>
    [JsonProperty(Order = 3)]
    public string? Currency { get; set; } = "EUR";

    /// <summary>
    /// Kind of the price, for example a net total or a discount. Optional.
    /// </summary>
    /// <example>NetTotal</example>
    [JsonProperty(Order = 1)]
    public PriceType? PriceType { get; set; }
}