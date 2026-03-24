using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Detailed statistics about individual orders that have been released or reset
    /// </summary>
    [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(UsageDetails))]
    public class UsageDetails : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets the timestamp the order was released at for production
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Timestamp))]
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The number of the order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderNumber))]
        [JsonProperty(Order = 2)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The name of the order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(OrderName))]
        [JsonProperty(Order = 3)]
        public string OrderName { get; set; }

        /// <summary>
        /// The number of the customer of this order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerNumber))]
        [JsonProperty(Order = 4)]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(CustomerName))]
        [JsonProperty(Order = 5)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets the quantity of parts in this order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(QuantityOfParts))]
        [JsonProperty(Order = 6)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// The order was released or reset
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Action))]
        [JsonProperty(Order = 7, DefaultValueHandling = DefaultValueHandling.Include)]
        public OrderAction Action { get; set; }

        /// <summary>
        /// The user who changed the order
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(ChangedBy))]
        [JsonProperty(Order = 8)]
        public string ChangedBy { get; set; }

        /// <summary>
        /// The source
        /// </summary>
        [Display(ResourceType = typeof(UsageDetailsPropertyDisplayNames), Name = nameof(Source))]
        [JsonProperty(Order = 9)]
        public string? Source { get; set; }
    }
}
