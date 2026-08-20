using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Statistics;

/// <summary>
/// Provides the part sizes by material statistic.
/// </summary>
[LocalizationResource(typeof(StatisticsDisplayNames))]
public class PartSizesByMaterialStatistic : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets the length of the part.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(Length))]
    public double Length { get; set; }

    /// <summary>
    /// Gets the material code of the part.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(MaterialCode))]
    public string MaterialCode { get; set; }

    /// <summary>
    /// Gets the quantity of parts having the same <see cref="Length" />, <see cref="Width" /> and <see cref="MaterialCode" />.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(Quantity))]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the width of the part.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(Width))]
    public double Width { get; set; }
}