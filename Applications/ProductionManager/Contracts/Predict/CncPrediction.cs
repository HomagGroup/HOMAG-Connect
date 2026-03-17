using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents a CNC duration prediction.
/// Inherits the predicted duration, minimum duration, maximum duration, and prediction method from <see cref="PredictionBase" />.
/// </summary>
/// <example>
/// {
///   "duration": "00:12:30",
///   "durationMin": "00:10:00",
///   "durationMax": "00:15:00",
///   "predictionBase": [
///     { "dataSourceType": "1-001-01-0001", "duration": "00:12:30", "durationMin": "00:10:00", "durationMax": "00:15:00" }
///   ]
/// }
/// </example>
public class CncPrediction : PredictionBase
{
    /// <summary>
    /// Gets or sets the machine-specific prediction data used as the basis for the aggregated CNC prediction.
    /// Machine identifiers use the format <c>x-xxx-xx-xxxx</c>.
    /// </summary>
    /// <example>
    /// [
    ///   { "dataSourceType": "1-001-01-0001", "duration": "00:12:30", "durationMin": "00:10:00", "durationMax": "00:15:00" }
    /// ]
    /// </example>
    [JsonProperty(Order = 6)]
    public IEnumerable<PredictionBaseData> PredictionBase { get; set; } = [];
}