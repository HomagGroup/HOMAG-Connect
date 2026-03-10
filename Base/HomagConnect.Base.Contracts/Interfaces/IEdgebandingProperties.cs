using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines the edgebanding-related properties of a part.
/// Includes edgeband codes for each side and an optional edge diagram representation.
/// </summary>
public interface IEdgebandingProperties
{
    /// <summary>
    /// Gets or sets the edgeband code applied on the back side.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>EB_White_1mm</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeBack))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets an optional edge diagram string that describes how edgebands are applied.
    /// </summary>
    /// <example>FBLR</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeDiagram))]
    public string? EdgeDiagram { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code applied on the front side.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>EB_White_1mm</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeFront))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeFront { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code applied on the left side.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>EB_White_1mm</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeLeft))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeLeft { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code applied on the right side.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>EB_White_1mm</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeRight))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeRight { get; set; }
}