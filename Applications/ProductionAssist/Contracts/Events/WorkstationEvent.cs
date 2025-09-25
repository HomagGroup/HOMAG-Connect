using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Events;

/// <summary>
/// Base class for events that occur on a workstation.
/// </summary>
public abstract class WorkstationEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the Identifier of the Workstation where the event occurred.
    /// </summary>
    [Required]
    [JsonProperty(Order = 10)]
    public Guid WorkstationId { get; set; }
}