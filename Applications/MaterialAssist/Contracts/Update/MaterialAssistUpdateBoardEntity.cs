using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Update;

/// <summary>
/// Changes to an existing board entity; omitted or null properties leave their current values unchanged.
/// </summary>
/// <example>
/// { "length": 1000, "width": 500, "comments": "Trimmed to measured dimensions." }
/// </example>
public class MaterialAssistUpdateBoardEntity
{
    /// <summary>
    /// Replacement notes for the board entity; omit to keep the existing notes.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; }

    /// <summary>
    /// New identifier for the board entity; omit to keep its existing identifier.
    /// </summary>
    /// <example>board-002</example>
    [StringLength(50)]
    public string? Id { get; set; } = null;

    /// <summary>
    /// Replacement length, supported only for individually managed boards.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Length { get; set; }

    /// <summary>
    /// Replacement item count, supported only for stacks or goods in stock.
    /// </summary>
    [Range(1, 9999)]
    public int? Quantity { get; set; }

    /// <summary>
    /// Replacement width, supported only for individually managed boards.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Width { get; set; }
}