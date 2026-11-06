using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.MaterialAssist.Contracts.Update;

/// <summary>
/// Changes to an existing board entity; omitted or null properties leave their current values unchanged.
/// </summary>
/// <remarks>
/// <para><strong>Dimension Updates</strong>: Length and Width inherit from <see cref="IDimensionPropertiesLengthWidth" />,
/// which documents the complete validation rules. In update operations (PATCH semantics), both are optional: omit or set to null
/// to leave current values unchanged. When provided, they must satisfy the range constraint (0.1 - 19999.9 mm).</para>
/// <para><strong>Supported Operations</strong>: Dimension changes are only supported for individually managed boards
/// (MaterialManagementType.Single).</para>
/// </remarks>
/// <example>
/// { "length": 1000, "width": 500, "comments": "Trimmed to measured dimensions." }
/// </example>
public class MaterialAssistUpdateBoardEntity : IDimensionPropertiesLengthWidth
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
    /// Replacement length of the board in millimetres; omit to leave unchanged. When provided, must be between 0.1 and 19999.9 mm.
    /// See <see cref="IDimensionPropertiesLengthWidth"/> for complete documentation.
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Replacement item count, supported only for stacks or goods in stock.
    /// </summary>
    [Range(1, 9999)]
    public int? Quantity { get; set; }

    /// <summary>
    /// Replacement width of the board in millimetres; omit to leave unchanged. When provided, must be between 0.1 and 19999.9 mm.
    /// See <see cref="IDimensionPropertiesLengthWidth"/> for complete documentation.
    /// </summary>
    public double? Width { get; set; }
}