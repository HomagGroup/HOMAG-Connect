using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Material properties
/// </summary>
public interface IMaterialProperties
{
    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Grain))]
    public Grain Grain { get; set; }

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Material))]
    public string? Material { get; set; }
}