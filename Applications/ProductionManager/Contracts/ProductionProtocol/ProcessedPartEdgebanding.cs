using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

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
    /// <inheritdoc />
    [JsonProperty(Order = 32)]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 33)]
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets the EdgeLeft
    /// </summary>
    [JsonProperty(Order = 34)]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    /// >
    [JsonProperty(Order = 35)]
    public string? EdgeRight { get; set; }

    /// <inheritdoc />
    /// >
    [JsonProperty(Order = 36)]
    public string? EdgeDiagram { get; set; }
}