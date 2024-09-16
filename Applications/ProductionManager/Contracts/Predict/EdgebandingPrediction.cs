using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Edgebanding Prediction
    /// </summary>
    public class EdgebandingPrediction : PredictionBase
    {
        /// <summary>
        /// Gets the predicted length of edgebands by edgeband code.
        /// </summary>
        [JsonProperty(Order = 4)]
        public Dictionary<string, double> LengthByEdgebandCode { get; set; } = [];

        /// <summary>
        /// Machines on which the prediction is based (in format "x-xxx-xx-xxxx").
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<PredictionMachineData> PredictionBase { get; set; } = [];

    }
}