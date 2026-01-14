using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(ProgramDuration))]
    public TimeSpan? ProgramDuration { get; set; }

    /// <summary>
    /// Gets or sets the name of the CNC program executed for this part.
    /// </summary>
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(ProgramName))]
    public string? ProgramName { get; set; }

    /// <summary>
    /// Gets or sets the preview image for this part.
    /// </summary>
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Preview))]
    public Uri? Preview { get; set; }

    /// <summary>
    /// Gets or sets the Timestamp when the CNC program execution started.
    /// </summary>
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(StartedAt))]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the Timestamp when the CNC program execution completed.
    /// </summary>
    [JsonProperty(Order = 35)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(CompletedAt))]
    public DateTimeOffset? CompletedAt { get; set; }
}