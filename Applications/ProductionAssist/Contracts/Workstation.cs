using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionAssist.Contracts;

/// <summary>
/// Workstation of productionAssist, including its assigned tapio machine and group.
/// </summary>
/// <example>
/// { "id": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "name": "productionAssist Cutting", "type": "Cutting", "group": "Dividing", "assignedTapioMachineId": "1-001-01-0001", "groupName": "Saw group 1" }
/// </example>
public class Workstation: Base.Contracts.Workstation, ISupportsLocalizedSerialization
{
    /// <summary>
    /// Identifier of the tapio machine assigned to the workstation.
    /// </summary>
    /// <example>1-001-01-0001</example>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(AssignedTapioMachineId))]
    public string AssignedTapioMachineId { get; set; }

    /// <summary>
    /// Name of the group the workstation belongs to.
    /// </summary>
    /// <example>Saw group 1</example>
    [JsonProperty(Order = 21)]
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(GroupName))]
    public string GroupName { get; set; }
}