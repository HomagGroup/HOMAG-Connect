﻿using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Model for material dependent offcut parameters.
    /// </summary>
    public class OffcutParameters : IContainsUnitSystemDependentProperties
    {
        private const double _LengthConstraintMin = 0.1;
        private const double _LengthConstraintMax = 9999.9;

        /// <summary>
        /// Gets or sets whether the offcut parameters are enabled.
        /// </summary>
        [JsonProperty(Order = 10)]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the minimum area of the offcut. The value is dependent on the unit system (Metric: m², Imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 13)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? MinimumArea { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the offcut. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 11)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? MinimumLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum width of the offcut. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 12)]
        [Range(_LengthConstraintMin, _LengthConstraintMax)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? MinimumWidth { get; set; }

        /// <summary>
        /// Gets or sets the value of the offcut in %.
        /// </summary>
        [JsonProperty(Order = 14)]
        [Range(0, 1)]
        public double? Value { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 30)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}