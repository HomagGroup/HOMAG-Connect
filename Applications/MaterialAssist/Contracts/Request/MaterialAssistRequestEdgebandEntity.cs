using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Request;

/// <summary>
/// Inventory values for creating an edgeband entity from an existing edgeband type.
/// </summary>
/// <remarks>
/// For <see cref="MaterialManager.Contracts.Material.Base.ManagementType.Single" />, supply a Quantity of one.
/// The service enforces this rule in addition to the local property constraints.
/// Length is required for creation; <see cref="HomagConnect.MaterialAssist.Contracts.Update.MaterialAssistUpdateEdgebandEntity" />
/// allows callers to omit it from an update.
/// </remarks>
/// <example>
/// {
/// "id": "edge-001",
/// "edgebandCode": "EDGE-WHITE-1",
/// "managementType": "Single",
/// "quantity": 1,
/// "length": 50,
/// "currentThickness": 1.2
/// }
/// </example>
public class MaterialAssistRequestEdgebandEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Optional measured thickness of this edgeband entity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0.01, 99.99)]
    public double? CurrentThickness { get; set; }

    /// <summary>
    /// Code of the existing edgeband type assigned to this inventory entity.
    /// </summary>
    /// <example>EDGE-WHITE-1</example>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Length of one edgeband coil, not the combined length represented by Quantity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m; for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    /// <remarks>
    /// This property is required when creating an edgeband entity. Updates may omit it; see
    /// <see cref="HomagConnect.MaterialAssist.Contracts.Update.MaterialAssistUpdateEdgebandEntity" />.
    /// The nullable type represents an unsupplied value, which validation rejects on creation.
    /// Explicit zero is a supplied length and remains valid.
    /// </remarks>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    [Required]
    public double? Length { get; set; }

    /// <summary>
    /// How the edgeband items are tracked: individually, as a stack, or as goods in stock.
    /// </summary>
    /// <example>Single</example>
    [Required]
    public ManagementType ManagementType { get; set; }
}