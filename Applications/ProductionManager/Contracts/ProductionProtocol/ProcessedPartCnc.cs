using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol for CNC machines, including program name and duration.
/// </summary>
public class ProcessedPartCnc : ProcessedPart
{
    /// <summary>
    /// Gets or sets the duration of the CNC program execution for this part.
    /// </summary>
    [JsonProperty(Order = 31)]
    public TimeSpan? ProgramDuration { get; set; }

    /// <summary>
    /// Gets or sets the name of the CNC program executed for this part.
    /// </summary>
    [JsonProperty(Order = 32)]
    public string? ProgramName { get; set; }

    /// <summary>
    /// Gets or sets the preview image for this part.
    /// </summary>
    [JsonProperty(Order = 33)]
    public Uri? Preview { get; set; }

    /// <summary>
    /// Gets or sets the Timestamp when the CNC program execution started.
    /// </summary>
    [JsonProperty(Order = 34)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the Timestamp when the CNC program execution completed.
    /// </summary>
    [JsonProperty(Order = 35)]
    public DateTimeOffset? CompletedAt { get; set; }
}