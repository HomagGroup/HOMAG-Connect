using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// Import state
    /// </summary>
    /// <summary>The state of an order import process</summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImportState
    {
        /// <summary>
        /// Queued
        /// </summary>
        Queued,

        /// <summary>
        /// InProgress
        /// </summary>
        InProgress,

        /// <summary>
        /// Succeeded
        /// </summary>
        Succeeded,

        /// <summary>
        /// Error
        /// </summary>
        Error
    }
}
