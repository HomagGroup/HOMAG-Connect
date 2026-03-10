using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines the material-related properties of a part or board.
/// Includes the material code and the grain direction used by downstream optimization and production systems.
/// </summary>
public interface IMaterialProperties
{
    /// <summary>
    /// Gets or sets the grain direction.
    /// Supported values: <c>None</c>, <c>Lengthwise</c>, <c>Crosswise</c>.
    /// </summary>
    /// <example>Lengthwise</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Grain))]
    public Grain Grain { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>P2_White_19.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Material))]
    [StringLength(50, MinimumLength = 1)]
    public string? Material { get; set; }
}