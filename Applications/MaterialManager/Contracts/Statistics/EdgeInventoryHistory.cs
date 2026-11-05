using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Statistics;

/// <summary>
/// Represents historical inventory data for an edgeband at a specific point in time, including unit-dependent values and
/// optional custom properties.
/// </summary>
/// <example>
/// { "timestamp": "2025-04-09T08:15:00+00:00", "edgebandCode": "ABS_White_23x1.0", "costs": 0.42, "height": 23.0,
/// "totalLengthInInventory": 1250.0, "unitSystem": "Metric", "supplierBatch": "BATCH-2025-04" }
/// </example>
public class EdgeInventoryHistory : IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization, ISupportsAdditionalProperties
{
    private const int _EdgebandCodeMaxLength = 50;
    private const double _HeightDimensionMinValue = 0.1;
    private const double _HeightDimensionMaxValue = 9999.9;

    /// <summary>
    /// Edgeband cost per length unit, when known.
    /// <para>Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: amount/m.</para>
    /// <para>Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Imperial" />: amount/ft.</para>
    /// </summary>
    /// <example>0.42</example>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.Costs))]
    public double? Costs { get; set; }

    /// <summary>
    /// Code identifying the edgeband type represented by this inventory snapshot.
    /// </summary>
    /// <example>ABS_White_23x1.0</example>
    [Key]
    [Required]
    [StringLength(_EdgebandCodeMaxLength, MinimumLength = 1)]
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.EdgebandCode))]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Edgeband height; the nullable C# type permits null, but validation requires a value.
    /// <para>Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>23.0</example>
    [Required]
    [Range(_HeightDimensionMinValue, _HeightDimensionMaxValue)]
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.Height))]
    public double? Height { get; set; }

    /// <summary>
    /// Date and time represented by this inventory snapshot.
    /// </summary>
    /// <example>2025-04-09T08:15:00+00:00</example>
    [Required]
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Timestamp))]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Total edgeband length in this inventory snapshot; validation requires a value despite the nullable C# type.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    /// <example>1250.0</example>
    [Required]
    [JsonProperty(Order = 5)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TotalLengthInInventory))]
    public double? TotalLengthInInventory { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UnitSystem))]
    [DefaultValue(UnitSystem.Metric)]
    public UnitSystem UnitSystem { get; set; }

    /// <summary>
    /// Custom inventory values serialized as properties at the JSON root, not inside an AdditionalProperties object.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}