using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Edgebanding Prediction Request
/// </summary>
public class EdgebandingPredictionRequest : IExtensibleDataObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EdgebandingPredictionRequest"/> class.
    /// </summary>
    public EdgebandingPredictionRequest()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EdgebandingPredictionRequest"/> class.
    /// </summary>
    public EdgebandingPredictionRequest(IEnumerable<EdgebandingPredictionPart>? predictionParts):this()
    {
        PredictionParts = predictionParts;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EdgebandingPredictionRequest"/> class.
    /// </summary>
    public EdgebandingPredictionRequest( IEnumerable<EdgebandingPredictionPart>? predictionParts, string? machineNumber) :this(predictionParts)
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
    public IEnumerable<EdgebandingPredictionPart>? PredictionParts { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}