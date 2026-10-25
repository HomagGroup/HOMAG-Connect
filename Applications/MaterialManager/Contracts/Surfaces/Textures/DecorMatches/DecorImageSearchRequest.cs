using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search for decors that match an image which is downloaded from a public URI.
/// </summary>
public class DecorImageSearchRequest
{
    /// <summary>
    /// Provide the public <c>https</c> URI of the image to search with. The image must be a PNG or JPEG
    /// (<c>.png</c>, <c>.jpg</c>, <c>.jpeg</c>) and must not exceed 10 MB.
    /// </summary>
    /// <example>https://example.com/images/oak.jpg</example>
    [JsonProperty("imageUri", Order = 1)]
    [Required]
    public Uri ImageUri { get; set; } = null!;
}
