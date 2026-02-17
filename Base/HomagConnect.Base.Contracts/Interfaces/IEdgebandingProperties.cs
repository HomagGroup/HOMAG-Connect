using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Edgebanding properties
/// </summary>
public interface IEdgebandingProperties
{
    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeBack))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets how the edgebands should get applied.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeDiagram))]
    public string? EdgeDiagram { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeFront))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeFront { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeLeft))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeLeft { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeRight))]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeRight { get; set; }
}