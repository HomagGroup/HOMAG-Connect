using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Material properties
/// </summary>
public interface IMaterialProperties
{
    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    public Grain Grain { get; set; }
}