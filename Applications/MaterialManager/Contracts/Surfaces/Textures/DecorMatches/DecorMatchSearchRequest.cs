using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <c>BoardType</c> or <c>EdgebandType</c>.
/// </summary>
public class DecorMatchSearchRequest
{
    /// <summary>
    /// Gets or sets the discriminator that determines whether <see cref="Material" /> is deserialized as a
    /// <c>BoardType</c> or an <c>EdgebandType</c>.
    /// </summary>
    /// <example>Board</example>
    [JsonProperty(Order = 0)]
    [Required]
    public DecorMatchMaterialType MaterialType { get; set; }

    /// <summary>
    /// Gets or sets the complete, unmodified <c>BoardType</c> or <c>EdgebandType</c> payload, passed through
    /// This is intentionally untyped so the search service can use any field present
    /// (e.g. <c>ManufacturerName</c>, <c>ArticleNumber</c>, <c>DecorCode</c>, <c>DecorName</c>,
    /// <c>EmbossingTop</c>, <c>EmbossingBottom</c>, <c>ProductName</c>, or others) as a matching signal, and so new signal fields can be consumed without requiring a contract change here. Any identifying field may be absent or <c>null</c> when identifying information is incomplete.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public JObject? Material { get; set; }

    /// <summary>
    /// Gets or sets an optional, customer-supplied free text search term. It is applied on top of the
    /// usage-frequency fallback ranking, narrowing rather than replacing it, and has no effect when a
    /// confidence-ranked result set is returned instead of the fallback.
    /// </summary>
    /// <example>Oak</example>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(DecorMatchSearchRequestDisplayNames), Name = nameof(SearchTerm))]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the preferred culture used to resolve <see cref="DecorMatchCandidate.LocalizedName" />.
    /// </summary>
    /// <example>en-US</example>
    [JsonProperty(Order = 3)]
    public string Culture { get; set; } = "en-US";

    /// <summary>
    /// Gets or sets the number of candidates to skip for paging.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 4)]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; } = 0;

    /// <summary>
    /// Gets or sets the maximum number of candidates to return. The server caps this value (e.g. to 200)
    /// regardless of what is requested.
    /// </summary>
    /// <example>50</example>
    [JsonProperty(Order = 5)]
    [Range(1, 200)]
    public int Take { get; set; } = 50;
}
