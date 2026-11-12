using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Identifies an existing edgeband allocation and supplies only the values to change.
/// Optional properties are nullable for PATCH updates; the default client serializer omits null values.
/// </summary>
/// <remarks>
/// Omitted or null optional properties are intended to leave the current values unchanged.
/// Explicit zero lengths and empty strings are supplied values, not omissions.
/// This behavior requires a service version that supports partial edgeband allocation updates.
/// </remarks>
/// <example>
/// { "allocatedLength": 12.5, "edgebandCode": "EDGE-001", "order": "ORDER-2401", "usedLength": 2.5 }
/// </example>
public class EdgebandTypeAllocationUpdate
{
    /// <summary>
    /// Replacement length reserved by this allocation; omit or set to null to keep the current length.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 99999.0)]
    public double? AllocatedLength { get; set; }

    /// <summary>
    /// Replacement notes about the allocation; omit or set to null to keep the current notes.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Replacement customer name; omit or set to null to keep the current customer.
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Code of the edgeband type used to identify the existing allocation.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Order identifier used to identify the existing allocation.
    /// </summary>
    [Required]
    public string Order { get; set; } = string.Empty;

    /// <summary>
    /// Replacement project identifier; omit or set to null to keep the current project.
    /// </summary>
    public string? Project { get; set; }

    /// <summary>
    /// Replacement used length; omit or set to null to keep the current length.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? UsedLength { get; set; }

    /// <summary>
    /// Replacement workstation; omit or set to null to keep the current workstation.
    /// </summary>
    public string? Workstation { get; set; } = null;
}