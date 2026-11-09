using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Request;

/// <summary>
/// Supplies the board type, quantity, and allocation details for a board reservation.
/// </summary>
/// <example>
/// { "boardTypeCode": "BOARD-001", "createdBy": "planner@example.com", "name": "Production reservation",
/// "quantity": 4, "source": "source", "workstation": "SAW-01" }
/// </example>
public class BoardTypeAllocationRequest
{
    /// <summary>
    /// Code of the board type to reserve.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// Optional notes about the allocation.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Name or identifier of the person or system creating the allocation.
    /// </summary>
    [Required]
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Name used to identify the allocation.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Number of boards to reserve.
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    /// <summary>
    /// Source that identifies the allocation.
    /// </summary>
    [Required]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Optional workstation associated with the allocation.
    /// </summary>
    public string? Workstation { get; set; }
}