using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// Parameters for milling.
    /// </summary>
    public class MillingParameters : IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Value of the cutting speed.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double CuttingSpeed { get; set; }

        /// <summary>
        /// Maximal value of the cutting speed.
        /// </summary>
        [JsonProperty(Order = 4)]
        public double? CuttingSpeedMax { get; set; }

        /// <summary>
        /// Minimal value of the cutting speed.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double? CuttingSpeedMin { get; set; }

        /// <summary>
        /// Determines if parameters should be used with this parameterGroup.
        /// </summary>
        [JsonProperty(Order = 1)]
        public bool Suitability { get; set; }

        /// <summary>
        /// Value of the tooth feed.
        /// </summary>
        [JsonProperty(Order = 5)]
        public double ToothFeed { get; set; }

        /// <summary>
        /// Maximal value of the tooth feed.
        /// </summary>
        [JsonProperty(Order = 7)]
        public double? ToothFeedMax { get; set; }

        /// <summary>
        /// Minimal value of the tooth feed.
        /// </summary>
        [JsonProperty(Order = 6)]
        public double? ToothFeedMin { get; set; }

        /// <summary>
        /// Units for the parameters.
        /// </summary>
        [JsonProperty(Order = 50)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}