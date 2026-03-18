using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents prediction result data from a single machine.
/// </summary>
/// <example>
/// {
///   "machineNumber": "1-001-01-0001",
///   "machineName": "EDGETEQ S-500",
///   "durationMin": "00:15:30",
///   "duration": "00:18:00",
///   "durationMax": "00:21:00",
///   "dataSourceType": "PredictionMachineData"
/// }
/// </example>
public class PredictionMachineData : PredictionBaseData
{
    /// <summary>
    /// Gets or sets the machine number used as the prediction data source.
    /// </summary>
    /// <example>1-001-01-0001</example>
    [JsonProperty(Order = 1)]
    public string MachineNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the machine name used as the prediction data source.
    /// </summary>
    /// <example>EDGETEQ S-500</example>
    [JsonProperty(Order = 2)]
    public string MachineName { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string DataSourceType { get; set; } = nameof(PredictionMachineData);
}