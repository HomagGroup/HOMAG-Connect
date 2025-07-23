using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialAssist.Contracts.Request;

public class MaterialAssistRequestOffcutEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Gets or sets the board code to which the entity should be assigned.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the length of the newly created offcut.
    /// </summary>
    [Required]
    [Range(0.1, 9999.9)]
    public double Length { get; set; }

    /// <summary>
    /// Gets or sets the width of the newly created offcut.
    /// </summary>
    [Required]
    [Range(0.1, 9999.9)]
    public double Width { get; set; }
}