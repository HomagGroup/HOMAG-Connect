#nullable enable
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Represents a board material type including master data and inventory totals.
    /// </summary>
    /// <example>
    /// { "boardCode": "P2_Gold_Craft_Oak_19.0", "materialCode": "P2_Gold_Craft_Oak", "thickness": 19.0, "materialCategory": "ParticleBoard", "coatingCategory": "MelamineResinCoated", "standardQuality": "P2", "width": 2070.0, "length": 2800.0, "grain": "Lengthwise", "costs": 12.45, "density": 650.0, "boardTypeType": "Stock", "manufacturerName": "HOMAG Sample Supplier", "productName": "Gold Craft Oak", "quantity": 12, "totalQuantityInInventory": 12, "unitSystem": "Metric" }
    /// </example>
    [DebuggerDisplay("{BoardCode}")]
    public class BoardType : BoardTypeMasterData
    {
#pragma warning disable S109 // Magic numbers should not be used

        #region Inventory

        /// <summary>
        /// Gets or sets the date and time when the board type was last used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_LastUsed))]
        [JsonProperty(Order = 90)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? LastUsed { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the material was last used.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialLastUsed))]
        [JsonProperty(Order = 15)]
        public DateTimeOffset? MaterialLastUsed { get; set; }

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
        /// Gets or sets whether <see cref="TotalQuantityAvailable" /> is below <see cref="BoardTypeMasterData.TotalQuantityAvailableWarningLimit" />.
        /// </summary>
        /// <example>false</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_InsufficientInventory))]
        [JsonProperty(Order = 55)]
        public bool? InsufficientInventory { get; set; }

        /// <summary>
        /// Gets the number of boards that should be ordered to cover shortages or meet the configured warning limit.
        /// </summary>
        /// <example>4</example>
        /// <remarks>
        /// This is a computed value.
        /// It reflects the greater of the current shortage caused by a negative <see cref="TotalQuantityAvailable" />
        /// and the quantity required to reach <see cref="BoardTypeMasterData.TotalQuantityAvailableWarningLimit" />.
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

#pragma warning restore S109 // Magic numbers should not be used
    }
}