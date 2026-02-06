using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Texture mapping type used in Roomle payloads. Parsed tolerantly from JSON via <see cref="TolerantEnumConverter"/>.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum TextureMapping
{
    /// <summary>
    /// Fallback when an unknown or empty mapping string is encountered.
    /// </summary>
    Unknown,

    /// <summary>
    /// Standard color texture mapping with Red, Green, Blue, Alpha channels.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    RGBA,

    /// <summary>
    /// Vector mapping with X, Y, Z components (e.g., normals or directional data).
    /// </summary>
    // ReSharper disable once InconsistentNaming
    XYZ
}