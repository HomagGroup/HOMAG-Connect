using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Statistics;

/// <summary>
/// Represents historical inventory totals for a board type at a specific point in time, including unit-dependent values
/// and optional custom properties.
/// </summary>
/// <example>
/// { "timestamp": "2025-04-09T08:15:00+00:00", "materialCode": "P2_Gold_Craft_Oak", "boardCode": "P2_Gold_Craft_Oak_19.0",
/// "boardTypeType": "Board", "length": 2800.0, "width": 2070.0, "costs": 12.45, "totalQuantityInInventory": 12,
/// "totalQuantityAllocated": 3, "totalQuantityAvailable": 9, "totalAreaInInventory": 69.55, "totalAreaAllocated": 17.39,
/// "totalAreaAvailable": 52.16, "totalValueInInventory": 865.8975, "unitSystem": "Metric", "supplierBatch":
/// "BATCH-2025-04" }
/// </example>
public class BoardTypeInventoryHistory : IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization, ISupportsAdditionalProperties
{
    private const int _BoardCodeMaxLength = 50;
    private const int _MaterialCodeMaxLength = 50;
    private const double _LengthDimensionMinValue = 0.1;
    private const double _LengthDimensionMaxValue = 19999.9;

    /// <summary>
    /// Code identifying the board format represented by this inventory snapshot.
    /// </summary>
    /// <example>P2_Gold_Craft_Oak_19.0</example>
    [Key]
    [Required]
    [StringLength(_BoardCodeMaxLength, MinimumLength = 1)]
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardCode))]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Whether the board format represents stock material, an offcut, or a template.
    /// </summary>
    /// <example>Board</example>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_BoardTypeType))]
    public BoardTypeType BoardTypeType { get; set; }

    /// <summary>
    /// Board cost per area unit, when known.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: amount/m².</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: amount/ft².</para>
    /// </summary>
    /// <example>12.45</example>
    [JsonProperty(Order = 6)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Costs))]
    public double? Costs { get; set; }

    /// <summary>
    /// Length of one board; the nullable C# type permits null, but validation requires a value.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2800.0</example>
    [Required]
    [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Length))]
    public double? Length { get; set; }

    /// <summary>
    /// Code of the shared material to which this board format belongs.
    /// </summary>
    /// <example>P2_Gold_Craft_Oak</example>
    [Required]
    [StringLength(_MaterialCodeMaxLength, MinimumLength = 1)]
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_MaterialCode))]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// Date and time represented by this inventory snapshot.
    /// </summary>
    /// <example>2025-04-09T08:15:00+00:00</example>
    [Required]
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Base.Contracts.Resources.Timestamp))]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Reserved area calculated from Length, Width, and TotalQuantityAllocated, or null if an input is unknown.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
    /// </summary>
    /// <example>17.39</example>
    [JsonProperty(Order = 21)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TotalAreaAllocated))]
    public double? TotalAreaAllocated
    {
        get
        {
            return UnitSystem.CalculateArea(Length, Width, TotalQuantityAllocated);
        }
    }

    /// <summary>
    /// Available area calculated from Length, Width, and TotalQuantityAvailable, or null if an input is unknown.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
    /// </summary>
    /// <example>52.16</example>
    [JsonProperty(Order = 22)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TotalAreaAvailable))]
    public double? TotalAreaAvailable
    {
        get
        {
            return UnitSystem.CalculateArea(Length, Width, TotalQuantityAvailable);
        }
    }

    /// <summary>
    /// Inventory area calculated from Length, Width, and TotalQuantityInInventory, or null if an input is unknown.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m².</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft².</para>
    /// </summary>
    /// <example>69.55</example>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TotalAreaInInventory))]
    public double? TotalAreaInInventory
    {
        get
        {
            return UnitSystem.CalculateArea(Length, Width, TotalQuantityInInventory);
        }
    }

    /// <summary>
    /// Number of boards reserved for production, when known.
    /// </summary>
    /// <example>3</example>
    [JsonProperty(Order = 11)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAllocated))]
    public int? TotalQuantityAllocated { get; set; }

    /// <summary>
    /// Boards available after subtracting TotalQuantityAllocated from TotalQuantityInInventory, or null if either is unknown.
    /// </summary>
    /// <example>9</example>
    [JsonProperty(Order = 12)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityAvailable))]
    public int? TotalQuantityAvailable
    {
        get
        {
            if (TotalQuantityInInventory.HasValue && TotalQuantityAllocated.HasValue)
            {
                return TotalQuantityInInventory.Value - TotalQuantityAllocated.Value;
            }

            return null;
        }
    }

    /// <summary>
    /// Number of boards held in inventory, when known.
    /// </summary>
    /// <example>12</example>
    [JsonProperty(Order = 10)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_TotalQuantityInInventory))]
    public int? TotalQuantityInInventory { get; set; }

    /// <summary>
    /// Inventory value calculated as Costs multiplied by TotalAreaInInventory, or null if either is unknown.
    /// </summary>
    /// <example>865.8975</example>
    [JsonProperty(Order = 30)]
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
    }

    /// <summary>
    /// Width of one board; the nullable C# type permits null, but validation requires a value.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2070.0</example>
    [Required]
    [Range(_LengthDimensionMinValue, _LengthDimensionMaxValue)]
    [JsonProperty(Order = 5)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeProperties_Width))]
    public double? Width { get; set; }

    /// <inheritdoc />
    [DefaultValue(UnitSystem.Metric)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Base.Contracts.Resources.UnitSystem))]
    public UnitSystem UnitSystem { get; set; }

    /// <summary>
    /// Custom inventory values serialized as properties at the JSON root, not inside an AdditionalProperties object.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}