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

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; }

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region BoardType properties

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(_MaterialCodeMaxLength, MinimumLength = 1)]
        [JsonProperty(Order = 2)]
        public string MaterialCode { get; set; } = string.Empty;

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
        /// Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [Required]
        [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
        [JsonProperty(Order = 4)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the width of the board. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Required]
        [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
        [JsonProperty(Order = 5)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the costs of the board. The unit depends on the settings of the subscription.
        /// </summary>
        [JsonProperty(Order = 6)]
        public double? Costs { get; set; }

        #endregion

        #region Inventory

        /// <summary>
        /// Gets or sets the timestamp of the inventory.
        /// </summary>
        [Required]
        [JsonProperty(Order = 1)]
        public DateTimeOffset Timestamp { get; set; }

        #region Quantity

        /// <summary>
        /// Gets or sets the total quantity allocated
        /// </summary>
        [JsonProperty(Order = 11)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Gets or sets the total quantity available
        /// </summary>
        [JsonProperty(Order = 10)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Gets or sets the total quantity available
        /// </summary>
        [JsonProperty(Order = 12)]
        public int? TotalQuantityAvailable
        {
            get
            {
                if (TotalQuantityInInventory.HasValue && TotalQuantityAvailable.HasValue)
                {
                    return TotalQuantityInInventory.Value - TotalQuantityAvailable.Value;
                }

                return null;
            }
        }

        #endregion

        #region Value

        /// <summary>
        /// Gets the total value of boards of this type in inventory
        /// </summary>
        [JsonProperty(Order = 30)]
        public double? TotalValueInInventory
        {
            get
            {
                if (TotalAreaInInventory.HasValue && Costs.HasValue)
                {
                    return Costs.Value * TotalAreaInInventory.Value;
                }

                return null;
            }
        }

        #endregion

        #region Area

        /// <summary>
        /// Gets or sets the total area of boards of this type
        /// </summary>
        [JsonProperty(Order = 20)]
        public double? TotalAreaInInventory
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
            }
        }

        /// <summary>
        /// Gets the total area of boards of this type
        /// </summary>
        [JsonProperty(Order = 20)]
        [Obsolete]
        public double? TotalAreaInventory
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
            }
        }

        /// <summary>
        /// Gets the total area of boards of this type which have been allocated
        /// </summary>
        [JsonProperty(Order = 21)]
        public double? TotalAreaAllocated
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAllocated);
            }
        }

        /// <summary>
        /// Gets the total area of boards of this type which are available.
        /// </summary>
        [JsonProperty(Order = 22)]
        public double? TotalAreaAvailable
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAvailable);
            }
        }

        #endregion

        #endregion
    }
}