using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Request;

/// <summary>
/// Inventory values for creating a reusable offcut from an existing board type.
/// </summary>
/// <remarks>
/// Supply a Quantity of one. The service enforces this rule in addition to the local property constraints.
/// </remarks>
/// <example>
/// {
/// "id": "offcut-001",
/// "boardCode": "BOARD-19",
/// "quantity": 1,
/// "length": 1000,
/// "width": 500
/// }
/// </example>
public class MaterialAssistRequestOffcutEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Code of the existing board type from which this offcut is created.
    /// </summary>
    /// <example>BOARD-19</example>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Length of the new offcut or template, which may differ from the parent board's length.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [Required]
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Length { get; set; }

    /// <summary>
    /// Width of the new offcut or template, which may differ from the parent board's width.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: in.</para>
    /// </summary>
    [Required]
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Width { get; set; }
}