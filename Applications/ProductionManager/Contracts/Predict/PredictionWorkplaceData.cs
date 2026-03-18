using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents prediction result data from a single workplace.
/// </summary>
/// <example>
/// {
///   "workplaceId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff",
///   "durationMin": "00:10:00",
///   "duration": "00:12:30",
///   "durationMax": "00:15:00",
///   "dataSourceType": "PredictionWorkplaceData"
/// }
/// </example>
public class PredictionWorkplaceData : PredictionBaseData
{
    /// <summary>
    /// Gets or sets the workplace identifier used as the prediction data source.
    /// </summary>
    /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
    [JsonProperty(Order = 1)]
    public Guid WorkplaceId { get; set; }

    /// <inheritdoc />
    public override string DataSourceType { get; set; } = nameof(PredictionWorkplaceData);

}