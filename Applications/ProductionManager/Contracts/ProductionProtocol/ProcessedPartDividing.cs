using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part that includes cutting-specific details, such as optimization name, pattern information,
/// and board-related data.
/// </summary>
/// <remarks>
/// This class extends <see cref="ProcessedPart" /> to include additional properties specific to the
/// cutting/nesting process. It provides details such as the optimization name, pattern name and cycle, board code, and the
/// quantity of boards cut in a book.
/// </remarks>
public class ProcessedPartDividing : ProcessedPart
{
    /// <summary>
    /// Gets or sets the id of the BoardEntity used
    /// </summary>
    [JsonProperty(Order = 31)]
    public string? BoardEntityId { get; set; }

    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    [JsonProperty(Order = 32)]
    public string? BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the name of optimization (aka run, job, etc.).
    /// </summary>
    [JsonProperty(Order = 33)]
    public string? OptimizationName { get; set; }

    /// <summary>
    /// Gets or sets the pattern cycle.
    /// </summary>
    [JsonProperty(Order = 34)]
    public int? PatternCycle { get; set; }

    /// <summary>
    /// Gets or sets the pattern name.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? PatternName { get; set; }
}