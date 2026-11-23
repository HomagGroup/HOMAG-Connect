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
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.ProductionItem.ProductionItemStatusChangedEvent", "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "identifier": "PART-1001", "quantity": 1, "source": "operator@example.com", "status": "Completed" }
    /// </example>
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
