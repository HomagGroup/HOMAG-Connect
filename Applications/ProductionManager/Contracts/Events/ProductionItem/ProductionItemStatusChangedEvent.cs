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
        /// Gets or sets the Identifier of the ProductionItem that was completed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Gets or sets the Quantity of the ProductionItem that was completed.
        /// </summary>
        [Required]
        [DefaultValue(1)]
        [JsonProperty(Order = 11)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the Source - person or program that triggered the feedback pf the item
        /// </summary>
        [JsonProperty(Order = 12)]
        public string? Source { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationId on which the status change occurred.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        public Guid WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the status of the ProductionItem after the change.
        /// </summary>
        [Required]
        [JsonProperty(Order = 14)]
        public ProductionItemStatus Status { get; set; }
    }
}
