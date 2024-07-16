using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgebandPrediction : IExtensibleDataObject
    {
        /// <summary>
        /// Predicted Timespan for the given parts
        /// </summary>
        public TimeSpan Prediction { get; set; }

        /// <summary>
        /// Strategy, which has been chosen based on the data of the customer
        /// </summary>
        public EdgebandPredictionStrategy PredictionStrategy { get; set; }

        /// <summary>
        /// plausible PredictionTime calculated on the Quartile 1
        /// </summary>
        public TimeSpan PredictionRangeMin { get; set; }

        /// <summary>
        /// plausible PredictionTime calculated on the Quartile 1
        /// </summary>
        public TimeSpan PredictionRangeMax { get; set; }

        /// <summary>
        /// PredictionsMachines (in format "x-xxx-xx-xxxx")
        /// </summary>
        public IEnumerable<string> PredictionMachines { get; set; } = new List<string>();

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
