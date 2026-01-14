using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part that includes edgebanding-specific details, such as optimization name, pattern information,
/// and board-related data.
/// </summary>
/// <remarks>
/// This class extends <see cref="ProcessedPart" /> to include additional properties specific to the
/// edgebanding process. It provides details such as the EdgeBack, EdgeFront, EdgeLeft and EdgeRight
/// </remarks>
public class ProcessedPartEdgebanding : ProcessedPart, IEdgebandingProperties
{
    /// <summary>
    /// Gets or sets the EdgeFront
    /// </summary>
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeFront))]
    public string? EdgeFront { get; set; }

    /// <summary>
    /// Gets or sets the EdgeBack
    /// </summary>
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeBack))]
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets the EdgeLeft
    /// </summary>
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeLeft))]
    public string? EdgeLeft { get; set; }

    /// <summary>
    /// Gets or sets the EdgeRight
    /// </summary>
    [JsonProperty(Order = 35)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeRight))]
    public string? EdgeRight { get; set; }

    /// <summary>
    /// Gets or sets how the edgebands should be applied
    /// </summary>
    [JsonProperty(Order = 36)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(EdgeDiagram))]
    public string? EdgeDiagram { get; set; }
}