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
        /// The order can be released
        /// </summary>
        Release,

        /// <summary>
        /// The order can be reverted to be modified or abandoned
        /// </summary>
        ResetRelease
    }
}
