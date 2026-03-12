using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines the laminating-related properties of a part.
/// Includes laminate material codes and laminate grain direction for the top and bottom faces.
/// </summary>
public interface ILaminatingProperties
{
    /// <summary>
    /// Gets or sets the material code of the laminate applied on the bottom face.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>Laminate_White_Matt</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottom))]
    [StringLength(50, MinimumLength = 1)]
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// Gets or sets the grain direction of the laminate applied on the bottom face.
    /// Supported values: <c>None</c>, <c>Lengthwise</c>, <c>Crosswise</c>.
    /// </summary>
    /// <example>Lengthwise</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottomGrain))]
    public Grain? LaminateBottomGrain { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate applied on the top face.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>Laminate_Oak_Natural</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTop))]
    [StringLength(50, MinimumLength = 1)]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the grain direction of the laminate applied on the top face.
    /// Supported values: <c>None</c>, <c>Lengthwise</c>, <c>Crosswise</c>.
    /// </summary>
    /// <example>Crosswise</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTopGrain))]
    public Grain? LaminateTopGrain { get; set; }
}