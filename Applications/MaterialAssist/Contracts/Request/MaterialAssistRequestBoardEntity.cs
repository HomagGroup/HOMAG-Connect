using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Request;

/// <summary>
/// Inventory values for creating a board entity from an existing board type.
/// </summary>
/// <remarks>
/// For <see cref="MaterialManager.Contracts.Material.Base.ManagementType.Single" />, supply a Quantity of one.
/// The service enforces this rule in addition to the local property constraints.
/// </remarks>
/// <example>
/// {
/// "id": "board-001",
/// "boardCode": "BOARD-19",
/// "managementType": "Single",
/// "quantity": 1
/// }
/// </example>
public class MaterialAssistRequestBoardEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Code of the existing board type assigned to this inventory entity.
    /// </summary>
    /// <example>BOARD-19</example>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// How the boards are tracked: individually, as a stack, or as goods in stock.
    /// </summary>
    /// <example>Single</example>
    [Required]
    public ManagementType ManagementType { get; set; }
}