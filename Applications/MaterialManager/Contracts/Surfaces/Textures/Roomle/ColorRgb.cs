namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Simple RGB color container (0–1 or 0–255 normalized depending on source).
/// Used in Roomle shading JSON payloads.
/// </summary>
public class ColorRgb
{
    /// <summary>
    /// Blue channel component.
    /// </summary>
    public double B { get; set; }

    /// <summary>
    /// Green channel component.
    /// </summary>
    public double G { get; set; }

    /// <summary>
    /// Red channel component.
    /// </summary>
    public double R { get; set; }
}