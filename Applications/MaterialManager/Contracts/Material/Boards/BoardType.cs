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
    /// A board type represents a specific sheet material format managed in materialManager, such as a stock sheet or offcut, and is identified separately from its shared material group.
    /// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-platten">Learn more about board types</see>.
    /// </summary>
    /// <example>
    /// { "boardCode": "P2_Gold_Craft_Oak_19.0_2800_2070", "materialCode": "P2_Gold_Craft_Oak_19.0", "thickness": 19.0, "materialCategory": "Chipboard", "coatingCategory": "MelamineThermoset", "standardQuality": "RawCut", "width": 2070.0, "length": 2800.0, "grain": "Lengthwise", "costs": 12.45, "density": 650.0, "boardTypeType": "Board", "manufacturerName": "Egger", "productName": "Gold Craft Oak", "decorName": "f004", "embossingTop": "st07", "totalQuantityInInventory": 12, "unitSystem": "Metric" }
    /// </example>
    [DebuggerDisplay("{BoardCode}")]
    public class BoardType : IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization, ISupportsAdditionalProperties
    {
#pragma warning disable S109 // Magic numbers should not be used

        /// <summary>
        /// The date and time when this specific board type was most recently used, tracked separately from when its shared material group was last used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LastUsed))]
        [JsonProperty(Order = 90)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? LastUsed { get; set; }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [DefaultValue(UnitSystem.Metric)]
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
        /// Code that identifies the shared material and may include its thickness; the board code distinguishes formats by length and width.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCode))]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 10)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Thickness of the board.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>19.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Thickness))]
        [JsonProperty(Order = 24)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Base material the board is made from, used to identify relevant processing parameters.
        /// </summary>
        /// <example>Chipboard</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCategory))]
        [DefaultValue(BoardMaterialCategory.AcrylicCompositeMaterials)]
        [JsonProperty(Order = 12)]
        public BoardMaterialCategory MaterialCategory { get; set; } = BoardMaterialCategory.AcrylicCompositeMaterials;

        /// <summary>
        /// Surface coating of the board, which can affect how the material is processed. When no value is specified, it defaults to <c>Undefined</c>.
        /// </summary>
        /// <example>MelamineThermoset</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_CoatingCategory))]
        [DefaultValue(CoatingCategory.Undefined)]
        [JsonProperty(Order = 13)]
        public CoatingCategory CoatingCategory { get; set; } = CoatingCategory.Undefined;

        /// <summary>
        /// Standard cut quality assigned to the board. When no value is specified, it defaults to <c>RawCut</c>.
        /// </summary>
        /// <example>RawCut</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_StandardQuality))]
        [DefaultValue(StandardQuality.RawCut)]
        [JsonProperty(Order = 14)]
        public StandardQuality StandardQuality { get; set; } = StandardQuality.RawCut;

        /// <summary>
        /// Date and time when the shared material group was most recently used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialLastUsed))]
        [JsonProperty(Order = 15)]
        public DateTimeOffset? MaterialLastUsed { get; set; }

        #endregion

        #region Board type

        /// <summary>
        /// Unique code for this board format or variant, often including dimensional information.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0_2800_2070</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardCode))]
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Width of the board.
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
        /// Length of the board.
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
        /// Grain direction, which guides how parts can be oriented on the board during optimization.
        /// </summary>
        /// <example>Lengthwise</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Grain))]
        [DefaultValue(Grain.None)]
        [JsonProperty(Order = 25)]
        public Grain Grain { get; set; } = Grain.None;

        /// <summary>
        /// Cost per unit area for this board type, used to evaluate material value during optimization.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: amount/m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: amount/ft².</para>
        /// </summary>
        /// <example>12.45</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Costs))]
        [JsonProperty(Order = 26)]
        public double? Costs { get; set; }

        /// <summary>
        /// Mass per unit volume of the board material.
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
        /// Density used for calculations: the specified value, or the typical value for the material category when none is provided.
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
        /// Classification that determines how this board type is handled in material management and optimization.
        /// </summary>
        /// <example>Board</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardTypeType))]
        [DefaultValue(BoardTypeType.Board)]
        [JsonProperty(Order = 28)]
        public BoardTypeType BoardTypeType { get; set; } = BoardTypeType.Board;

        #endregion

        #region Manufacturer

        /// <summary>
        /// Name of the company that manufactures the board material.
        /// </summary>
        /// <example>HOMAG Sample Supplier</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ManufacturerName))]
        [JsonProperty(Order = 31)]
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Product name used by the manufacturer or supplier for this board material.
        /// </summary>
        /// <example>Gold Craft Oak</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ProductName))]
        [JsonProperty(Order = 32)]
        public string? ProductName { get; set; }

        /// <summary>
        /// Manufacturer's article number for identifying or ordering the product.
        /// </summary>
        /// <example>ART-100200</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_ArticleNumber))]
        [JsonProperty(Order = 33)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Code identifying the board's surface decor.
        /// </summary>
        /// <example>DCR-7788</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorCode))]
        [JsonProperty(Order = 34)]
        public string? DecorCode { get; set; }

        /// <summary>
        /// Name of the board's surface decor.
        /// </summary>
        /// <example>Craft Oak</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorName))]
        [JsonProperty(Order = 35)]
        public string? DecorName { get; set; }

        /// <summary>
        /// Global Trade Item Number used to identify this product in the supply chain.
        /// </summary>
        /// <example>04012345678901</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Gtin))]
        [JsonProperty(Order = 36)]
        public string? Gtin { get; set; }

        /// <summary>
        /// Embossing or texture pattern on the top decor side.
        /// </summary>
        /// <example>ST22</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingTop))]
        [JsonProperty(Order = 37)]
        public string? EmbossingTop { get; set; }

        /// <summary>
        /// Embossing or texture pattern on the bottom decor side.
        /// </summary>
        /// <example>ST10</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_EmbossingBottom))]
        [JsonProperty(Order = 38)]
        public string? EmbossingBottom { get; set; }

        /// <summary>
        /// Identifier associated with the board's decor.
        /// </summary>
        /// <example>ST10</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_DecorId))]
        [JsonProperty(Order = 39)]
        public string? DecorId { get; set; }

        /// <summary>
        /// Legacy identifier used by an external system to refer to this board type.
        /// </summary>
        /// <example>EXT-4711</example>
        [JsonProperty(Order = 95)]
        [Obsolete ("use ExternalSystemId instead", false)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// Identifier used by an external system to refer to this board type.
        /// </summary>
        /// <example>EXT-4711</example>
        [JsonProperty(Order = 96)]
        public string? ExternalSystemId { get; set; }

        #endregion

        #region Material Management

        /// <summary>
        /// Minimum available board quantity that triggers a low-stock warning.
        /// </summary>
        /// <example>10</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAvailableWarningLimit))]
        [JsonProperty(Order = 53)]
        public int? TotalQuantityAvailableWarningLimit { get; set; }

        /// <summary>
        /// Minimum available board area that triggers a low-stock warning.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
        /// </summary>
        /// <example>55.2</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalAreaAvailableWarningLimit))]
        [JsonProperty(Order = 54)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double? TotalAreaAvailableWarningLimit { get; set; }

        /// <summary>
        /// Whether optimization treats this board type as unlimited stock instead of using its recorded quantity.
        /// </summary>
        /// <example>true</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_OptimizeAgainstInfinite))]
        [JsonProperty(Order = 92)]
        public bool OptimizeAgainstInfinite { get; set; } = true;

        /// <summary>
        /// Whether this board type is excluded from optimization.
        /// </summary>
        /// <example>false</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForOptimization))]
        [JsonProperty(Order = 93)]
        public bool LockedForOptimization { get; set; }

        /// <summary>
        /// Whether this board type is protected from configuration changes.
        /// </summary>
        /// <example>false</example>
        [JsonProperty(Order = 94)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LockedForConfiguration))]
        [BooleanValueDisplay(true, typeof(Resources), nameof(Resources.LockedForConfiguration_True))]
        [BooleanValueDisplay(false, typeof(Resources), nameof(Resources.LockedForConfiguration_False))]
        public bool LockedForConfiguration { get; set; }

        /// <summary>
        /// Number of boards to order to cover a shortage or reach the configured warning limit.
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
        /// Total number of boards of this type currently recorded in inventory.
        /// </summary>
        /// <example>12</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityInInventory))]
        [JsonProperty(Order = 50)]
        public int? TotalQuantityInInventory { get; set; }

        /// <summary>
        /// Number of boards of this type reserved for production orders.
        /// </summary>
        /// <example>3</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAllocated))]
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; }

        /// <summary>
        /// Number of boards remaining for use after production allocations are accounted for.
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
        /// Estimated value of the current stock, calculated from its total area and the board's unit-area cost.
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
        /// Combined area of all boards of this type currently in inventory.
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
        /// Combined area of this board type reserved for production orders.
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
        /// Combined area remaining for use after production allocations are accounted for.
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
        /// Whether the available quantity is below the configured low-stock warning limit.
        /// </summary>
        /// <example>false</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_InsufficientInventory))]
        [JsonProperty(Order = 55)]
        public bool? InsufficientInventory { get; set; }

        /// <summary>
        /// Barcode operators can scan to identify this board type.
        /// </summary>
        /// <example>4012345678901</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Barcode))]
        [StringLength(50)]
        [JsonProperty(Order = 56)]
        public string Barcode { get; set; } = string.Empty;

        /// <summary>
        /// Material-level parameter supplied to the cutting optimization for this board type.
        /// </summary>
        /// <example>QUALITY=A</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 57)]
        public string MaterialParameterForOptimization { get; set; } = string.Empty;

        /// <summary>
        /// Board-specific parameter supplied to the cutting optimization.
        /// </summary>
        /// <example>GRAIN=LENGTHWISE</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
        [StringLength(300)]
        [JsonProperty(Order = 58)]
        public string BoardParameterForOptimization { get; set; } = string.Empty;

        #endregion

        #region Additional data

        /// <summary>
        /// Notes about this board type, such as handling or purchasing guidance.
        /// </summary>
        /// <example>Preferred stock item for standard orders.</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Comments))]
        [StringLength(300)]
        [JsonProperty(Order = 80)]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// Image used to help users visually identify this board type.
        /// </summary>
        /// <example>https://example.com/materials/boards/P2_Gold_Craft_Oak_19.0.png</example>
        [JsonProperty(Order = 3)]
        public Uri? Thumbnail { get; set; }

        #endregion

#pragma warning restore S109 // Magic numbers should not be used
    }
}