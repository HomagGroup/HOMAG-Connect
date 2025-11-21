using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Specifies the type of price represented in a monetary calculation, such as total, gross, net, shipping, discount,
/// tax, or subtotal amounts.
/// </summary>
/// <remarks>This enumeration is used to distinguish between different components of a price in financial or
/// e-commerce contexts. The <see cref="Unknown"/> value serves as a fallback for cases where the price type is not
/// recognized or is newly introduced. When deserializing from JSON, unknown values will be mapped to <see
/// cref="Unknown"/> due to the tolerant enum converter.</remarks>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum PriceType
{
    /// <summary>
    /// Default / unknown type (fallback if the enum is new or not known)
    /// </summary>
    Unknown,

    /// <summary>
    /// The total amount represented by this instance.
    /// </summary>
    Total,

    /// <summary>
    /// The gross total amount represented by this instance.
    /// </summary>
    GrossTotal,

    /// <summary>
    /// The net total amount represented by this instance.
    /// </summary>
    NetTotal,

    /// <summary>
    /// The shipping cost represented by this instance.
    /// </summary>
    Shipping,

    /// <summary>
    /// The discount amount represented by this instance.
    /// </summary>
    Discount,

    /// <summary>
    /// The tax amount represented by this instance.
    /// </summary>
    Tax,

    /// <summary>
    /// The subtotal amount represented by this instance.
    /// </summary>
    SubTotal
}