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
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(GroupName))]
    public string GroupName { get; set; }
}