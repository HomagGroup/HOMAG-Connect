using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Result for one machine
/// </summary>
public class PredictionMachineData : PredictionBaseData
{
    /// <summary>
    /// relevant machinenumber
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MachineNumber { get; set; } = string.Empty;

    /// <summary>
    /// Machine name
    /// </summary>
    [JsonProperty(Order = 2)]
    public string MachineName { get; set; } = string.Empty;

    /// <summary>
    /// defines the object type as the class
    /// </summary>
    public string DataSourceType { get; } = nameof(PredictionMachineData);
}