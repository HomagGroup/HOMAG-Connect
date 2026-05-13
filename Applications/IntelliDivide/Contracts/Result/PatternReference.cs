using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Links a part to a pattern.
/// </summary>
public class PatternReference
{
    /// <summary>
    /// Gets or sets the lot id.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(PatternId))]
    public string PatternId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of parts in the pattern.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(QuantityOfParts))]
    public int QuantityOfParts { get; set; }
}