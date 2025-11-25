using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Order Status Enumeration
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OrderStatus
    {
        /// <summary>
        /// After a successful import of a customer order, it receives the state "New".
        /// </summary>
        New,

        /// <summary>
        /// After an order has been release it receives the state "ReadyForProduction".
        /// </summary>
        ReadyForProduction,

        /// <summary>
        /// As soon as a production order of a order is in production.
        /// </summary>
        InProduction,

        /// <summary>
        /// As soon as all production orders have been completed, the order receives the state "Completed".
        /// </summary>
        Completed,

        /// <summary>
        /// The order has been archived.
        /// </summary>
        Archived,

        [Obsolete("Use Completed instead", true)]
        Finished,

        /// <summary>
        /// After a successful import of a customer order, but not all blob operations are finished it receives the state "New (in
        /// calculation)
        /// </summary>
        NewCalculating
    }
}