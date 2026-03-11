using HomagConnect.Base.Contracts.Enumerations;

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
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the workstation type.
    /// Example values include <c>Cutting</c>, <c>Nesting</c>, <c>Storage</c>, <c>CNC</c>, and <c>Assembly</c>.
    /// </summary>
    /// <example>Cutting</example>
    public WorkstationType WorkstationType { get; set; }
}