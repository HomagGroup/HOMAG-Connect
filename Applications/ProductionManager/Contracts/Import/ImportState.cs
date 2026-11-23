using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// State of an order import. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImportState
    {
        /// <summary>
        /// The import is waiting to be processed.
        /// </summary>
        Queued,

        /// <summary>
        /// The import is currently being processed.
        /// </summary>
        InProgress,

        /// <summary>
        /// The import finished successfully and the order was created.
        /// </summary>
        Succeeded,

        /// <summary>
        /// The import failed; see the error details.
        /// </summary>
        Error
    }
}
