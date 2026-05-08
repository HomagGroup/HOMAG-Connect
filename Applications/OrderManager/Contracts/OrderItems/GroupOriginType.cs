using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Specifies the origin of a group, indicating from which feature it was added to the basket.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum GroupOriginType
{
    /// <summary>
    /// Unknown group origin. Fallback value used when deserializing unsupported or future JSON values.
    /// </summary>
    Unknown,

    /// <summary>
    /// Group was added from the Quick Input list.
    /// </summary>
    QuickInput,

    /// <summary>
    /// Group was added from the Sales Order Configurator (SOC).
    /// </summary>
    Soc,
}