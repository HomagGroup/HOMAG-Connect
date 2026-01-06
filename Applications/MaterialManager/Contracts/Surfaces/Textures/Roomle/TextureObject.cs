using System;
using System.Collections.Generic;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Describes a concrete texture asset instance for a material, including dimensions,
/// mapping, creation/update timestamps, and related asset links.
/// </summary>
public class TextureObject
{
    /// <summary>
    /// Arbitrary asset dictionary with auxiliary resources (e.g., thumbnails, normal maps).
    /// </summary>
    public Dictionary<string, object>? Assets { get; set; }

    /// <summary>
    /// Timestamp when the texture object was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Definition metadata including pixel and physical dimensions.
    /// </summary>
    public TextureDefinition? Definition { get; set; }

    /// <summary>
    /// Texture height in pixels.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Unique identifier of the texture object.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Image file name or relative path.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Mapping type for interpreting texture data (RGBA/XYZ).
    /// </summary>
    public TextureMapping Mapping { get; set; }

    /// <summary>
    /// Material identifier this texture is associated with.
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Physical height in millimeters.
    /// </summary>
    public int MmHeight { get; set; }

    /// <summary>
    /// Physical width in millimeters.
    /// </summary>
    public int MmWidth { get; set; }

    /// <summary>
    /// Quality level indicator provided by source system.
    /// </summary>
    public int QualityLevel { get; set; }

    /// <summary>
    /// Indicates whether the texture is tileable (seamless).
    /// </summary>
    public bool Tileable { get; set; }

    /// <summary>
    /// Timestamp when the texture object was last updated.
    /// </summary>
    public DateTime Updated { get; set; }

    /// <summary>
    /// Absolute or relative URL to the texture resource.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Texture width in pixels.
    /// </summary>
    public int Width { get; set; }
}