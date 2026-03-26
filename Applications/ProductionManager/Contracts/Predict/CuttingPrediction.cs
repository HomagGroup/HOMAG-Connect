using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents a cutting duration prediction.
/// Inherits the predicted duration, minimum duration, maximum duration, and prediction method from <see cref="PredictionBase" />.
/// </summary>
/// <example>
/// {
///   "duration": "00:08:45",
///   "durationMin": "00:07:30",
///   "durationMax": "00:10:15",
///   "predictionBase": [
///     { "dataSourceType": "1-001-01-0001", "duration": "00:08:45", "durationMin": "00:07:30", "durationMax": "00:10:15" }
///   ]
/// }
/// </example>
public class CuttingPrediction : PredictionBase
{
    /// <summary>
    /// Gets or sets the machine-specific prediction data used as the basis for the aggregated cutting prediction.
    /// Machine identifiers use the format <c>x-xxx-xx-xxxx</c>.
    /// </summary>
    /// <example>
    /// [
    ///   { "dataSourceType": "1-001-01-0001", "duration": "00:08:45", "durationMin": "00:07:30", "durationMax": "00:10:15" }
    /// ]
    /// </example>
    [JsonProperty(Order = 6)]
    public IEnumerable<PredictionBaseData> PredictionBase { get; set; } = [];
}