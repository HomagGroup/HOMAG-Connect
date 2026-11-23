using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.Orders;

/// <summary>
/// Type of an order. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(OrderTypeDisplayNames))]
public enum OrderType
{
    /// <summary>
    /// Type is not known (fallback for new or unrecognized values).
    /// </summary>
    Unknown,

    /// <summary>
    /// The order is a customer order. This is the default type.
    /// </summary>
    CustomerOrder,

    /// <summary>
    /// The order is a reclaim, used when a customer returns an item or requests a replacement
    /// because of damage, defects or other issues.
    /// </summary>
    ReclaimOrder
}
