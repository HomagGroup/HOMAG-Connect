using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a single decor candidate returned from a decor match search.
/// </summary>
public class DecorMatchCandidate : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the identifier of the decor.
    /// </summary>
    /// <example>dec-001</example>
    [JsonProperty(Order = 0)]
    [Required]
    public string DecorId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the localized name of the decor, resolved using the request's <see cref="DecorMatchSearchRequest.Culture" />.
    /// </summary>
    /// <example>White</example>
    [JsonProperty(Order = 1)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchCandidateDisplayNames), Name = nameof(LocalizedName))]
    public string LocalizedName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the preview images of the decor. At minimum, entries for <c>Small</c>, <c>Medium</c> and
    /// <c>Large</c> can be present.
    /// </summary>
    [JsonProperty(Order = 2)]
    public IReadOnlyList<AdditionalDataPreview> Previews { get; set; } = new List<AdditionalDataPreview>();

    /// <summary>
    /// Gets or sets the texture url for this decor. There is exactly one texture per decor, and it is always
    /// served from a pre-populated cache rather than resolved inline.
    /// </summary>
    /// <example>https://example.com/textures/dec-001.jpg</example>
    [JsonProperty(Order = 3)]
    [Required]
    public string TextureUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confidence score of the match, in the range 0.0 - 1.0.
    /// </summary>
    /// <example>0.95</example>
    [JsonProperty(Order = 4)]
    [Range(0.0, 1.0)]
    public double ConfidenceScore { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this candidate is recommended. This is <c>true</c> if and only if
    /// <see cref="ConfidenceScore" /> is greater than or equal to 0.9.
    /// </summary>
    [JsonProperty(Order = 5)]
    public bool IsRecommended { get; set; }
}
