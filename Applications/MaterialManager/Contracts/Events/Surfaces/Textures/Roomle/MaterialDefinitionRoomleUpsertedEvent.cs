using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Surfaces.Textures.Roomle
{
    /// <summary>
    /// Event that occurs when a single Roomle material definition has been created or updated.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Surfaces) + "." + nameof(Textures) + "." + nameof(Roomle) + "." + nameof(MaterialDefinitionRoomleUpsertedEvent))]
    [LiveEvent]
    public class MaterialDefinitionRoomleUpsertedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the action performed during the upsert operation.
        /// </summary>
        [JsonProperty(Order = 19)]
        public UpsertAction Action { get; set; }

        /// <summary>
        /// Gets or sets the SAS url pointing to the Roomle material definition file stored in blob storage.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public Uri MaterialDefinitionRoomleUrl { get; set; }
    }
}
