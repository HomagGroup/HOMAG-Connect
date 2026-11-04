using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands;

/// <summary>
/// Reports creation or modification of a physical edgeband inventory entry.
/// </summary>
/// <example>
/// { "id": "7848ee7c-2204-4637-88b6-6bb01e2cb8c0", "timestamp": "2025-04-09T08:15:00+00:00",
/// "subscriptionId": "27645095-1768-41cd-a692-294e43e9f4ab",
/// "key": "MaterialManager.Material.Edgebands.EdgebandEntityUpsertedEvent", "action": "Created",
/// "edgebandEntity": { "id": "edge-001", "edgebandType": { "edgebandCode": "EDGE-001",
/// "height": 23.0, "thickness": 0.8, "defaultLength": 100.0, "materialCategory": "ABS",
/// "process": "HotmeltGlue", "unitSystem": "Metric" }, "currentThickness": 0.8,
/// "length": 100.0, "totalLength": 100.0, "quantity": 1, "managementType": "Single",
/// "location": { "locationId": "rack-a-01" } } }
/// </example>
[AppEvent(nameof(MaterialManager) + "." + nameof(Material) + "." + nameof(Edgebands) + "." + nameof(EdgebandEntityUpsertedEvent))]
public class EdgebandEntityUpsertedEvent : AppEvent
{
    /// <summary>
    /// Whether the inventory entry was created or updated.
    /// </summary>
    [JsonProperty(Order = 19)]
    public UpsertAction Action { get; set; }

    /// <summary>
    /// Edgeband inventory entry after the creation or update.
    /// </summary>
    [Required]
    [JsonProperty(Order = 20)]
    public EdgebandEntity EdgebandEntity { get; set; }
}