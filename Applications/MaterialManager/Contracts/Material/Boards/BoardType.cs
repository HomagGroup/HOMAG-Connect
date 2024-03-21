using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type.
    /// </summary>
    [DebuggerDisplay("{BoardCode}")]
    public class BoardType : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the timestamp when board type has been used last.
        /// </summary>
        [JsonProperty(Order = 90)]
        public DateTimeOffset? LastUsed { get; set; }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />

        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region Material

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 10)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 24)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the material category name
        /// </summary>
        [JsonProperty(Order = 12)]
        public string MaterialCategory { get; set; } = string.Empty; // TODO: Will be handled by intelliSettings BE to provide a fullname, not the ID

        [JsonProperty(Order = 13)]
        public string CoatingCategory { get; set; } = string.Empty; // TODO: Will be handled by intelliSettings BE to provide a fullname, not the ID

        [JsonProperty(Order = 14)]
        public StandardQuality StandardQuality { get; set; }

        #endregion

        #region Board type

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the width of the board. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        [JsonProperty(Order = 22)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        [JsonProperty(Order = 23)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or set the grain of the board.
        /// </summary>
        [JsonProperty(Order = 25)]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the costs of the board. The unit depends on the settings of the subscription.
        /// </summary>
        [JsonProperty(Order = 26)]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        [JsonProperty(Order = 27)]
        public BoardTypeType BoardTypeType { get; set; }

        #endregion

        #region Manufacturer

        /// <summary>
        /// Gets or sets the name of the manufacturer.
        /// </summary>
        [JsonProperty(Order = 31)]
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [JsonProperty(Order = 32)]
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the article number.
        /// </summary>
        [JsonProperty(Order = 33)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        [JsonProperty(Order = 34)]
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        [JsonProperty(Order = 35)]
        public string? DecorName { get; set; }

        #endregion

        #region Material Management

        [JsonProperty(Order = 53)]
        public int? TotalQuantityAvailableWarningLimit { get; set; } // Isn't MinimumAmountAvailable the same?

        /// <summary>
        /// Gets or sets whether the board type should be optimized against infinite.
        /// </summary>
        [JsonProperty(Order = 92)]
        public bool OptimizeAgainstInfinite { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the board type is locked for optimization.
        /// </summary>
        [JsonProperty(Order = 93)]
        public bool LockedForOptimization { get; set; }

        #endregion

        #region Inventory

        /// <summary>
        /// Gets or sets the total quantity of boards of this type in the inventory.
        /// </summary>
        [JsonProperty(Order = 50)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which have been allocated to a production order.
        /// </summary>
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which are available in the inventory.
        /// </summary>
        [JsonProperty(Order = 52)]
        public int? TotalQuantityAvailable
        {
            get
            {
                if (TotalQuantityInInventory != null && TotalQuantityAllocated != null)
                {
                    return TotalQuantityInInventory - TotalQuantityAllocated;
                }

                if (TotalQuantityInInventory != null && TotalQuantityAllocated == null)
                {
                    return TotalQuantityInInventory;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the
        /// subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 56)]
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

                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on
        /// the settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 57)]
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

                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the
        /// settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 58)]
        public double? TotalAreaAvailable
        {
            get
            {
                if (Width == null || Length == null || TotalQuantityAvailable == null)
                {
                    return null;
                }

                if (UnitSystem == UnitSystem.Metric)
                {
                    return Length / 1000 * Width / 1000 * TotalQuantityAvailable;
                }

                if (UnitSystem == UnitSystem.Imperial)
                {
                    return Length * Width / 144 * TotalQuantityAvailable;
                }

                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets a indication whether the <see cref="TotalQuantityAvailable" /> is below the defined limit
        /// <see cref="TotalQuantityAvailableWarningLimit" />.
        /// </summary>
        [JsonProperty(Order = 54)]
        public bool? TotalQuantityAvailableWarningLimitReached { get; set; }

        #endregion

        #region Additional data

        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        [StringLength(300)]
        [JsonProperty(Order = 80)]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set the thumbnail uri.
        /// </summary>
        [JsonProperty(Order = 3)]
        public Uri? Thumbnail { get; set; }

        #endregion
    }
}