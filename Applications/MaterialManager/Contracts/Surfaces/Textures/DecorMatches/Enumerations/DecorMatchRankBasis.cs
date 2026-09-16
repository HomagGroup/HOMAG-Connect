using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

/// <summary>
/// Indicates which ranking strategy produced the <see cref="DecorMatchSearchResponse.Candidates" /> list.
/// </summary>
[ResourceManager(typeof(DecorMatchRankBasisDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum DecorMatchRankBasis
{
    /// <summary>
    /// Candidates are ranked by confidence score (the normal path).
    /// </summary>
    [Display(Description = "Confidence")]
    Confidence,

    /// <summary>
    /// Candidates are ranked by usage frequency (Edgeband fallback path when no confident match exists).
    /// </summary>
    [Display(Description = "Usage frequency")]
    UsageFrequency
}
