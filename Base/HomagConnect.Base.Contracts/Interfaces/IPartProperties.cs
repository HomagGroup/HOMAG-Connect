using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines the complete set of properties that describe a part in an optimization or production context.
/// Combines dimension, material, edgebanding, laminating, and CNC program properties.
/// </summary>
/// <remarks>
/// Inherited interfaces: <see cref="IDimensionProperties" /> (length, width, thickness),
/// <see cref="IMaterialProperties" /> (material code, grain), <see cref="IEdgebandingProperties" /> (edge codes and diagram),
/// <see cref="ILaminatingProperties" /> (laminate codes and grain), <see cref="ICncProgramProperties" /> (CNC program names).
/// </remarks>
public interface IPartProperties : ILaminatingProperties, IEdgebandingProperties, IDimensionProperties, IMaterialProperties, ICncProgramProperties
{
    /// <summary>
    /// Gets or sets a human-readable description of the part. Maximum length is 255 characters.
    /// </summary>
    /// <example>Side Panel Left</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Description))]
    [StringLength(255, MinimumLength = 0)]
    public string Description { get; set; }
}