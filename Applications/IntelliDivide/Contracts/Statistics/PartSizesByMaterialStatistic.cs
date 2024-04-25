using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Statistics;

/// <summary>
/// Provides the part sizes by material statistic.
/// </summary>
public class PartSizesByMaterialStatistic
{
    /// <summary>
    /// Gets the length of the part.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double Length { get; set; }

    /// <summary>
    /// Gets the material code of the part.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MaterialCode { get; set; }

    /// <summary>
    /// Gets the quantity of parts having the same <see cref="Length" />, <see cref="Width" /> and <see cref="MaterialCode" />.
    /// </summary>
    [JsonProperty(Order = 2)]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the width of the part.
    /// </summary>
    [JsonProperty(Order = 4)]
    public double Width { get; set; }
}