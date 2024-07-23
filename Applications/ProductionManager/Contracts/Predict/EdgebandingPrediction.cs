using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Edgebanding Prediction
    /// </summary>
    public class EdgebandingPrediction : IExtensibleDataObject
    {
        /// <summary>
        /// Predicted edgebanding duration.
        /// </summary>
        [JsonProperty(Order = 1)]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The maximum estimated duration calculated based on Quartile 3.
        /// </summary>
        [JsonProperty(Order = 3)]
        public TimeSpan DurationMax { get; set; }

        /// <summary>
        /// The minimum estimated duration calculated based on Quartile 1.
        /// </summary>
        [JsonProperty(Order = 2)]
        public TimeSpan DurationMin { get; set; }

        /// <summary>
        /// Gets the predicted length of edgebands by edgeband code.
        /// </summary>
        [JsonProperty(Order = 4)]
        public Dictionary<string, double> LengthByEdgebandCode { get; set; } = [];

        /// <summary>
        /// Machines on which the prediction is based (in format "x-xxx-xx-xxxx").
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<string> PredictionBase { get; set; } = [];

        /// <summary>
        /// Method, which has been chosen to predict duration.
        /// </summary>
        [JsonProperty(Order = 5)]
        public PredictionMethod PredictionMethod { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}