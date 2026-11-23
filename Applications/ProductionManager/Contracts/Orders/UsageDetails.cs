#nullable enable

using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Represents usage details for an individual order action within license usage reporting.
    /// </summary>
    /// <example>
    /// { "timestamp": "2025-03-14T08:42:15+00:00", "orderNumber": "4711", "orderName": "Kitchen Order", "customerNumber": "C-10025", "customerName": "Muster GmbH", "quantityOfParts": 84, "action": "Release", "changedBy": "planner@example.com", "source": "ProductionManager" }
    /// </example>
    [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(UsageDetails))]
    public class UsageDetails : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Date and time when the order action was recorded.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Timestamp))]
        [JsonProperty(Order = 1)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Order number of the order the action was performed on.
        /// </summary>
        /// <example>4711</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderNumber))]
        [JsonProperty(Order = 2)]
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Name of the order.
        /// </summary>
        /// <example>Kitchen Order</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderName))]
        [JsonProperty(Order = 3)]
        public string OrderName { get; set; } = string.Empty;

        /// <summary>
        /// Customer number of the customer assigned to the order. Empty when the order has none.
        /// </summary>
        /// <example>C-10025</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerNumber))]
        [JsonProperty(Order = 4)]
        public string CustomerNumber { get; set; } = string.Empty;

        /// <summary>
        /// Name of the customer assigned to the order. Empty when the order has none.
        /// </summary>
        /// <example>Muster GmbH</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerName))]
        [JsonProperty(Order = 5)]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Number of parts in the order.
        /// </summary>
        /// <example>84</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(QuantityOfParts))]
        [JsonProperty(Order = 6)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Order action that affected usage reporting.
        /// </summary>
        /// <example>Release</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Action))]
        [JsonProperty(Order = 7, DefaultValueHandling = DefaultValueHandling.Include)]
        public OrderAction Action { get; set; }

        /// <summary>
        /// User who performed the recorded order action.
        /// </summary>
        /// <example>planner@example.com</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(ChangedBy))]
        [JsonProperty(Order = 8)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>
        /// System that reported the order action. Optional.
        /// </summary>
        /// <example>ProductionManager</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Source))]
        [JsonProperty(Order = 9)]
        public string? Source { get; set; }
    }
}
