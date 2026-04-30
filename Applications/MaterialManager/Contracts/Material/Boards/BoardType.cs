#nullable enable
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Represents a board material type including material classification, dimensions, manufacturer data, inventory totals, and optimization settings.
    /// </summary>
    /// <example>
    /// { "boardCode": "P2_Gold_Craft_Oak_19.0", "materialCode": "P2_Gold_Craft_Oak", "thickness": 19.0, "materialCategory": "ParticleBoard", "coatingCategory": "MelamineResinCoated", "standardQuality": "P2", "width": 2070.0, "length": 2800.0, "grain": "Lengthwise", "costs": 12.45, "density": 650.0, "boardTypeType": "Stock", "manufacturerName": "HOMAG Sample Supplier", "productName": "Gold Craft Oak", "quantity": 12, "totalQuantityInInventory": 12, "unitSystem": "Metric" }
    /// </example>
    [DebuggerDisplay("{BoardCode}")]
    public class BoardType : IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization, ISupportsAdditionalProperties
    {
#pragma warning disable S109 // Magic numbers should not be used

        /// <summary>
        /// Gets or sets the date and time when the board type was last used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LastUsed))]
        [JsonProperty(Order = 90)]
        public DateTimeOffset? LastUsed { get; set; }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region ISupportsAdditionalProperties

        /// <inheritdoc/>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        #endregion

        #region Material

        /// <summary>
        /// Gets or sets the material code used to identify the material independent of board dimensions.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCode))]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 10)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the board thickness.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>19.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Thickness))]
        [JsonProperty(Order = 24)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the material category of the board.
        /// </summary>
        /// <example>ParticleBoard</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCategory))]
        [JsonProperty(Order = 12)]
        public BoardMaterialCategory MaterialCategory { get; set; }

        /// <summary>
        /// Gets or sets the coating category.
        /// </summary>
        /// <example>MelamineResinCoated</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_CoatingCategory))]
        [JsonProperty(Order = 13)]
        public CoatingCategory CoatingCategory { get; set; }

        /// <summary>
        /// Gets or sets the standard quality classification.
        /// </summary>
        /// <example>P2</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_StandardQuality))]
        [JsonProperty(Order = 14)]
        public StandardQuality StandardQuality { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the material was last used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialLastUsed))]
        [JsonProperty(Order = 15)]
        public DateTimeOffset? MaterialLastUsed { get; set; }

        #endregion

        #region Board type

        /// <summary>
        /// Gets or sets the unique board code including dimensional or variant-specific information.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardCode))]
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the board width.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>2070.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Width))]
        [Required]
        [Range(0.1, 19999.9)]
        [JsonProperty(Order = 22)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the board length.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>2800.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Length))]
        [Required]
        [Range(0.1, 19999.9)]
        [JsonProperty(Order = 23)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the grain direction of the board.
        /// </summary>
        /// <example>Lengthwise</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Grain))]
        [JsonProperty(Order = 25)]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the board cost per area unit.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: amount/m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: amount/ft².</para>
        /// </summary>
        /// <example>12.45</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Costs))]
        [JsonProperty(Order = 26)]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the board density.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: kg/m³.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: lb/ft³.</para>
        /// </summary>
        /// <example>650.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Density))]
        [JsonProperty(Order = 27)]
        [Range(0.1, 999999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
        public double? Density { get; set; }

        /// <summary>
        /// Gets the specific density or, if not set, the typical density derived from the material category.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: kg/m³.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: lb/ft³.</para>
        /// </summary>
        /// <example>650.0</example>
        [JsonProperty(Order = 28)]
        [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DensityOrCategoryTypical))]
        public double? DensityOrCategoryTypical
        {
            get
            {
                return Density ?? (MaterialCategory != BoardMaterialCategory.Undefined ? MaterialCategory.GetTypicalDensity(UnitSystem) : null);
            }
            private set => _ = value;
        }

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        /// <example>Stock</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardTypeType))]
        [JsonProperty(Order = 28)]
        public BoardTypeType BoardTypeType { get; set; }

        #endregion

        #region Manufacturer

        /// <summary>
        /// Gets or sets the manufacturer name.
        /// </summary>
        /// <example>HOMAG Sample Supplier</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ManufacturerName))]
        [JsonProperty(Order = 31)]
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <example>Gold Craft Oak</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ProductName))]
        [JsonProperty(Order = 32)]
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the article number.
        /// </summary>
        /// <example>ART-100200</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ArticleNumber))]
        [JsonProperty(Order = 33)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        /// <example>DCR-7788</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorCode))]
        [JsonProperty(Order = 34)]
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        /// <example>Craft Oak</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorName))]
        [JsonProperty(Order = 35)]
        public string? DecorName { get; set; }

        /// <summary>
        /// Gets or sets the GTIN.
        /// </summary>
        /// <example>04012345678901</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Gtin))]
        [JsonProperty(Order = 36)]
        public string? Gtin { get; set; }

        /// <summary>
        /// Gets or sets the embossing of the top decor side.
        /// </summary>
        /// <example>ST22</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingTop))]
        [JsonProperty(Order = 37)]
        public string? EmbossingTop { get; set; }

        /// <summary>
        /// Gets or sets the embossing of the bottom decor side.
        /// </summary>
        /// <example>ST10</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingBottom))]
        [JsonProperty(Order = 38)]
        public string? EmbossingBottom { get; set; }

        /// <summary>
        /// Gets or sets the identifier from an external system.
        /// </summary>
        /// <example>EXT-4711</example>
        [JsonProperty(Order = 95)]
        [Obsolete ("use ExternalSystemId instead", false)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the identifier from an external system.
        /// </summary>
        /// <example>EXT-4711</example>
        [JsonProperty(Order = 96)]
        public string? ExternalSystemId { get; set; }

        #endregion

        #region Material Management

        /// <summary>
        /// Gets or sets the warning threshold for total available quantity.
        /// </summary>
        /// <example>10</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAvailableWarningLimit))]
        [JsonProperty(Order = 53)]
        public int? TotalQuantityAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets the warning threshold for total available area.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
        /// </summary>
        /// <example>55.2</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAvailableWarningLimit))]
        [JsonProperty(Order = 54)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets whether the board type should be optimized against infinite stock.
        /// </summary>
        /// <example>true</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_OptimizeAgainstInfinite))]
        [JsonProperty(Order = 92)]
        public bool OptimizeAgainstInfinite { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the board type is locked for optimization.
        /// </summary>
        /// <example>false</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForOptimization))]
        [JsonProperty(Order = 93)]
        public bool LockedForOptimization { get; set; }

        /// <summary>
        /// Gets or sets whether the board type is locked for configuration.
        /// </summary>
        /// <example>false</example>
        [JsonProperty(Order = 94)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForConfiguration))]
        [BooleanValueDisplay(true, typeof(Resources), nameof(Resources.LockedForConfiguration_True))]
        [BooleanValueDisplay(false, typeof(Resources), nameof(Resources.LockedForConfiguration_False))]
        public bool LockedForConfiguration { get; set; }

        /// <summary>
        /// Gets the number of boards that should be ordered to cover shortages or meet the configured warning limit.
        /// </summary>
        /// <example>4</example>
        /// <remarks>
        /// This is a computed value.
        /// It reflects the greater of the current shortage caused by a negative <see cref="TotalQuantityAvailable" />
        /// and the quantity required to reach <see cref="TotalQuantityAvailableWarningLimit" />.
        /// </remarks>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_OrderDemand))]
        [JsonProperty(Order = 95)]
        [DefaultValue(0)]
        public int OrderDemand
        {
            get
            {
                var orderDemand = 0;

                if (TotalQuantityAvailable is < 0)
                {
                    orderDemand = -1 *TotalQuantityAvailable.Value;
                }

                if (TotalQuantityInInventory.HasValue && TotalQuantityAvailableWarningLimit.HasValue)
                {
                    if (TotalQuantityInInventory.Value < TotalQuantityAvailableWarningLimit.Value)
                    {
                        orderDemand = Math.Max(orderDemand, TotalQuantityAvailableWarningLimit.Value - TotalQuantityInInventory.Value);
                    }
                }

                return orderDemand;
            }
            private set => _ = value;
        }

        #endregion

        #region Inventory

        /// <summary>
        /// Gets or sets the total quantity of boards of this type in inventory.
        /// </summary>
        /// <example>12</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityInInventory))]
        [JsonProperty(Order = 50)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type allocated to production orders.
        /// </summary>
        /// <example>3</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAllocated))]
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Gets or sets the total quantity of boards of this type currently available in inventory.
        /// </summary>
        /// <example>9</example>
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
            private set => _ = value;
        }

        /// <summary>
        /// Gets the total inventory value of boards of this type.
        /// </summary>
        /// <example>112.05</example>
        [JsonProperty(Order = 53)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalValueInInventory))]
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
            private set => _ = value;
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type in inventory.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
        /// </summary>
        /// <example>69.55</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaInInventory))]
        [JsonProperty(Order = 56)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaInInventory
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
            }
            private set => _ = value;
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type allocated to production orders.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
        /// </summary>
        /// <example>17.39</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAllocated))]
        [JsonProperty(Order = 57)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAllocated
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAllocated);
            }
            private set => _ = value;
        }

        /// <summary>
        /// Gets or sets the total area of boards of this type currently available in inventory.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
        /// </summary>
        /// <example>52.16</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAvailable))]
        [JsonProperty(Order = 58)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAvailable
        {
            get
            {
                return UnitSystem.CalculateArea(Length, Width, TotalQuantityAvailable);
            }
            private set => _ = value;
        }

        /// <summary>
        /// Gets or sets whether <see cref="TotalQuantityAvailable" /> is below <see cref="TotalQuantityAvailableWarningLimit" />.
        /// </summary>
        /// <example>false</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_InsufficientInventory))]
        [JsonProperty(Order = 55)]
        public bool? InsufficientInventory { get; set; }

        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        /// <example>4012345678901</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Barcode))]
        [StringLength(50)]
        [JsonProperty(Order = 56)]
        public string Barcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the material parameter used for optimization.
        /// </summary>
        /// <example>QUALITY=A</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 57)]
        public string MaterialParameterForOptimization { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the board parameter used for optimization.
        /// </summary>
        /// <example>GRAIN=LENGTHWISE</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 58)]
        public string BoardParameterForOptimization { get; set; } = string.Empty;

        #endregion

        #region Additional data

        /// <summary>
        /// Gets or sets additional comments.
        /// </summary>
        /// <example>Preferred stock item for standard orders.</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Comments))]
        [StringLength(300)]
        [JsonProperty(Order = 80)]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thumbnail URI.
        /// </summary>
        /// <example>https://example.com/materials/boards/P2_Gold_Craft_Oak_19.0.png</example>
        [JsonProperty(Order = 3)]
        public Uri? Thumbnail { get; set; }

        #endregion

#pragma warning restore S109 // Magic numbers should not be used
    }
}