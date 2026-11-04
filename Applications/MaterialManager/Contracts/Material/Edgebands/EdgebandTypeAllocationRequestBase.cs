using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// Identifies the edgeband type and order associated with an allocation.
/// </summary>
/// <example>
/// { "edgebandCode": "EDGE-001", "order": "ORDER-2401" }
/// </example>
public class EdgebandTypeAllocationRequestBase
{
    /// <summary>
    /// Name of the customer associated with the allocation, when applicable.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Code of the edgeband type associated with the allocation.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Order identifier associated with the edgeband allocation.
    /// </summary>
    [Required]
    public string Order { get; set; } = string.Empty;

    /// <summary>
    /// Project identifier associated with the allocation, when applicable.
    /// </summary>
    public string Project { get; set; } = string.Empty;
}