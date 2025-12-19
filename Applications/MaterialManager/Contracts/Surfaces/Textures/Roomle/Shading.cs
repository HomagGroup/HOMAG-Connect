namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Physically based shading parameters for a Roomle material.
/// Contains alpha, metallic, roughness, color channels and various coating attributes.
/// </summary>
public class Shading
{
    /// <summary>
    /// Shading schema/version string.
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Global alpha value.
    /// </summary>
    public double Alpha { get; set; }

    /// <summary>
    /// Alpha cutoff threshold used for mask mode.
    /// </summary>
    public double AlphaCutoff { get; set; }

    /// <summary>
    /// Alpha blending mode (opaque, mask, blend).
    /// </summary>
    public AlphaMode AlphaMode { get; set; } = AlphaMode.OPAQUE;

    /// <summary>
    /// Base color in RGB.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    public ColorRgb? Basecolor { get; set; }

    /// <summary>
    /// Transmission factor for translucency.
    /// </summary>
    public double Transmission { get; set; }

    /// <summary>
    /// Index of refraction for transmission.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public double TransmissionIOR { get; set; }

    /// <summary>
    /// Metallic coefficient.
    /// </summary>
    public double Metallic { get; set; }

    /// <summary>
    /// Surface roughness.
    /// </summary>
    public double Roughness { get; set; }

    /// <summary>
    /// Whether the material should render double-sided.
    /// </summary>
    public bool DoubleSided { get; set; }

    /// <summary>
    /// Ambient occlusion intensity.
    /// </summary>
    public double Occlusion { get; set; }

    /// <summary>
    /// Emissive color component.
    /// </summary>
    public ColorRgb? EmissiveColor { get; set; }

    /// <summary>
    /// Emissive intensity.
    /// </summary>
    public double EmissiveIntensity { get; set; }

    /// <summary>
    /// Clearcoat layer intensity.
    /// </summary>
    public double ClearcoatIntensity { get; set; }

    /// <summary>
    /// Clearcoat layer roughness.
    /// </summary>
    public double ClearcoatRoughness { get; set; }

    /// <summary>
    /// Clearcoat normal scale.
    /// </summary>
    public double ClearcoatNormalScale { get; set; }

    /// <summary>
    /// Sheen color for fabric-like materials.
    /// </summary>
    public ColorRgb? SheenColor { get; set; }

    /// <summary>
    /// Sheen intensity.
    /// </summary>
    public double SheenIntensity { get; set; }

    /// <summary>
    /// Sheen roughness.
    /// </summary>
    public double SheenRoughness { get; set; }

    /// <summary>
    /// Normal map scale.
    /// </summary>
    public double NormalScale { get; set; }

    /// <summary>
    /// Thickness factor for volumetric effects.
    /// </summary>
    public double ThicknessFactor { get; set; }

    /// <summary>
    /// Attenuation color for volumetric transmission.
    /// </summary>
    public ColorRgb? AttenuationColor { get; set; }

    /// <summary>
    /// Attenuation distance for volumetric transmission.
    /// </summary>
    public double AttenuationDistance { get; set; }
}