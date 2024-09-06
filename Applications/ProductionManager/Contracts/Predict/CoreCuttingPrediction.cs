using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Cutting Prediction
    /// </summary>
    public class CoreCuttingPrediction : IExtensibleDataObject
    {
        /// <summary>
        /// Predicted duration.
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
        /// Machines on which the prediction is based (in format "x-xxx-xx-xxxx").
        /// </summary>
        [JsonProperty(Order = 6)]
        public IEnumerable<PredictionWorkplaceData> PredictionBase { get; set; } = [];

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