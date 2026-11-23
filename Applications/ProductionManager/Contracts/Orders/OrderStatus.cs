using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Production status of an order, from import through production to archiving. Serialized as the member name.
            /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OrderStatus
    {
        /// <summary>
        /// The customer order was imported successfully and has not been released yet.
        /// </summary>
        New,

        /// <summary>
        /// The order has been released and is ready for production.
        /// </summary>
        ReadyForProduction,

        /// <summary>
        /// At least one production order of the order is in production.
        /// </summary>
        InProduction,

        /// <summary>
        /// All production orders of the order have been completed.
        /// </summary>
        Completed,

        /// <summary>
        /// The order has been archived.
        /// </summary>
        Archived,

        [Obsolete("Use Completed instead", true)]
        Finished,

        /// <summary>
        /// The customer order was imported successfully, but background calculations are not finished yet ("New (in calculation)").
        /// </summary>
        NewCalculating
    }
}