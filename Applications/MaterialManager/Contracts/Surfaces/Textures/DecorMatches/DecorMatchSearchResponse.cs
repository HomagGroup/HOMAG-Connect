using System.Collections.Generic;

using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents the response of a decor match search, returned by <c>POST /v1/decor-matches:search</c>.
/// </summary>
/// <remarks>
/// The service targets a p95 response time of 300ms or less, and never calls the Roomle API inline to build
/// this response; <see cref="DecorMatchCandidate.TextureUrl" /> is always served from a pre-populated cache.
/// </remarks>
public class DecorMatchSearchResponse
{
    /// <summary>
    /// Gets or sets the ranking strategy used to order <see cref="Candidates" />, from highest to lowest.
    /// </summary>
    /// <example>Confidence</example>
    [JsonProperty(Order = 0)]
    public DecorMatchRankBasis RankBasis { get; set; }

    /// <summary>
    /// Gets or sets the number of candidates skipped, echoing the request's <see cref="DecorMatchSearchRequest.Skip" />.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 1)]
    public int Skip { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of candidates requested, echoing the request's <see cref="DecorMatchSearchRequest.Take" />.
    /// </summary>
    /// <example>50</example>
    [JsonProperty(Order = 2)]
    public int Take { get; set; }

    /// <summary>
    /// Gets or sets the total number of candidates available, independent of paging.
    /// </summary>
    /// <example>3</example>
    [JsonProperty(Order = 3)]
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the candidates found for the current page. This list can be empty when no confident
    /// match exists, or contain multiple entries when matches are ambiguous; callers must handle both cases.
    /// </summary>
    [JsonProperty(Order = 4)]
    public IReadOnlyList<DecorMatchCandidate> Candidates { get; set; } = new List<DecorMatchCandidate>();
}
