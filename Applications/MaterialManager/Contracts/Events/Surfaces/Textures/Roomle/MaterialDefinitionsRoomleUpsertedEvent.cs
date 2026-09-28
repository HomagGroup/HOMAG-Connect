using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Surfaces.Textures.Roomle
{
    /// <summary>
    /// Event that occurs when multiple Roomle material definitions have been created or updated.
    /// </summary>
    [AppEvent(nameof(MaterialManager) + "." + nameof(Surfaces) + "." + nameof(Textures) + "." + nameof(Roomle) + "." + nameof(MaterialDefinitionsRoomleUpsertedEvent))]
    [LiveEvent]
    public class MaterialDefinitionsRoomleUpsertedEvent : AppEvent
    {
        /// <summary>
        /// Gets or sets the SAS urls pointing to the Roomle material definition files stored in blob storage together with the
        /// action performed during the upsert operation.
        /// </summary>
        [Required]
        [JsonProperty(Order = 20)]
        public IDictionary<Uri, UpsertAction> MaterialDefinitionRoomleUrls { get; set; }
    }
}
