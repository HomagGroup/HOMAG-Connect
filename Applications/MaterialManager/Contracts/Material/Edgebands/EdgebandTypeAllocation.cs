using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// A production reservation for an edgeband type, including the reserved length and the length already used.
/// </summary>
/// <example>
/// { "name": "ORDER-2025-001", "edgebandCode": "EDGE-001", "allocatedLength": 100.0,
/// "usedLength": 25.0, "customer": "Example customer", "order": "ORDER-2025-001",
/// "project": "Kitchen", "unitSystem": "Metric" }
/// </example>
public class EdgebandTypeAllocation : Allocation, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Length reserved for production.
    /// <para>
    /// Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m; for
    /// <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.
    /// </para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double AllocatedLength { get; set; }

    /// <summary>
    /// Customer associated with the reservation.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Code identifying the edgeband type reserved for production.
    /// </summary>
    [Required]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Order associated with the reservation.
    /// </summary>
    public string Order { get; set; } = string.Empty;

    /// <summary>
    /// Project associated with the reservation.
    /// </summary>
    public string Project { get; set; } = string.Empty;

    /// <summary>
    /// Length already used for this reservation.
    /// <para>
    /// Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m; for
    /// <see cref="UnitSystem.Imperial" />: ft.
    /// </para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double UsedLength { get; set; }

    /// <summary>
    /// Unit system used for the reserved and used lengths.
    /// </summary>
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}