using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.Rework
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

    /// <summary>
    /// Represents an event that is raised when a rework is created.
    /// </summary>
    [AppEvent(nameof(ProductionManager) + "." + nameof(Rework) + "." + nameof(ReworkCreatedEvent))]
    public class ReworkCreatedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the rework associated with this event.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public Contracts.Rework.Rework Rework { get; set; }
    }
}
