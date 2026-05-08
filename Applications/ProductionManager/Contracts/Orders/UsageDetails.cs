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
    /// { "timestamp": "2025-03-14T08:42:15+00:00", "orderNumber": "4711", "orderName": "Kitchen Order", "customerNumber": "C-10025", "customerName": "Muster GmbH", "quantityOfParts": 84, "action": "Released", "changedBy": "planner@example.com", "source": "ProductionManager" }
    /// </example>
    [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(UsageDetails))]
    public class UsageDetails : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets the date and time when the order action was recorded.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Timestamp))]
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the business identifier of the order.
        /// </summary>
        /// <example>4711</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderNumber))]
        [JsonProperty(Order = 2)]
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name of the order.
        /// </summary>
        /// <example>Kitchen Order</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderName))]
        [JsonProperty(Order = 3)]
        public string OrderName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the business identifier of the customer assigned to the order.
        /// </summary>
        /// <example>C-10025</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerNumber))]
        [JsonProperty(Order = 4)]
        public string CustomerNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer name assigned to the order.
        /// </summary>
        /// <example>Muster GmbH</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerName))]
        [JsonProperty(Order = 5)]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of parts contained in the order.
        /// </summary>
        /// <example>84</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(QuantityOfParts))]
        [JsonProperty(Order = 6)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets or sets the order action that affected usage reporting.
        /// </summary>
        /// <example>Released</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Action))]
        [JsonProperty(Order = 7, DefaultValueHandling = DefaultValueHandling.Include)]
        public OrderAction Action { get; set; }

        /// <summary>
        /// Gets or sets the user who performed the recorded order action.
        /// </summary>
        /// <example>planner@example.com</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(ChangedBy))]
        [JsonProperty(Order = 8)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the source system that reported the order action.
        /// </summary>
        /// <example>ProductionManager</example>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Source))]
        [JsonProperty(Order = 9)]
        public string? Source { get; set; }
    }
}
