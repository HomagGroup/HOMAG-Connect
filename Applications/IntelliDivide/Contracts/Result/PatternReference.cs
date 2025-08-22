using Newtonsoft.Json;

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
    public string PatternId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of parts in the pattern.
    /// </summary>
    [JsonProperty(Order = 2)]
    public int QuantityOfParts { get; set; }
}