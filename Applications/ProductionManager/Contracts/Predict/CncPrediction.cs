using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// CNC Prediction
    /// </summary>
    public class CncPrediction : PredictionBase
    {
        /// <summary>
        /// Machines on which the prediction is based (in format "x-xxx-xx-xxxx").
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<PredictionBaseData> PredictionBase { get; set; } = [];

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}