using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed edgebanding part in a production protocol, including edge information for each side.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedPartEdgebanding",
///   "identifier": "10",
///   "description": "Side Panel Left",
///   "edgeFront": "EB_White_1mm",
///   "edgeBack": "EB_White_1mm",
///   "edgeLeft": "EB_White_1mm",
///   "edgeRight": "EB_White_1mm",
///   "edgeDiagram": "FBLR",
///   "edgeFrontThumbnail": "https://example.com/materials/edgebands/EB_White_1mm.png",
///   "edgeBackThumbnail": "https://example.com/materials/edgebands/EB_White_1mm.png",
///   "edgeLeftThumbnail": "https://example.com/materials/edgebands/EB_White_1mm.png",
///   "edgeRightThumbnail": "https://example.com/materials/edgebands/EB_White_1mm.png"
/// }
/// </example>
public class ProcessedPartEdgebanding : ProcessedPart, IEdgebandingProperties
{
    /// <inheritdoc />
    public override ProcessedItemType Type
    {
        get
        {
            return ProcessedItemType.ProcessedPartEdgebanding;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    /// <inheritdoc />
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(EdgeFront))]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(EdgeBack))]
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(EdgeLeft))]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 35)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(EdgeRight))]
    public string? EdgeRight { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 36)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(EdgeDiagram))]
    public string? EdgeDiagram { get; set; }

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/edgebands/EB_White_1mm.png</example>
    [JsonProperty(Order = 37)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeFrontThumbnail))]
    public Uri? EdgeFrontThumbnail { get; set; }

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/edgebands/EB_White_1mm.png</example>
    [JsonProperty(Order = 38)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeBackThumbnail))]
    public Uri? EdgeBackThumbnail { get; set; }

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/edgebands/EB_White_1mm.png</example>
    [JsonProperty(Order = 39)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeLeftThumbnail))]
    public Uri? EdgeLeftThumbnail { get; set; }

    /// <summary>
    /// Gets or sets the URI of the product thumbnail image.
    /// </summary>
    /// <example>https://example.com/materials/edgebands/EB_White_1mm.png</example>
    [JsonProperty(Order = 40)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeRightThumbnail))]
    public Uri? EdgeRightThumbnail { get; set; }
}