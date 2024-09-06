using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Result for one machine
/// </summary>
public class PredictionWorkplaceData
{
    /// <summary>
    /// requested workplaceId
    /// </summary>    
    [JsonProperty(Order = 1)]
    public Guid WorkplaceId { get; set; }

    /// <summary>
    /// The minimum estimated duration calculated based on Quartile 1.
    /// </summary>
    [JsonProperty(Order = 2)]
    public TimeSpan? DurationMin { get; set; }

    /// <summary>
    /// Predicted edgebanding duration.
    /// </summary>
    [JsonProperty(Order = 3)]
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// The maximum estimated duration calculated based on Quartile 3.
    /// </summary>
    [JsonProperty(Order = 4)]
    public TimeSpan? DurationMax { get; set; }

}