using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents an edgebanding duration prediction.
/// Inherits the predicted duration, minimum duration, maximum duration, and prediction method from <see cref="PredictionBase" />.
/// </summary>
/// <example>
/// {
///   "duration": "00:18:00",
///   "durationMin": "00:15:30",
///   "durationMax": "00:21:00",
///   "lengthByEdgebandCode": {
///     "EB_White_1mm": 124.5
///   },
///   "predictionBase": [
///     {
///       "machineNumber": "1-001-01-0001",
///       "machineName": "EDGETEQ S-500",
///       "duration": "00:18:00",
///       "durationMin": "00:15:30",
///       "durationMax": "00:21:00"
///     }
///   ]
/// }
/// </example>
public class EdgebandingPrediction : PredictionBase
{
    /// <summary>
    /// Gets or sets the predicted edgeband length grouped by edgeband code.
    /// </summary>
    /// <example>{ "EB_White_1mm": 124.5 }</example>
    [JsonProperty(Order = 4)]
    public Dictionary<string, double> LengthByEdgebandCode { get; set; } = [];

    /// <summary>
    /// Gets or sets the machine-specific prediction data used as the basis for the aggregated edgebanding prediction.
    /// Machine identifiers use the format <c>x-xxx-xx-xxxx</c>.
    /// </summary>
    /// <example>
    /// [
    ///   {
    ///     "machineNumber": "1-001-01-0001",
    ///     "machineName": "EDGETEQ S-500",
    ///     "duration": "00:18:00",
    ///     "durationMin": "00:15:30",
    ///     "durationMax": "00:21:00"
    ///   }
    /// ]
    /// </example>
    [JsonProperty(Order = 6)]
    public IEnumerable<PredictionMachineData> PredictionBase { get; set; } = [];
}