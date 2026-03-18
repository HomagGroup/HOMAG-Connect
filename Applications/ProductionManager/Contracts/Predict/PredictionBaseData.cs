using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents prediction result data from a single data source.
/// </summary>
/// <example>
/// {
///   "durationMin": "00:10:00",
///   "duration": "00:12:30",
///   "durationMax": "00:15:00",
///   "dataSourceType": "PredictionMachineData"
/// }
/// </example>
public class PredictionBaseData
{
    /// <summary>
    /// Gets or sets the minimum estimated duration, calculated from the lower quartile.
    /// </summary>
    /// <example>00:10:00</example>
    [JsonProperty(Order = 3)]
    public TimeSpan? DurationMin { get; set; }

    /// <summary>
    /// Gets or sets the predicted duration.
    /// </summary>
    /// <example>00:12:30</example>
    [JsonProperty(Order = 4)]
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Gets or sets the maximum estimated duration, calculated from the upper quartile.
    /// </summary>
    /// <example>00:15:00</example>
    [JsonProperty(Order = 5)]
    public TimeSpan? DurationMax { get; set; }

    /// <summary>
    /// Gets or sets the type of data source used to create this prediction result.
    /// </summary>
    /// <example>PredictionMachineData</example>
    public virtual string DataSourceType { get; set; } = nameof(PredictionBaseData);
}
