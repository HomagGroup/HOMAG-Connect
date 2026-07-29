using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed laminating part in a production protocol, including laminate information for top and bottom faces.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedPartLaminating",
///   "identifier": "10",
///   "description": "Table Top",
///   "quantity": 2,
///   "length": 1200,
///   "width": 800,
///   "thickness": 19.0,
///   "unitSystem": "Metric",
///   "material": "P2_White_19.0",
///   "grain": "Lengthwise",
///   "laminateTop": "Laminate_Oak_Natural",
///   "laminateTopGrain": "Lengthwise",
///   "laminateBottom": "Laminate_White_Matt",
///   "laminateBottomGrain": "Crosswise",
///   "laminateTopThumbnail": "https://example.com/materials/laminates/Laminate_Oak_Natural.png",
///   "laminateBottomThumbnail": "https://example.com/materials/laminates/Laminate_White_Matt.png"
/// }
/// </example>
public class ProcessedPartLaminating : ProcessedPart, ILaminatingProperties
{
    /// <inheritdoc />
    public override ProcessedItemType Type
    {
        get
        {
            return ProcessedItemType.ProcessedPartLaminating;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    #region ILaminatingProperties

    /// <inheritdoc />      
    [JsonProperty(Order = 31)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottom))]
    public string? LaminateBottom { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottomGrain))]
    public Grain? LaminateBottomGrain { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTop))]
    public string? LaminateTop { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTopGrain))]
    public Grain? LaminateTopGrain { get; set; }

    #endregion

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/boards/EB_White_19mm.png</example>
    [JsonProperty(Order = 39)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(LaminateBottomThumbnail))]
    public Uri? LaminateBottomThumbnail { get; set; }

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/boards/EB_White_19mm.png</example>
    [JsonProperty(Order = 40)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(LaminateTopThumbnail))]
    public Uri? LaminateTopThumbnail { get; set; }
}