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
    public EdgebandingPredictionRequest(IEnumerable<EdgebandingPredictionPart>? productionEntities):this()
    {
        ProductionEntities = productionEntities;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EdgebandingPredictionRequest"/> class.
    /// </summary>
    public EdgebandingPredictionRequest( IEnumerable<EdgebandingPredictionPart>? productionEntities, string? machine) :this(productionEntities)
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
    public IEnumerable<EdgebandingPredictionPart>? ProductionEntities { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}