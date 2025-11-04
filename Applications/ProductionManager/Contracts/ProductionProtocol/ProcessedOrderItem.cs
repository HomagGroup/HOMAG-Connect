using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents a processed OrderItem in a production protocol, including its id, description and quantity processed.
    /// </summary>
    public class ProcessedOrderItem: ProcessedItem
    {
        /// <summary>
        /// Gets or sets the id of the processed OrderItem
        /// </summary>
        [JsonProperty(Order = 10)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the processed OrderItem
        /// </summary>
        [JsonProperty(Order = 11)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity processed.
        /// </summary>
        [JsonProperty(Order = 12)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the name of the Order in which the OrderItem was defined.
        /// </summary>
        [JsonProperty(Order = 13)]
        public string? OrderName { get; set; }

        /// <summary>
        /// Gets or sets the OrderId in which the OrderItem was defined.
        /// </summary>
        [JsonProperty(Order = 14)]
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the Customer name.
        /// </summary>
        [JsonProperty(Order = 15)]
        public string? CustomerName { get; set; }
    }
}
