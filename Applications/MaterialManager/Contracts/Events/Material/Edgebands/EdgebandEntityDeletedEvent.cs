using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands;

/// <summary>
/// Reports deletion of a physical edgeband inventory entry, not deletion of its edgeband type.
/// </summary>
/// <example>
/// { "id": "7848ee7c-2204-4637-88b6-6bb01e2cb8c0", "timestamp": "2025-04-09T08:15:00+00:00",
/// "subscriptionId": "27645095-1768-41cd-a692-294e43e9f4ab",
/// "key": "MaterialManager.Material.Edgebands.EdgebandEntityDeletedEvent", "edgebandEntityId": "edge-001" }
/// </example>
[AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Edgebands) + "." + nameof(EdgebandEntityDeletedEvent))]
public class EdgebandEntityDeletedEvent : AppEvent
{
    /// <summary>
    /// Identifier of the deleted edgeband inventory entry, not its edgeband type code.
    /// </summary>
    [Required]
    [JsonProperty(Order = 20)]
    public string EdgebandEntityId { get; set; }
}