using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Kind of a price, such as total, gross, net, shipping, discount, tax or subtotal. Serialized as the member name.
/// </summary>
/// <remarks>When deserializing from JSON, unknown values are mapped to <see cref="Unknown"/> due to the tolerant enum converter.</remarks>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(PriceTypeDisplayNames))]
public enum PriceType
{
    /// <summary>
    /// Type is not known (fallback for new or unrecognized values).
    /// </summary>
    Unknown,

    /// <summary>
    /// Total amount.
    /// </summary>
    Total,

    /// <summary>
    /// Gross total amount.
    /// </summary>
    GrossTotal,

    /// <summary>
    /// Net total amount.
    /// </summary>
    NetTotal,

    /// <summary>
    /// Shipping costs.
    /// </summary>
    Shipping,

    /// <summary>
    /// Discount amount.
    /// </summary>
    Discount,

    /// <summary>
    /// Tax amount.
    /// </summary>
    Tax,

    /// <summary>
    /// Subtotal amount.
    /// </summary>
    SubTotal
}