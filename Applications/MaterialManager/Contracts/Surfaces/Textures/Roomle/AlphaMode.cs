using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Alpha blending mode for Roomle materials. Values are parsed tolerantly from JSON via <see cref="TolerantEnumConverter"/>.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum AlphaMode
{
    /// <summary>
    /// Fallback value when the JSON contains an unknown or empty alpha mode.
    /// </summary>
    Unknown,

    /// <summary>
    /// Fully opaque rendering; alpha channel ignored.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    OPAQUE,

    /// <summary>
    /// Binary transparency using a cutoff threshold.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    MASK,

    /// <summary>
    /// Standard alpha blending for semi-transparent materials.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    BLEND
}