#nullable enable
using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Event that occurs when a cycle has been completed on a dividing (Cutting or Nesting) workstation.
    /// </summary>
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionAssist.Dividing.CycleCompletedEvent", "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "optimizationId": "3f2504e0-4f89-11d3-9a0c-0305e82c3301", "patternName": "Pattern 1", "patternCycle": 3 }
    /// </example>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleCompletedEvent))]
    public class CycleCompletedEvent : WorkstationEvent
    {
        /// <summary>
        /// Unique identifier of the optimization the pattern belongs to.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public Guid? OptimizationId { get; set; }

        /// <summary>
        /// Cycle number of the pattern that was processed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 22)]
        public int? PatternCycle { get; set; }

        /// <summary>
        /// Name of the pattern that was processed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 21)]
        public string? PatternName { get; set; }
    }
}
