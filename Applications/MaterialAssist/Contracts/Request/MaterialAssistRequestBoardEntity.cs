using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Request;

public class MaterialAssistRequestBoardEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Gets or sets the board code to which the entity should be assigned.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the management type.
    /// </summary>
    [Required]
    public ManagementType ManagementType { get; set; }
}