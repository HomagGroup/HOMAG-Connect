using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Represents a workstation in the HOMAG Connect system.
/// </summary>
/// <example>
/// { "id": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "name": "productionAssist Cutting", "workstationType": "Cutting" }
/// </example>
public class Workstation
{
    /// <summary>
    /// Gets or sets the unique identifier of the workstation.
    /// </summary>
    /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the display name of the workstation.
    /// </summary>
    /// <example>productionAssist Cutting</example>
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Name))]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the workstation type.
    /// Example values include <c>Cutting</c>, <c>Nesting</c>, <c>Storage</c>, <c>CNC</c>, and <c>Assembly</c>.
    /// </summary>
    /// <example>Cutting</example>
    [Obsolete("This property is obsolete. Use the new Type property instead.", true)]
    public WorkstationType WorkstationType { get; set; }

    /// <summary>
    /// Gets or sets the workstation type.
    /// Example values include <c>Cutting</c>, <c>Nesting</c>, <c>Storage</c>, <c>CNC</c>, and <c>Assembly</c>.
    /// </summary>
    /// <example>Cutting</example>
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Type))]
    public WorkstationType Type { get; set; }

    /// <summary>
    /// Gets or sets the workstation group.
    /// Example values include <c>Dividing</c>, <c>Edgebanding</c>, <c>CNC</c>, and <c>Assembly</c>.
    /// </summary>
    /// <example>Dividing</example>
    [Display(ResourceType = typeof(WorkstationPropertyDisplayNames), Name = nameof(Group))]
    public WorkstationGroup Group { get; set; }
}