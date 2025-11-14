#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Event that occurs when a cycle has been started on a dividing (Cutting or Nesting) workstation.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleStartedEvent))]
    public class CycleStartedEvent : WorkstationEvent
    {
        /// <summary>
        /// Gets or sets the Identifier of the BoardEntity that is currently being processed.
        /// </summary>
        [JsonProperty(Order = 30)]
        public string? BoardEntityId { get; set; }

        /// <summary>
        /// Gets or sets BoardCode used for the current cycle
        /// </summary>
        [JsonProperty(Order = 31)]
        public string? BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the Identifier of the Optimization that was processed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public Guid? OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the Cycle number of the Pattern that was processed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 22)]
        public int? PatternCycle { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Pattern that was processed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 21)]
        public string? PatternName { get; set; }
    }
}