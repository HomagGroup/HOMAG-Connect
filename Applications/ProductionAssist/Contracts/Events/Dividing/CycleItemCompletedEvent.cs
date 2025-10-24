#nullable enable
using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Event that occurs when a part is confirmed in production Assist in the manual feedback for a dividing cycle (cutting / nesting).
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleItemCompletedEvent))]
    public class CycleItemCompletedEvent : ProductionItemCompletedEvent
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