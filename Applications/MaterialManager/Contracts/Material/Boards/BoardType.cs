using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type.
    /// </summary>
    [DebuggerDisplay("{BoardCode}")]
    public class BoardType : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
#pragma warning disable S109 // Magic numbers should not be used

        /// <summary>
        /// Gets or sets the timestamp when board type has been used last.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LastUsed))]
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
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCode))]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 10)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thickness. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Thickness))]
        [JsonProperty(Order = 24)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the material category name
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCategory))]
        [JsonProperty(Order = 12)]
        public BoardMaterialCategory MaterialCategory { get; set; }

        /// <summary>
        /// Gets or sets the coating category.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_CoatingCategory))]
        [JsonProperty(Order = 13)]
        public CoatingCategory CoatingCategory { get; set; }

        /// <summary>
        /// Gets or set the standard quality.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_StandardQuality))]
        [JsonProperty(Order = 14)]
        public StandardQuality StandardQuality { get; set; }

        /// <summary>
        /// Gets or sets the material last used data.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialLastUsed))]
        [JsonProperty(Order = 15)]
        public DateTimeOffset? MaterialLastUsed { get; set; }

        #endregion

        #region Board type

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardCode))]
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the width of the board. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Width))]
        [Required]
        [Range(0.1, 19999.9)]
        [JsonProperty(Order = 22)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Length))]
        [Required]
        [Range(0.1, 19999.9)]
        [JsonProperty(Order = 23)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or set the grain of the board.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Grain))]
        [JsonProperty(Order = 25)]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the costs of the board. The unit depends on the settings of the subscription.
        /// (metric: amount/m², imperial: amount/ft²).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Costs))]
        [JsonProperty(Order = 26)]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the density of the board. The unit depends on the settings of the subscription.
        /// (metric: kg/m³, imperial: lb/ft³).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Density))]
        [JsonProperty(Order = 27)]
        [Range(0.1, 999999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
        public double? Density { get; set; }

        /// <summary>
        /// Gets the specific density or the typical density based on the material category if no specific density is set.
        /// </summary>
        [JsonProperty(Order = 28)]
        [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
        public double? DensityOrCategoryTypical
        {
            get
            {
                return Density ?? (MaterialCategory != BoardMaterialCategory.Undefined ? MaterialCategory.GetTypicalDensity(UnitSystem) : null);
            }
        }

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardTypeType))]
        [JsonProperty(Order = 28)]
        public BoardTypeType BoardTypeType { get; set; }

        #endregion

        #region Manufacturer

        /// <summary>
        /// Gets or sets the name of the manufacturer.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ManufacturerName))]
        [JsonProperty(Order = 31)]
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ProductName))]
        [JsonProperty(Order = 32)]
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the article number.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ArticleNumber))]
        [JsonProperty(Order = 33)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorCode))]
        [JsonProperty(Order = 34)]
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorName))]
        [JsonProperty(Order = 35)]
        public string? DecorName { get; set; }

        /// <summary>
        /// Gets or sets the gtin.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Gtin))]
        [JsonProperty(Order = 36)]
        public string? Gtin { get; set; }

        /// <summary>
        /// Gets or sets the decor top embossing.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingTop))]
        [JsonProperty(Order = 37)]
        public string? EmbossingTop { get; set; }

        /// <summary>
        /// Gets or sets the decor bottom embossing.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingBottom))]
        [JsonProperty(Order = 38)]
        public string? EmbossingBottom { get; set; }

        /// <summary>
        /// Gets or sets the id from an external system.
        /// </summary>
        [JsonProperty(Order = 95)]
        public string? ExternalId { get; set; }

        #endregion

        #region Material Management

        /// <summary>
        /// Gets or sets the total quantity available warning limit.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAvailableWarningLimit))]
        [JsonProperty(Order = 53)]
        public int? TotalQuantityAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets the total area available warning limit. The unit depends on the settings of the subscription
        /// (metric: m², imperial: ft²).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAvailableWarningLimit))]
        [JsonProperty(Order = 54)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets whether the board type should be optimized against infinite.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_OptimizeAgainstInfinite))]
        [JsonProperty(Order = 92)]
        public bool OptimizeAgainstInfinite { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the board type is locked for optimization.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForOptimization))]
        [JsonProperty(Order = 93)]
        public bool LockedForOptimization { get; set; }

        /// <summary>
        /// Gets or sets whether the board type is locked for configuration.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForConfiguration))]
        [JsonProperty(Order = 94)]
        public bool LockedForConfiguration { get; set; }

        #endregion

        #region Inventory

        /// <summary>
        /// Gets or sets the total quantity of boards of this type in the inventory.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityInInventory))]
        [JsonProperty(Order = 50)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which have been allocated to a production order.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAllocated))]
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which are available in the inventory.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAvailable))]
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
        /// Gets the total value of boards of this type in inventory
        /// </summary>
        [JsonProperty(Order = 53)]
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

        /// <summary>
        /// Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the
        /// subscription (metric: m², imperial: ft²).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaInInventory))]
        [JsonProperty(Order = 56)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaInInventory
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on
        /// the settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAllocated))]
        [JsonProperty(Order = 57)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAllocated
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAllocated);
            }
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the
        /// settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAvailable))]
        [JsonProperty(Order = 58)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAvailable
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAvailable);
            }
        }

        /// <summary>
        /// Gets or sets an indication whether the <see cref="TotalQuantityAvailable" /> is below the defined limit
        /// <see cref="TotalQuantityAvailableWarningLimit" />.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_InsufficientInventory))]
        [JsonProperty(Order = 55)]
        public bool? InsufficientInventory { get; set; }

        /// <summary>
        /// Gets or sets the Barcode.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Barcode))]
        [StringLength(50)]
        [JsonProperty(Order = 56)]
        public string Barcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MaterialParameterForOptimization.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 57)]
        public string MaterialParameterForOptimization { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MaterialParameterForOptimization.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 58)]
        public string BoardParameterForOptimization { get; set; } = string.Empty;

        #endregion

        #region Additional data

        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Comments))]
        [StringLength(300)]
        [JsonProperty(Order = 80)]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set the thumbnail uri.
        /// </summary>
        [JsonProperty(Order = 3)]
        public Uri? Thumbnail { get; set; }

        #endregion

#pragma warning restore S109 // Magic numbers should not be used
    }
}