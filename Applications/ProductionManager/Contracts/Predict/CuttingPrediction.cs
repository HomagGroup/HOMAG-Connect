using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Cutting Prediction
    /// </summary>
    public class CuttingPrediction : PredictionBase
    {
        
        /// <summary>
        /// Machines on which the prediction is based (in format "x-xxx-xx-xxxx").
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<PredictionBaseData> PredictionBase { get; set; } = [];
    }
}