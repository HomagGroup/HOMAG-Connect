using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Import
{
    /// <summary>
    /// State of an order import process. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImportState
    {
        /// <summary>
        /// The import is waiting to be processed.        /// </summary>
        Queued,

        /// <summary>
        /// The import is currently being processed.        /// </summary>
        InProgress,

        /// <summary>
        /// The import finished successfully and the order was created.        /// </summary>
        Succeeded,

        /// <summary>
        /// The import failed. See the error details of the response.        /// </summary>
        Error
    }
}
