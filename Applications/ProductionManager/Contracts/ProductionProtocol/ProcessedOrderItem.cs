using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Id))]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the processed OrderItem
        /// </summary>
        [JsonProperty(Order = 11)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity processed.
        /// </summary>
        [JsonProperty(Order = 12)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Quantity))]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the name of the Order in which the OrderItem was defined.
        /// </summary>
        [JsonProperty(Order = 13)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Gets or sets the OrderId in which the OrderItem was defined.
        /// </summary>
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OrderId))]
        [JsonProperty(Order = 14)]
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the Customer name.
        /// </summary>
        [JsonProperty(Order = 15)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }
    }
}
