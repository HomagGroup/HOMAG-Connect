using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts;

/// <summary>
/// Workstation
/// </summary>
public class Workstation:ISupportsLocalizedSerialization
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
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(DisplayName))]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Group))]
    public WorkstationGroup Group { get; set; } = WorkstationGroup.None;

    /// <summary>
    /// Gets or sets the Workstation Id
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Id))]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Type))]
    public WorkstationType Type { get; set; } = WorkstationType.None;
}