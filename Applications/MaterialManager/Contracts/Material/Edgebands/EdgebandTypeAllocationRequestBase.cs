using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// </summary>
public class EdgebandTypeAllocationRequestBase
{
    /// <summary>
    /// Customer of the edgeband type allocation.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the allocation edgeband code.
    /// </summary>
    [Required]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Order of the edgeband type allocation.
    /// </summary>
    [Required]
    public string Order { get; set; } = string.Empty;

    /// <summary>
    /// Project of the edgeband type allocation.
    /// </summary>
    public string Project { get; set; } = string.Empty;
}