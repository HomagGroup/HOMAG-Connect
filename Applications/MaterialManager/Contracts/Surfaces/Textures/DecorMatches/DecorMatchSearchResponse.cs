using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents the result of a decor match search, containing the matching decors found.
/// </summary>
public class DecorMatchSearchResponse : ISupportsLocalizedSerialization, ISupportsAdditionalProperties
{
    /// <summary>
    /// Gets or sets the candidates found for the current page. Candidates are always returned, even when
    /// their <see cref="DecorMatchCandidate.ConfidenceScore" /> is 0, so that customers can visually browse
    /// the available decors when no confident match exists; callers should not treat an empty list as the
    /// only "no match" signal.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(Candidates))]
    public IReadOnlyList<DecorMatchCandidate> Candidates { get; set; } = new List<DecorMatchCandidate>();

    /// <summary>
    /// Gets or sets the total number of candidates available, independent of paging.
    /// </summary>
    /// <example>3</example>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(DecorMatchDisplayNames), Name = nameof(TotalCount))]
    public int TotalCount { get; set; }

    /// <inheritdoc />
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}