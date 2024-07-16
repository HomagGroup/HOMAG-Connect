using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgebandPredictionResponse : IExtensibleDataObject
    {
        /// <summary>
        /// Predicted Timespan for the given parts
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Strategy, which has been chosen based on the data of the customer
        /// </summary>
        public EdgebandPredictionStrategy PredictionStrategy { get; set; }

        /// <summary>
        /// PredictionTime based on the Quartile 1
        /// </summary>
        public TimeSpan PredictionMinValueQ1 { get; set; }

        /// <summary>
        /// PredictionTime based on the Quartile 3
        /// </summary>
        public TimeSpan PredictionMaxValueQ3 { get; set; }

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
