using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Provides allocation values for an update. The default client serializer omits properties whose values are null.
/// </summary>
/// <example>
/// { "name": "Production reservation", "quantity": 4 }
/// </example>
public class BoardTypeAllocationUpdate
{
    /// <summary>
    /// Code of a replacement board type for the allocation.
    /// Null or an empty string leaves the current board type unchanged.
    /// </summary>

    [StringLength(50)]
    public string? BoardTypeCode { get; set; } = null;

    /// <summary>
    /// Notes about the allocation.
    /// </summary>
    public string? Comments { get; set; } = null;

    /// <summary>
    /// Name to assign to the allocation, which can differ from the name used to select the existing allocation.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Number of boards reserved by the allocation.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? Quantity { get; set; } = null;

    /// <summary>
    /// Source associated with the allocation.
    /// </summary>
    public string? Source { get; set; } = null;

    /// <summary>
    /// Workstation associated with the allocation.
    /// </summary>
    public string? Workstation { get; set; } = null;
}