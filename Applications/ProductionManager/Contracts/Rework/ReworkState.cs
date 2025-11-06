using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework State
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ReworkState
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending,

        /// <summary>
        /// Approved
        /// </summary>
        Approved,

        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,

        /// <summary>
        /// Transferred
        /// </summary>
        Transferred
    }
}