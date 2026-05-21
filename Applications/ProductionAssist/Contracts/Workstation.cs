using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts;

/// <summary>
/// Workstation
/// </summary>
public class Workstation: Base.Contracts.Workstation, ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(AssignedTapioMachineId))]
    public string AssignedTapioMachineId { get; set; }

    /// <summary>
    /// Gets or sets the display name
    /// </summary>
    [JsonProperty(Order = 2)]
    [Obsolete("This property is obsolete. Use the new Name property instead.", true)]
    public string DisplayName { get; set; }
}