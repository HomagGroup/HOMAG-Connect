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
    [AppEvent(nameof(ProductionManager) + "." + nameof(Rework) + "." + nameof(ReworkStatusChangedEvent))]
    public class ReworkStatusChangedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the rework id.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public string? ReworkId { get; set; }

        /// <summary>
        /// Gets or sets the current state of the rework.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        public ReworkState State { get; set; }

        /// <summary>
        /// Gets or sets the user or system that changed the rework status.
        /// </summary>
        [Required]
        [JsonProperty(Order = 12)]
        public string? StatusChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the Part Identifier.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        public string? Identifier { get; set; }

        /// <summary>
        /// Rejection details if available
        /// </summary>
        [Required]
        [JsonProperty(Order = 14)]
        public RejectionDetails? RejectionDetails { get; set; } = null;
    }
}
