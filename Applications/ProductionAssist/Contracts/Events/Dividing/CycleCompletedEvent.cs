#nullable enable
using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Event that occurs when a cycle has been completed on a dividing (Cutting or Nesting) workstation.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleCompletedEvent))]
    public class CycleCompletedEvent : WorkstationEvent
    {
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