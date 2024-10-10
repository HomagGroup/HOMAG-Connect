using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    /// <summary>
    /// An edge inventory for statistical use.
    /// </summary>
    public class EdgeInventoryHistory : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        private const int _EdgebandCodeMaxLength = 50;
        private const double _HeightDimensionMinValue = 0.1;
        private const double _HeightDimensionMaxValue = 9999.9;

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Key]
        [Required]
        [StringLength(_EdgebandCodeMaxLength, MinimumLength = 1)]
        [JsonProperty(Order = 2)]
        public string EdgebandCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the costs of the Edgeband. The unit depends on the settings of the subscription.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [Required]
        [Range(_HeightDimensionMinValue, _HeightDimensionMaxValue)]
        [JsonProperty(Order = 4)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Height { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the inventory.
        /// </summary>
        [Required]
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the total quantity allocated
        /// </summary>
        [Required]
        [JsonProperty(Order = 5)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? TotalLengthInInventory { get; set; }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; }

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}