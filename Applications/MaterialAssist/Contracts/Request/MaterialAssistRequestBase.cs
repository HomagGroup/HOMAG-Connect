using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialAssist.Contracts.Request;

/// <summary>
/// Shared inventory values for creating a board, offcut, template, or edgeband entity.
/// </summary>
/// <example>
/// { "id": "item-001", "quantity": 1, "comments": "Reserved for cutting." }
/// </example>
public class MaterialAssistRequestBase
{
    /// <summary>
    /// Optional notes about this inventory entity.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; }

    /// <summary>
    /// Identifier assigned to the new inventory entity, distinct from its board or edgeband type code.
    /// </summary>
    /// <example>item-001</example>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Id { get; set; }

    /// <summary>
    /// Number of items represented by this inventory entity.
    /// The service also checks the quantity against the entity kind and management type.
    /// </summary>
    [Required]
    [Range(1, 9999)]
    public int Quantity { get; set; }
}