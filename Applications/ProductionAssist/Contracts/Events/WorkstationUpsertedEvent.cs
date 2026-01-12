using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts.Events;

/// <summary>
/// Base class for events that occur on a workstation that has been updated or created.
/// </summary>
[AppEvent(nameof(ProductionAssist) + "." + nameof(WorkstationUpsertedEvent))]
public class WorkstationUpsertedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the Workstation information.
    /// </summary>
    [Required]
    [JsonProperty(Order = 10)]
    public Workstation Workstation { get; set; }

    /// <summary>
    /// Gets or sets the action performed during the upsert operation.
    /// </summary>
    [Required]
    [JsonProperty(Order = 11)]
    public UpsertAction Action { get; set; } = UpsertAction.Created;
}