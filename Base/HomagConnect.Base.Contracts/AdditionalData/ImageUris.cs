using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Download links for a single image in several sizes. Sizes that are not available are null.
/// </summary>
/// <example>
/// { "thumbnail": "https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_thumbnail.png", "small": "https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_thumbnail.png",
/// "medium": "https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png", "large": "https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png",
/// "original": "https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png", "downloadFileName": "f434_st20_texture.png" }
/// </example>
public class ImageUris
{
    /// <summary>
    /// Link to the smallest version of the image, suitable for lists and grids.
    /// </summary>
    /// <example>https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_thumbnail.png</example>
    [JsonProperty(Order = 0)]
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// Link to a small version of the image, suitable for compact cards.
    /// </summary>
    /// <example>https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_thumbnail.png</example>
    [JsonProperty(Order = 1)]
    public Uri? Small { get; set; }

    /// <summary>
    /// Link to a medium-sized version of the image, suitable for detail views.
    /// </summary>
    /// <example>https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png</example>
    [JsonProperty(Order = 2)]
    public Uri? Medium { get; set; }

    /// <summary>
    /// Link to a large version of the image, suitable for full-screen display.
    /// </summary>
    /// <example>https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png</example>
    [JsonProperty(Order = 3)]
    public Uri? Large { get; set; }

    /// <summary>
    /// Link to the image in its original, unscaled resolution.
    /// </summary>
    /// <example>https://materialmanager.homag.cloud/catalogs/decors/egger/f434_st20_texture.png</example>
    [JsonProperty(Order = 4)]
    public Uri? Original { get; set; }

    /// <summary>
    /// File name, including extension, to use when saving the image locally.
    /// </summary>
    /// <example>f434_st20_texture.png</example>
    [JsonProperty(Order = 5)]
    public string? DownloadFileName { get; set; }
}
