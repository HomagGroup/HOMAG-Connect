using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Statistics
{
    /// <summary>
    /// A board type inventory for statistical use.
    /// </summary>
    public class BoardTypeInventoryHistory : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
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
        [Range(0.1, 9999.9)]
        [JsonProperty(Order = 4)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
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
                if (Width == null || Length == null || TotalQuantityAllocated == null)
                {
                    return null;
                }

                if (UnitSystem == UnitSystem.Metric)
                {
                    return Length / 1000 * Width / 1000 * TotalQuantityAllocated;
                }

                if (UnitSystem == UnitSystem.Imperial)
                {
                    return Length * Width / 144 * TotalQuantityAllocated;
                }

                throw new NotSupportedException("Unknown unit system.");
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
                if (Width == null || Length == null || TotalQuantityInInventory == null)
                {
                    return null;
                }

                if (UnitSystem == UnitSystem.Metric)
                {
                    return Length / 1000 * Width / 1000 * TotalQuantityInInventory;
                }

                if (UnitSystem == UnitSystem.Imperial)
                {
                    return Length * Width / 144 * TotalQuantityInInventory;
                }

                throw new NotSupportedException("Unknown unit system.");
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
        [Range(0.1, 9999.9)]
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