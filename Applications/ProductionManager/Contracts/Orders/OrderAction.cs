using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Order action enumeration
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OrderAction
    {

        /// <summary>
        /// The order was released
        /// </summary>
        Release,

        /// <summary>
        /// The release of the order was reset
        /// </summary>
        ResetRelease
    }
}
