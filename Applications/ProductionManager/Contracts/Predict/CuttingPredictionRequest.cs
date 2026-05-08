using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents a request to predict cutting duration for one or more parts on a specific machine.
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
    /// <param name="predictionParts">The parts to include in the prediction request.</param>
    public CuttingPredictionRequest(IEnumerable<CuttingPredictionPart>? predictionParts):this()
    {
        PredictionParts = predictionParts;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    /// <param name="predictionParts">The parts to include in the prediction request.</param>
    /// <param name="machineNumber">The machine number for which the prediction should be calculated.</param>
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
    /// Gets or sets additional custom properties configured in the application.
    /// JSON properties that are not mapped to typed members are captured here via <c>[JsonExtensionData]</c>.
    /// </summary>
    /// <example>{ "customField1": "value1" }</example>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}