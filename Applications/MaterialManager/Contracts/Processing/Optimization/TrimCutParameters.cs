﻿using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Model for material dependent trim cut parameters.
    /// </summary>
    public class TrimCutParameters
    {
        private const double _LengthConstraintMin = 0;
        private const double _LengthConstraintMax = 9999.9;

        /// <summary>
        /// Determines whether undershooting the minimum trim cut is allowed.
        /// </summary>
        [JsonProperty(Order = 10)]
        public bool MinimumTrimCutUndershotAllowed { get; set; }

        /// <summary>
        /// Gets or sets the minimum trim cut. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 13)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double MinimumTrimCut { get; set; }

        /// <summary>
        /// Gets or sets the minimum trim recut in front. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 11)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double MinimumTrimRecutInFront { get; set; }

        /// <summary>
        /// Gets or sets the minimum trim recut behind. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 12)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double MinimumTrimRecutBehind { get; set; }

    }
}