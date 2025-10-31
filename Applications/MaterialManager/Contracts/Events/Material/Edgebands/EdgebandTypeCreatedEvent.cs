using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Events.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands
{
    /// <summary>
    /// Event that occurs when a board entity has been created.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Boards) + "." + nameof(EdgebandTypeCreatedEvent))]
    public class EdgebandTypeCreatedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the EdgebandType that has been created.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public EdgebandType EdgebandType { get; set; }
    }
}