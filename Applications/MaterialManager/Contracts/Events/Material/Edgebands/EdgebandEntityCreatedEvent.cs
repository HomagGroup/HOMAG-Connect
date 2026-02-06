using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands
{
    /// <summary>
    /// Event that occurs when a board entity has been created.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Edgebands) + "." + nameof(EdgebandEntityCreatedEvent))]
    public class EdgebandEntityCreatedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the EdgebandEntity that has been created.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public EdgebandEntity EdgebandEntity { get; set; }
    }
}