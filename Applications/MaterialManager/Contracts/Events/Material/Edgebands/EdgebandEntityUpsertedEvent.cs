using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands
{
    /// <summary>
    /// Event that occurs when a board entity has been upserted.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Edgebands) + "." + nameof(EdgebandTypeDeletedEvent))]
    public class EdgebandEntityUpsertedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the action performed during the upsert operation.
        /// </summary>
        public UpsertAction Action { get; set; }

        /// <summary>
        /// Gets or sets the BoardEntity that has been upserted.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public EdgebandEntity EdgebandEntity { get; set; }
    }
}