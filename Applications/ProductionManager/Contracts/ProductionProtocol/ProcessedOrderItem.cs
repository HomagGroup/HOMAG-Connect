using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents a processed order item in a production protocol, including its identifier, description,
    /// processed quantity, and order context.
    /// </summary>
    /// <example>
    /// {
    ///   "identifier": "10",
    ///   "description": "Side Panel Left",
    ///   "quantity": 4,
    ///   "orderName": "Kitchen_4711",
    ///   "orderNumber": "4711",
    ///   "customerName": "Miller"
    /// }
    /// </example>
    public class ProcessedOrderItem : ProcessedItem
    {
        /// <summary>
        /// Identifier of the processed item, for example its position number or barcode. Optional.
        /// </summary>
        /// <example>10</example>
        [JsonProperty(Order = 10)]
        [Display(AutoGenerateField = false)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Description of the processed item. Optional.
        /// </summary>
        /// <example>Side Panel Left</example>
        [JsonProperty(Order = 11)]
        [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Number of pieces processed. Optional.
        /// </summary>
        /// <example>4</example>
        [JsonProperty(Order = 12)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Quantity))]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Name of the order the item belongs to. Optional.
        /// </summary>
        /// <example>Kitchen_4711</example>
        [JsonProperty(Order = 13)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Number of the order the item belongs to. Optional.
        /// </summary>
        /// <example>4711</example>
        [JsonProperty(Order = 14)]
        [Display(AutoGenerateField = false, ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OrderNumber))]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Name of the customer of the order. Optional.
        /// </summary>
        /// <example>Miller</example>
        [JsonProperty(Order = 15)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }
    }
}
