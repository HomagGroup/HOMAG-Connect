using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.Rework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.Rework
{
    /// <summary>
    /// Represents an event that is triggered when the status of a rework changes in the Production Manager.
    /// </summary>
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.Rework.ReworkStatusChangedEvent", "reworkId": "RW-1001", "state": "Approved", "statusChangedBy": "planner@example.com", "identifier": "PART-1001" }
    /// </example>
    [AppEvent(nameof(ProductionManager) + "." + nameof(Rework) + "." + nameof(ReworkStatusChangedEvent))]
    public class ReworkStatusChangedEvent : AppEvent
    {
        /// <summary>
        /// The rework id.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public string? ReworkId { get; set; }

        /// <summary>
        /// The current state of the rework.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        public ReworkState State { get; set; }

        /// <summary>
        /// The user or system that changed the rework status.
        /// </summary>
        [Required]
        [JsonProperty(Order = 12)]
        public string? StatusChangedBy { get; set; }

        /// <summary>
        /// The Part Identifier.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Rejection details. Only set when the rework was rejected.
        /// </summary>
        [JsonProperty(Order = 14)]
        public RejectionDetails? RejectionDetails { get; set; } = null;
    }
}
