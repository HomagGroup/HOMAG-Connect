using System;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Model for material dependent book height parameters.
    /// </summary>
    public class BookHeightParameters : IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets whether the book height parameters are enabled.
        /// </summary>
        [JsonProperty(Order = 10)]
        public BookHeightMode Mode { get; set; }

        /// <summary>
        /// The deduction of the maximum saw blade projection.
        /// </summary>
        [Obsolete("This parameter is obsolete because its mode was removed. Please see BookHeightMode for the available modes")]
        [JsonProperty(Order = 11)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? MaximumSawBladeProjectionDeduction { get; set; }

        /// <summary>
        /// The maximum book height.
        /// </summary>
        [Obsolete("This parameter is obsolete because its mode was removed. Please see BookHeightMode for the available modes")]
        [JsonProperty(Order = 12)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? MaximumBookHeight { get; set; }

        /// <summary>
        /// The limit of the book height.
        /// </summary>
        [JsonProperty(Order = 13)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? LimitedBookHeight { get; set; }

        /// <summary>
        /// The current unit system.
        /// </summary>
        public UnitSystem UnitSystem { get; set; }
    }
}