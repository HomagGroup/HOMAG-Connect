using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands
{
    /// <summary>
    /// Event that occurs when an edgeband type has been deleted.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Boards) + "." + nameof(EdgebandTypeDeletedEvent))]
    public class EdgebandTypeDeletedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the EdgebandCode that has been deleted.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public string EdgebandCode { get; set; }
    }
}