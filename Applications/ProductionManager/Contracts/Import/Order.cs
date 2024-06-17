
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// Order data
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The unique id of this order
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        ///  The name of this order
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [JsonProperty(Order = 3)]
        public string CustomerName { get; set; } = string.Empty;
        /// <summary>
        /// The date the order was created at
        /// </summary>

        [JsonProperty(Order = 4)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// The delivery date of this order
        /// </summary>

        [JsonProperty(Order = 5)]
        public DateTimeOffset? DeliveryDate { get; set; }

        /// <summary>
        /// The state of the order. Possible states are:`New` / `ReadyForProduction` / `InProduction` / `Finished` 
        /// </summary>       
        [JsonProperty(Order = 4)]
        public OrderState State { get; set; }
    }
}
