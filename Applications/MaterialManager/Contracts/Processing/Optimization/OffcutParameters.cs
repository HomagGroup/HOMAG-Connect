using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    public class OffcutParameters : IContainsUnitSystemDependentProperties, IValidatableObject
    {
        private const double _LengthConstraintMin = 0.1;
        private const double _LengthConstraintMax = 9999.9;

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 30)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region IValidatableObject Members

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (LargeOffcutsEnabled && !OffcutsEnabled)
            {
                results.Add(new ValidationResult($"When '{nameof(LargeOffcutsEnabled)}' is true, '{nameof(OffcutsEnabled)}' must be true as well.",
                    new[] { nameof(LargeOffcutsEnabled), nameof(OffcutsEnabled) }));
            }
            else if (LargeOffcutsEnabled && OffcutsEnabled)
            {
                if (LargeOffcutMinimumLength <= OffcutMinimumLength)
                {
                    results.Add(new ValidationResult(
                        $"'{nameof(LargeOffcutMinimumLength)}' must be greater than '{nameof(OffcutMinimumLength)}'.",
                        new[] { nameof(LargeOffcutMinimumLength), nameof(OffcutMinimumLength) }));
                }

                if (LargeOffcutMinimumWidth <= OffcutMinimumWidth)
                {
                    results.Add(new ValidationResult(
                        $"'{nameof(LargeOffcutMinimumWidth)}' must be greater than '{nameof(OffcutMinimumWidth)}'.",
                        new[] { nameof(LargeOffcutMinimumWidth), nameof(OffcutMinimumWidth) }));
                }

                if (LargeOffcutMinimumArea <= OffcutMinimumArea)
                {
                    results.Add(new ValidationResult(
                        $"'{nameof(LargeOffcutMinimumArea)}' must be greater than '{nameof(OffcutMinimumArea)}'.",
                        new[] { nameof(LargeOffcutMinimumArea), nameof(OffcutMinimumArea) }));
                }

                if (LargeOffcutValue <= OffcutValue)
                {
                    results.Add(new ValidationResult(
                        $"'{nameof(LargeOffcutValue)}' must be greater than '{nameof(OffcutValue)}'.",
                        new[] { nameof(LargeOffcutValue), nameof(OffcutValue) }));
                }
            }

            return results;
        }

        #endregion

        #region Public Properties

        #region Offcuts

        /// <summary>
        /// Gets or sets a value indicating whether offcuts are enabled.
        /// </summary>
        [JsonProperty(Order = 10)]
        public bool OffcutsEnabled { get; set; }

        [JsonProperty(Order = 11)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? OffcutMinimumLength { get; set; }

        [JsonProperty(Order = 12)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? OffcutMinimumWidth { get; set; }

        [JsonProperty(Order = 13)]
        public double? OffcutMinimumArea { get; set; }

        [JsonProperty(Order = 14)]
        [Range(0, 1)]
        public double? OffcutValue { get; set; }

        #endregion

        #region Large Off

        /// <summary>
        /// Gets or sets a value indicating whether large offcuts are enabled.
        /// </summary>
        [JsonProperty(Order = 20)]
        public bool LargeOffcutsEnabled { get; set; }

        [JsonProperty(Order = 21)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? LargeOffcutMinimumLength { get; set; }

        [JsonProperty(Order = 22)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        public double? LargeOffcutMinimumWidth { get; set; }

        [JsonProperty(Order = 23)]
        public double? LargeOffcutMinimumArea { get; set; }

        [JsonProperty(Order = 24)]
        [Range(0, 1)]
        public double? LargeOffcutValue { get; set; }

        #endregion

        #endregion
    }
}