using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts;

/// <summary>
/// list of parts / productionEntities for analysis and prediction of productionTime
/// </summary>
public class PredictionRequest : IExtensibleDataObject
{
    /// <summary>
    /// List of Parts, that will be produced, and for which we need prediction
    /// </summary>
    public IEnumerable<EdgebandPredictPart>? ProductionEntities { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}