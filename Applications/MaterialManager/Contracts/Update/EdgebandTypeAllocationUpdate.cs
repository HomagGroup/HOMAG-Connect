using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Provides edgeband allocation values for an update. The default client serializer omits properties whose values are
/// null.
/// </summary>
/// <example>
/// { "edgebandCode": "EDGE-001", "order": "ORDER-2401", "allocatedLength": 12.5, "usedLength": 2.5 }
/// </example>
public class EdgebandTypeAllocationUpdate : EdgebandTypeAllocationRequestBase
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
    /// Length of edgeband already used for this allocation.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double UsedLength { get; set; }

    /// <summary>
    /// Workstation associated with the allocation.
    /// </summary>
    public string? Workstation { get; set; } = null;
}