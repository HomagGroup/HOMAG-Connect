using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Result for one machine
/// </summary>
public class PredictionWorkplaceData: PredictionBaseData
{
    /// <summary>
    /// requested workplaceId
    /// </summary>    
    [JsonProperty(Order = 1)]
    public Guid WorkplaceId { get; set; }

    /// <summary>
    /// defines the object type as the class
    /// </summary>
    public string DataSourceType { get; } = nameof(PredictionWorkplaceData);

}