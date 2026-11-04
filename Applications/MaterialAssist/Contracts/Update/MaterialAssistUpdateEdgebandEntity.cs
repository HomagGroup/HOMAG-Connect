using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Update;

/// <summary>
/// Changes to an existing edgeband entity; omitted or null properties leave their current values unchanged.
/// </summary>
/// <example>
/// { "currentThickness": 1.2, "length": 45, "comments": "Measured after use." }
/// </example>
public class MaterialAssistUpdateEdgebandEntity
{
    /// <summary>
    /// Replacement notes for the edgeband entity; omit to keep the existing notes.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; } = null;

    /// <summary>
    /// Replacement measured thickness of the edgeband entity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0.01, 99.99)]
    public double? CurrentThickness { get; set; } = null;

    /// <summary>
    /// New identifier for the edgeband entity; omit to keep its existing identifier.
    /// </summary>
    /// <example>edge-002</example>
    [StringLength(50)]
    public string? Id { get; set; } = null;

    /// <summary>
    /// Replacement length of one edgeband item, not the combined length of all items in Quantity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m; for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    public double? Length { get; set; } = null;

    /// <summary>
    /// Replacement number of edgeband items represented by this inventory entity.
    /// </summary>
    [Range(1, 9999)]
    public int? Quantity { get; set; } = null;
}