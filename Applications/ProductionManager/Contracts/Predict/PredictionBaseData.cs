using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Result for one machine
/// </summary>
public class PredictionBaseData
{
    /// <summary>
    /// The minimum estimated duration calculated based on Quartile 1.
    /// </summary>
    [JsonProperty(Order = 3)]
    public TimeSpan? DurationMin { get; set; }

    /// <summary>
    /// Predicted edgebanding duration.
    /// </summary>
    [JsonProperty(Order = 4)]
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// The maximum estimated duration calculated based on Quartile 3.
    /// </summary>
    [JsonProperty(Order = 5)]
    public TimeSpan? DurationMax { get; set; }


    public virtual string DataSourceType { get; set; }
}
