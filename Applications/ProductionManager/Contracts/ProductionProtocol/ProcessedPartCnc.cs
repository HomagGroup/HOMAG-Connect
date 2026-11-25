using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed CNC part in a production protocol, including executed program information and timing data.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedPartCnc",
///   "identifier": "10",
///   "description": "Side Panel Left",
///   "programName": "Drilling_Main.mpr",
///   "programDuration": "00:03:12",
///   "preview": "https://example.com/previews/part-10.png",
///   "startedAt": "2025-01-20T14:32:10+00:00",
///   "completedAt": "2025-01-20T14:35:22+00:00"
/// }
/// </example>
public class ProcessedPartCnc : ProcessedPart
{
    /// <inheritdoc />
    public override ProcessedItemType Type
    {
        get
        {
            return ProcessedItemType.ProcessedPartCnc;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    /// <summary>
    /// Duration of the CNC program execution for the part, as a time span. Optional.
    /// </summary>
    /// <example>00:03:12</example>
    [JsonProperty(Order = 31)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(ProgramDuration))]
    public TimeSpan? ProgramDuration { get; set; }

    /// <summary>
    /// Name of the CNC program executed for the part. Optional.
    /// </summary>
    /// <example>Drilling_Main.mpr</example>
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(ProgramName))]
    public string? ProgramName { get; set; }

    /// <summary>
    /// URL of a preview image of the part. Optional.
    /// </summary>
    /// <example>https://example.com/previews/part-10.png</example>
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Preview))]
    public Uri? Preview { get; set; }

    /// <summary>
    /// Date and time the CNC program execution started. Optional.
    /// </summary>
    /// <example>2025-01-20T14:32:10+00:00</example>
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(StartedAt))]
    [DisplayFormat(DataFormatString = "{0:g}")]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
    /// Date and time the CNC program execution completed. Optional.
    /// </summary>
    /// <example>2025-01-20T14:35:22+00:00</example>
    [JsonProperty(Order = 35)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(CompletedAt))]
    [DisplayFormat(DataFormatString = "{0:g}")]
    public DateTimeOffset? CompletedAt { get; set; }
}
