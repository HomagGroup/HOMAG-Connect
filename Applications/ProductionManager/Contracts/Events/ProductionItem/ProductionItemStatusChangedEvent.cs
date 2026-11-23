using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Applications.ProductionManager.Contracts.Events.ProductionItem
{
    /// <summary>
    /// Represents an event that is triggered when the status of a production item changes.
    /// </summary>
    [AppEvent(nameof(ProductionManager) + "." + nameof(ProductionItem) + "." + nameof(ProductionItemStatusChangedEvent))]
    public class ProductionItemStatusChangedEvent : AppEvent
    {
        /// <summary>
        /// Identifier of the production item whose status changed. Optional.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Number of items affected by the status change. Defaults to 1.
        /// </summary>
        [Required]
        [DefaultValue(1)]
        [JsonProperty(Order = 11)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Person or program that triggered the feedback for the item. Optional.
        /// </summary>
        [JsonProperty(Order = 12)]
        public string? Source { get; set; }

        /// <summary>
        /// The WorkstationId on which the status change occurred.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// The status of the ProductionItem after the change.
        /// </summary>
        [Required]
        [JsonProperty(Order = 14)]
        public ProductionItemStatus Status { get; set; }
    }
}
