using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents the response of a decor match search, returned by <c>POST /v1/decor-matches:search</c>.
/// </summary>
/// <remarks>
/// The service targets a p95 response time of 300ms or less, and never calls the Roomle API inline to build
/// this response; <see cref="DecorMatchCandidate.TextureUrl" /> is always served from a pre-populated cache.
/// </remarks>
public class DecorMatchSearchResponse : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the ranking strategy used to order <see cref="Candidates" />, from highest to lowest.
    /// </summary>
    /// <example>Confidence</example>
    [JsonProperty(Order = 0)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(RankBasis))]
    public DecorMatchRankBasis RankBasis { get; set; }

    /// <summary>
    /// Gets or sets the total number of candidates available, independent of paging.
    /// </summary>
    /// <example>3</example>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(TotalCount))]
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the candidates found for the current page. This list can be empty when no confident
    /// match exists, or contain multiple entries when matches are ambiguous; callers must handle both cases.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(Candidates))]
    public IReadOnlyList<DecorMatchCandidate> Candidates { get; set; } = new List<DecorMatchCandidate>();
}
