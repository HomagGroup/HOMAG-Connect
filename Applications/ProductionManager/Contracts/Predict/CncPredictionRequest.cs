using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// CNC Prediction Request
/// </summary>
public class CncPredictionRequest : IExtensibleDataObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CncPredictionRequest "/> class.
    /// </summary>
    public CncPredictionRequest()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CncPredictionRequest "/> class.
    /// </summary>
    public CncPredictionRequest(IEnumerable<CncPredictionPart>? predictionParts):this()
    {
        PredictionParts = predictionParts;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    public CncPredictionRequest(IEnumerable<CncPredictionPart>? predictionParts, string? machineNumber) :this(predictionParts)
    {
        MachineNumber = machineNumber;
    }

    /// <summary>
    /// MachineNumber for which the prediction should be made.
    /// </summary>
    public string? MachineNumber { get; set; }

    /// <summary>
    /// List of Parts, that will be produced, and for which we need prediction
    /// </summary>
    public IEnumerable<CncPredictionPart>? PredictionParts { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}