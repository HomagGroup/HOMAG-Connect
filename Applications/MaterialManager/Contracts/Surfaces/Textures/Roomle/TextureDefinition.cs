namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Definition metadata for a texture asset, including pixel and physical dimensions,
/// tiling capability and mapping type.
/// </summary>
public class TextureDefinition
{
    /// <summary>
    /// Texture height in pixels.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Texture width in pixels.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Physical height in millimeters corresponding to the texture.
    /// </summary>
    public int MmHeight { get; set; }

    /// <summary>
    /// Physical width in millimeters corresponding to the texture.
    /// </summary>
    public int MmWidth { get; set; }

    /// <summary>
    /// Indicates whether the texture can be tiled seamlessly.
    /// </summary>
    public bool Tileable { get; set; }

    /// <summary>
    /// Mapping type for interpreting texture data (e.g., RGBA or XYZ).
    /// </summary>
    public TextureMapping Mapping { get; set; }
}