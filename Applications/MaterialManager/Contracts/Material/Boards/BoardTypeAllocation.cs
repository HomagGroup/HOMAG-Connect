#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// Represents the allocation of a board type to a production-related entity.
/// </summary>
/// <example>
/// { "boardCode": "BOARD-001", "name": "Production reservation", "quantity": 4 }
/// </example>
public class BoardTypeAllocation : Allocation
{
    /// <summary>
    /// Code of the board type reserved by this allocation.
    /// </summary>
    /// <example>BOARD-001</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeAllocationProperties_BoardCode))]
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Number of boards reserved by this allocation.
    /// </summary>
    /// <example>4</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeAllocationProperties_Quantity))]
    public int Quantity { get; set; }
}