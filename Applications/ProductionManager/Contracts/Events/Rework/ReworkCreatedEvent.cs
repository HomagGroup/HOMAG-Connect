using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.Rework
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

    /// <summary>
    /// Event that occurs when a rework has been created.
    /// </summary>
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.Rework.ReworkCreatedEvent",
    /// "rework": { "id": "PART-1001", "description": "Visible edge damage", "orderName": "Kitchen Order", "material": "P2_Gold_Craft_Oak_19.0", "quantityOfReworks": 2, "category": "Edgebanding", "capturedAt": "2025-03-14T08:42:15+00:00" } }
    /// </example>
    [AppEvent(nameof(ProductionManager) + "." + nameof(Rework) + "." + nameof(ReworkCreatedEvent))]
    public class ReworkCreatedEvent : AppEvent
    {
        /// <summary>
        /// The rework that was created.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public Contracts.Rework.Rework Rework { get; set; }
    }
}
