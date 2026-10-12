using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.Orders;

/// <summary>
/// Order type.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(OrderTypeDisplayNames))]
public enum OrderType
{
    /// <summary>
    /// Default / unknown type (fallback if the enum is new or not known)
    /// </summary>
    Unknown,

    /// <summary>
    /// The order is a customer order. Default type of an order.
    /// </summary>
    CustomerOrder,

    /// <summary>
    /// The order is a reclaim. This type of order is used when a customer returns an item 
    /// or requests a replacement due to issues such as damage, defects, or other issues.
    /// </summary>
    ReclaimOrder
}
