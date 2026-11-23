using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Order action that is relevant for license usage reporting. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(OrderActionDisplayNames))]
    public enum OrderAction
    {
        /// <summary>
        /// The order was released for production. Use it for the action that counts the order's parts as released.
        /// </summary>
        Release,

        /// <summary>
        /// The release of the order was withdrawn, so the order is no longer released for production.
        /// </summary>
        ResetRelease
    }
}
