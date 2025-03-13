using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Request;

public class MaterialAssistRequestBoardEntity
{
    /// <summary>
    /// Gets or sets the board code to which the entity should be assigned.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the additional comments.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the custom id.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the management type.
    /// </summary>
    [Required]
    public ManagementType ManagementType { get; set; }

    /// <summary>
    /// Gets or sets the quantity code.
    /// </summary>
    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }
}