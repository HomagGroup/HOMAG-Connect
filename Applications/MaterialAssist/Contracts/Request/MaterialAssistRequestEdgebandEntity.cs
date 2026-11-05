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
    /// Optional length of one edgeband item, not the combined length of all items in Quantity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m; for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    public double? Length { get; set; }

    /// <summary>
    /// How the edgeband items are tracked: individually, as a stack, or as goods in stock.
    /// </summary>
    /// <example>Single</example>
    [Required]
    public ManagementType ManagementType { get; set; }
}