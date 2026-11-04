using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// A board material groups interchangeable board types that share core material characteristics, such as category and
/// surface coating.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-platten">Learn more about board materials</see>.
/// </summary>
/// <example>
/// {
/// "code": "MAT-001-19",
/// "materialCategory": "Chipboard",
/// "thickness": { "value": 19.0, "unitSystem": "Metric" },
/// "hasGrain": true,
/// "coatingCategory": "MelamineThermoset",
/// "standardQuality": "RawCut",
/// "decorName": [ "Oak" ],
/// "decorCode": [ "DCR-001" ],
/// "manufacturer": [ "Example Supplier" ],
/// "productName": [ "Oak sample board" ],
/// "unitSystem": "Metric"
/// }
/// </example>
public class Material : IContainsUnitSystemDependentProperties, ISupportsAdditionalData
{
    /// <summary>
    /// Average cost per unit area across board types belonging to this material.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: amount/m².</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: amount/ft².</para>
    /// </summary>
    /// <example>12.45</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AverageCosts))]
    [JsonProperty(Order = 13)]
    public double? AverageCosts { get; set; }

    /// <summary>
    /// Board-specific optimization parameters collected from this material's board types.
    /// </summary>
    /// <example>[ "GRAIN=LENGTHWISE" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
    [JsonProperty(Order = 16)]
    public ICollection<string> BoardParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// Surface coating shared by the board types belonging to this material.
    /// </summary>
    /// <example>MelamineThermoset</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_CoatingCategory))]
    [JsonProperty(Order = 5)]
    public CoatingCategory CoatingCategory { get; set; }

    /// <summary>
    /// Code identifying the shared material, distinct from the codes of its individual board formats.
    /// </summary>
    /// <example>MAT-001-19</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Code))]
    [JsonProperty(Order = 1)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Decorative-design codes collected from this material's board types.
    /// </summary>
    /// <example>[ "DCR-001" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorCode))]
    [JsonProperty(Order = 9)]
    public ICollection<string> DecorCode { get; set; } = new List<string>();

    /// <summary>
    /// Decorative-design names collected from this material's board types.
    /// </summary>
    /// <example>[ "Oak" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorName))]
    [JsonProperty(Order = 8)]
    public ICollection<string> DecorName { get; set; } = new List<string>();

    /// <summary>
    /// Mass per unit volume of the board material, when known.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: kg/m³.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: lb/ft³.</para>
    /// </summary>
    /// <example>650.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Density))]
    [JsonProperty(Order = 14)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Density { get; set; }

    /// <summary>
    /// Whether this material has a grain direction that affects part orientation.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 4)]
    public bool HasGrain { get; set; }

    /// <summary>
    /// Manufacturer names collected from this material's board types.
    /// </summary>
    /// <example>[ "Example Supplier" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Manufacturer))]
    [JsonProperty(Order = 11)]
    public ICollection<string> Manufacturer { get; set; } = new List<string>();

    /// <summary>
    /// Master-data notes collected from this material's board types.
    /// </summary>
    /// <example>[ "Preferred stock item" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MasterDataComments))]
    [JsonProperty(Order = 12)]
    public ICollection<string> MasterDataComments { get; set; } = new List<string>();

    /// <summary>
    /// Base material shared by the board types in this group.
    /// </summary>
    /// <example>Chipboard</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialCategory))]
    [JsonProperty(Order = 2)]
    public BoardMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// Material-level optimization parameters collected from this material's board types.
    /// </summary>
    /// <example>[ "QUALITY=A" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
    [JsonProperty(Order = 15)]
    public ICollection<string> MaterialParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// Product names collected from this material's board types.
    /// </summary>
    /// <example>[ "Oak sample board" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_ProductName))]
    [JsonProperty(Order = 10)]
    public ICollection<string> ProductName { get; set; } = new List<string>();

    /// <summary>
    /// Standard cut quality associated with the material, when specified.
    /// </summary>
    /// <example>RawCut</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_StandardQuality))]
    [JsonProperty(Order = 6)]
    public StandardQuality? StandardQuality { get; set; }

    /// <summary>
    /// Thickness measurement with an optional tolerance and its own unit system.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>{ "value": 19.0, "unitSystem": "Metric" }</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Thickness))]
    [JsonProperty(Order = 3)]
    public DoubleValue Thickness { get; set; } = new DoubleValue(null);

    /// <summary>
    /// Image used to help identify the shared material.
    /// </summary>
    /// <example>https://example.com/materials/MAT-001-19.png</example>
    [JsonProperty(Order = 7)]
    public Uri? Thumbnail { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 50)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    /// <summary>
    /// Supporting data associated with the shared material, such as documents or images.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AdditionalData))]
    [JsonProperty(Order = 80)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <inheritdoc />
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}