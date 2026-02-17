using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands
{
    /// <summary>
    /// Event that occurs when a board type has been deleted.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Edgebands) + "." + nameof(EdgebandEntityDeletedEvent))]
    public class EdgebandEntityDeletedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the BoardCode that has been deleted.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public string Id { get; set; }
    }
}