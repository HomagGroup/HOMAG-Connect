using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Represents a set of properties that describe a part, including laminating, edgebanding, dimension, material, and CNC
/// program characteristics.
/// </summary>
/// <remarks>This interface combines multiple property interfaces to provide a comprehensive description of a
/// part's attributes in manufacturing or design contexts. Implementers should ensure that all inherited property
/// contracts are fulfilled.</remarks>
public interface IPartProperties : ILaminatingProperties, IEdgebandingProperties, IDimensionProperties, IMaterialProperties, ICncProgramProperties
{
    /// <summary>
    /// Gets or sets the description of the part.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Description))]
    [StringLength(255, MinimumLength = 0)]
    public string Description { get; set; }
}