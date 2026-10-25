using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a single decor candidate returned from a decor match search.
/// </summary>
public class DecorMatchCandidate : ISupportsLocalizedSerialization, ISupportsAdditionalProperties
{
    /// <summary>
    /// Gets or sets the identifier of the decor.
    /// </summary>
    /// <example>catalog:dec-001_sand</example>
    [JsonProperty(Order = 1)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(DecorId))]
    public string DecorId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the decor.
    /// </summary>
    /// <example>dec-001</example>
    [JsonProperty(Order = 2)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(DecorCode))]
    public string DecorCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the catalog.
    /// </summary>
    /// <example>catalog</example>
    [JsonProperty(Order = 3)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(Catalog))]
    public string Catalog { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the embossing of the decor.
    /// </summary>
    /// <example>sand</example>
    [JsonProperty(Order = 4)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(Embossing))]
    public string Embossing { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the localized name of the decor, resolved using the request's <see cref="DecorMatchSearchRequest.Culture" />.
    /// </summary>
    /// <example>White</example>
    [JsonProperty(Order = 5)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(LocalizedName))]
    public string LocalizedName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the preview images of the decor. At minimum, entries for <c>Small</c>, <c>Medium</c> and
    /// <c>Large</c> must be present.
    /// </summary>
    [JsonProperty(Order = 6)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(Previews))]
    public IReadOnlyList<AdditionalDataPreview> Previews { get; set; } = new List<AdditionalDataPreview>();

    /// <summary>
    /// Gets or sets the texture URI for this decor. There is exactly one texture per decor, and it is always
    /// served from a pre-populated cache rather than resolved inline.
    /// </summary>
    /// <example>https://example.com/textures/dec-001.jpg</example>
    [JsonProperty(Order = 7)]
    [Required]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(TextureUri))]
    public Uri? TextureUri { get; set; }

    /// <summary>
    /// Gets or sets the confidence score of the match, in the range 0.0 - 1.0.
    /// </summary>
    /// <example>0.95</example>
    [JsonProperty(Order = 8)]
    [Range(0.0, 1.0)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(ConfidenceScore))]
    public double ConfidenceScore { get; set; }

    /// <inheritdoc />
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}
