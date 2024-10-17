using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Model for material dependent tension trim parameters.
    /// </summary>
    public class TensionTrimParameters
    {
        private const double _LengthConstraintMin = 0;
        private const double _LengthConstraintMax = 9999.9;

        /// <summary>
        /// Gets or sets whether the tension trim parameters are enabled.
        /// </summary>
        [JsonProperty(Order = 10)]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the width including the saw blade if the tension trim. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 13)]
        public double? WidthIncludingSawBladeThickness { get; set; }

        /// <summary>
        /// Gets or sets the minimum distance of the tension trim. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 11)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? MinimumDistanceBetweenTensionTrims { get; set; }

        /// <summary>
        /// Gets or sets the minimum cutting length of the tension trim. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 12)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? MinimumCuttingLength { get; set; }

    }
}