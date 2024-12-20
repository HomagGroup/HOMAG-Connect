using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    public class UsageDetails
    {
        /// <summary>
        /// Gets the timestamp the order was released at for production
        /// </summary>
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The number of the order
        /// </summary>
        [JsonProperty(Order = 2)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The name of the order
        /// </summary>
        [JsonProperty(Order = 3)]
        public string OrderName { get; set; }

        /// <summary>
        /// The number of the customer of this order
        /// </summary>
        [JsonProperty(Order = 4)]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [JsonProperty(Order = 5)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets the quantity of parts in this order
        /// </summary>
        [JsonProperty(Order = 6)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// The order was released or reset
        /// </summary>
        [JsonProperty(Order = 7)]
        public OrderAction Action { get; set; }

        /// <summary>
        /// The user who changed the order
        /// </summary>
        [JsonProperty(Order = 8)]
        public string ChangedBy { get; set; }
    }
}
