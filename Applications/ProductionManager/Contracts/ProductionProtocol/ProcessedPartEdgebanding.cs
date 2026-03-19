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
///   "edgeDiagram": "FBLR"
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
}