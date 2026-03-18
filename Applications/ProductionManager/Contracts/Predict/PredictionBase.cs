using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Represents the common result data for a production duration prediction.
    /// </summary>
    /// <example>
    /// {
    ///   "duration": "00:12:30",
    ///   "durationMin": "00:10:00",
    ///   "durationMax": "00:15:00",
    ///   "predictionMethod": "Machine"
    /// }
    /// </example>
    public class PredictionBase
    {
        /// <summary>
        /// Gets or sets the predicted duration.
        /// </summary>
        /// <example>00:12:30</example>
        [JsonProperty(Order = 1)]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the maximum estimated duration, calculated from the upper quartile.
        /// </summary>
        /// <example>00:15:00</example>
        [JsonProperty(Order = 3)]
        public TimeSpan DurationMax { get; set; }

        /// <summary>
        /// Gets or sets the minimum estimated duration, calculated from the lower quartile.
        /// </summary>
        /// <example>00:10:00</example>
        [JsonProperty(Order = 2)]
        public TimeSpan DurationMin { get; set; }
        
        /// <summary>
        /// Gets or sets the method used to calculate the prediction.
        /// Supported values include <c>Machine</c>, <c>MachineGroup</c>, <c>Workplace</c>, <c>WorkplaceGroup</c>, and <c>Global</c>.
        /// </summary>
        /// <example>Machine</example>
        [JsonProperty(Order = 9)]
        public PredictionMethod PredictionMethod { get; set; }

        /// <summary>
        /// Gets or sets additional custom properties configured in the application. Any JSON properties not mapped
        /// to a typed member are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "customField1": "value1" }</example>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}