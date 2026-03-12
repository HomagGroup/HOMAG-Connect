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
        /// Gets or sets the identifier of the processed order item.
        /// </summary>
        /// <example>10</example>
        [JsonProperty(Order = 10)]
        [Display(AutoGenerateField = false)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Gets or sets the deprecated identifier alias.
        /// Use <see cref="Identifier" /> instead.
        /// </summary>
        /// <example>10</example>
        [Obsolete("Replaced by Identifier")]
        public string? Id
        {
            get => Identifier;
            set => Identifier = value;
        }

        /// <summary>
        /// Gets or sets the description of the processed order item.
        /// </summary>
        /// <example>Side Panel Left</example>
        [JsonProperty(Order = 11)]
        [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity processed.
        /// </summary>
        /// <example>4</example>
        [JsonProperty(Order = 12)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Quantity))]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the name of the order in which the order item was defined.
        /// </summary>
        /// <example>Kitchen_4711</example>
        [JsonProperty(Order = 13)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Gets or sets the order number in which the order item was defined.
        /// </summary>
        /// <example>4711</example>
        [JsonProperty(Order = 14)]
        [Display(AutoGenerateField = false)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the deprecated order identifier.
        /// Use <see cref="OrderNumber" /> instead.
        /// </summary>
        /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
        [Obsolete("Use OrderNumber instead.")]
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        /// <example>Miller</example>
        [JsonProperty(Order = 15)]
        [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }
    }
}
