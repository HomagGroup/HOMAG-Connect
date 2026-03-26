using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents a request for cutting duration prediction.
/// </summary>
/// <example>
/// {
///   "machineNumber": "1-001-01-0001",
///   "predictionParts": [
///     {
///       "id": "PART-10",
///       "quantity": 2,
///       "length": 720,
///       "width": 480,
///       "thickness": 19.0,
///       "unitSystem": "Metric"
///     }
///   ]
/// }
/// </example>
public class CuttingPredictionRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    public CuttingPredictionRequest()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    public CuttingPredictionRequest(IEnumerable<CuttingPredictionPart>? predictionParts):this()
    {
        PredictionParts = predictionParts;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    public CuttingPredictionRequest(IEnumerable<CuttingPredictionPart>? predictionParts, string? machineNumber) :this(predictionParts)
    {
        MachineNumber = machineNumber;
    }

    /// <summary>
    /// Gets or sets the machine number for which the prediction should be calculated.
    /// </summary>
    /// <example>1-001-01-0001</example>
    public string? MachineNumber { get; set; }

    /// <summary>
    /// Gets or sets the parts for which the cutting duration prediction should be calculated.
    /// </summary>
    /// <example>
    /// [
    ///   {
    ///     "id": "PART-10",
    ///     "quantity": 2,
    ///     "length": 720,
    ///     "width": 480,
    ///     "thickness": 19.0,
    ///     "unitSystem": "Metric"
    ///   }
    /// ]
    /// </example>
    public IEnumerable<CuttingPredictionPart>? PredictionParts { get; set; }
    
    /// <summary>
    /// Gets or sets additional custom properties configured in the application. Any JSON properties not mapped
    /// to a typed member are captured here via <c>[JsonExtensionData]</c>.
    /// </summary>
    /// <example>{ "customField1": "value1" }</example>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}