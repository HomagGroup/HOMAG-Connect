using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Double value with optional tolerance and unit system.
    /// </summary>
    public class DoubleValue : IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Constructor for DoubleValue.
        /// </summary>
        public DoubleValue(double? value, double? tolerance = null)
        {
            Value = value;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Allowed value tolerance
        /// </summary>
        [JsonProperty(Order = 1)]
        public double? Tolerance { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty(Order = 2)]
        public double? Value { get; set; }

        /// <summary>
        /// The unit system for the value.
        /// </summary>
        [JsonProperty(Order = 3)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}