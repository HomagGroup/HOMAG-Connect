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
    public CuttingPredictionRequest(IEnumerable<CuttingPredictionPart>? productionEntities):this()
    {
        ProductionEntities = productionEntities;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CuttingPredictionRequest"/> class.
    /// </summary>
    public CuttingPredictionRequest(IEnumerable<CuttingPredictionPart>? productionEntities, string? machine) :this(productionEntities)
    {
        Machine = machine;
    }

    /// <summary>
    /// Machine for which the prediction should be made.
    /// </summary>
    public string? Machine { get; set; }

    /// <summary>
    /// List of Parts, that will be produced, and for which we need prediction
    /// </summary>
    public IEnumerable<CuttingPredictionPart>? ProductionEntities { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}