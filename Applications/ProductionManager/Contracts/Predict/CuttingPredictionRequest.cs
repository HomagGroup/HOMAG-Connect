using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Cutting Prediction Request
/// </summary>
public class CuttingPredictionRequest : IExtensibleDataObject
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
    /// MachineNumber for which the prediction should be made.
    /// </summary>
    public string? MachineNumber { get; set; }

    /// <summary>
    /// List of Parts, that will be produced, and for which we need prediction
    /// </summary>
    public IEnumerable<CuttingPredictionPart>? PredictionParts { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}