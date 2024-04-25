using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Statistics
{
    /// <summary>
    /// A board type inventory for statistical use.
    /// </summary>
    public class BoardTypeInventoryHistory : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        private const int _BoardCodeMaxLength = 50;
        private const int _MaterialCodeMaxLength = 50;
        private const double _LengthDimensionMinValue = 0.1;
        private const double _LengthDimensionMaxValue = 9999.9;

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Key]
        [Required]
        [StringLength(_BoardCodeMaxLength, MinimumLength = 1)]
        [JsonProperty(Order = 2)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        [JsonProperty(Order = 3)]
        public BoardTypeType BoardTypeType { get; set; }

        /// <summary>
        /// Gets or sets the costs of the board. The unit depends on the settings of the subscription.
        /// </summary>
        [JsonProperty(Order = 6)]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [Required]
        [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
        [JsonProperty(Order = 4)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(_MaterialCodeMaxLength, MinimumLength = 1)]
        [JsonProperty(Order = 2)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp of the inventory.
        /// </summary>
        [Required]
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the total area of boards of this type which have been allocated
        /// </summary>
        [JsonProperty(Order = 10)]
        public double? TotalAreaAllocated
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAllocated);
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type
        /// </summary>
        [JsonProperty(Order = 9)]
        public double? TotalAreaInventory
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
            }
        }

        /// <summary>
        /// Gets or sets the total quantity allocated
        /// </summary>
        [JsonProperty(Order = 8)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Gets or sets the total quantity available
        /// </summary>
        [JsonProperty(Order = 7)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Gets or sets the width of the board. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Required]
        [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
        [JsonProperty(Order = 5)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

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