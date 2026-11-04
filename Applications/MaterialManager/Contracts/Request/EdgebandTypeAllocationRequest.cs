using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialManager.Contracts.Request;

/// <summary>
/// Supplies the edgeband type, order, and requested length for an edgeband allocation.
/// </summary>
/// <example>
/// { "edgebandCode": "EDGE-001", "order": "ORDER-2401", "allocatedLength": 12.5,
/// "createdBy": "planner@example.com", "workstation": "EDGETEC-01", "usedLength": 0.0 }
/// </example>
public class EdgebandTypeAllocationRequest : EdgebandTypeAllocationRequestBase
{
    /// <summary>
    /// Length of edgeband reserved by this allocation.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 99999.0)]
    public double AllocatedLength { get; set; }

    /// <summary>
    /// Optional notes about the allocation.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Name or identifier of the person or system creating the allocation.
    /// </summary>
    [Required]
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Source associated with the allocation, when provided.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Length of edgeband already used for this allocation.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double UsedLength { get; set; }

    /// <summary>
    /// Workstation associated with the allocation.
    /// </summary>
    [Required]
    public string Workstation { get; set; } = string.Empty;
}