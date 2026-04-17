using System;
using System.Collections.Generic;
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
/// Represents aggregated material information for board materials, including classification, optimization parameters, and unit-dependent values.
/// </summary>
/// <example>
/// { "code": "P2_Gold_Craft_Oak", "materialCategory": "ParticleBoard", "thickness": 19.0, "hasGrain": true, "coatingCategory": "MelamineResinCoated", "standardQuality": "P2", "decorName": [ "Craft Oak" ], "decorCode": [ "DCR-7788" ], "thumbnail": "https://example.com/materials/P2_Gold_Craft_Oak.png", "manufacturer": [ "HOMAG Sample Supplier" ], "productName": [ "Gold Craft Oak" ], "masterDataComments": [ "Preferred stock item" ], "averageCosts": 12.45, "density": 650.0, "materialParameterForOptimization": [ "QUALITY=A" ], "boardParameterForOptimization": [ "GRAIN=LENGTHWISE" ], "unitSystem": "Metric" }
/// </example>
public class Material : IContainsUnitSystemDependentProperties, ISupportsAdditionalData
{
    /// <summary>
    /// Gets or sets the additional data entries associated with the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AdditionalData))]
    [JsonProperty(Order = 80)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the average material cost in the subscription currency and subscription unit system.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: amount/m².</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: amount/ft².</para>
    /// </summary>
    /// <example>12.45</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AverageCosts))]
    [JsonProperty(Order = 13)]
    public double? AverageCosts { get; set; }

    /// <summary>
    /// Gets or sets the board parameters used for optimization.
    /// </summary>
    /// <example>[ "GRAIN=LENGTHWISE" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
    [JsonProperty(Order = 16)]
    public ICollection<string> BoardParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the coating category of the material.
    /// </summary>
    /// <example>MelamineResinCoated</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_CoatingCategory))]
    [JsonProperty(Order = 5)]
    public CoatingCategory CoatingCategory { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    /// <example>P2_Gold_Craft_Oak</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Code))]
    [JsonProperty(Order = 1)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the decor codes used by board types of the material.
    /// </summary>
    /// <example>[ "DCR-7788" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorCode))]
    [JsonProperty(Order = 9)]
    public ICollection<string> DecorCode { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the decor names used by board types of the material.
    /// </summary>
    /// <example>[ "Craft Oak" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorName))]
    [JsonProperty(Order = 8)]
    public ICollection<string> DecorName { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the material density.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: kg/m³.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: lb/ft³.</para>
    /// </summary>
    /// <example>650.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Density))]
    [JsonProperty(Order = 14)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Density { get; set; }

    /// <summary>
    /// Gets or sets whether the material has a grain direction.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 4)]
    public bool HasGrain { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer names used by board types of the material.
    /// </summary>
    /// <example>[ "HOMAG Sample Supplier" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Manufacturer))]
    [JsonProperty(Order = 11)]
    public ICollection<string> Manufacturer { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the master data comments used by board types of the material.
    /// </summary>
    /// <example>[ "Preferred stock item" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MasterDataComments))]
    [JsonProperty(Order = 12)]
    public ICollection<string> MasterDataComments { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the material category.
    /// </summary>
    /// <example>ParticleBoard</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialCategory))]
    [JsonProperty(Order = 2)]
    public BoardMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// Gets or sets the material-dependent parameters used for optimization.
    /// </summary>
    /// <example>[ "QUALITY=A" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
    [JsonProperty(Order = 15)]
    public ICollection<string> MaterialParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the product names used by board types of the material.
    /// </summary>
    /// <example>[ "Gold Craft Oak" ]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_ProductName))]
    [JsonProperty(Order = 10)]
    public ICollection<string> ProductName { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the standard quality classification of the material.
    /// </summary>
    /// <example>P2</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_StandardQuality))]
    [JsonProperty(Order = 6)]
    public StandardQuality? StandardQuality { get; set; }

    /// <summary>
    /// Gets or sets the material thickness.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>19.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Thickness))]
    [JsonProperty(Order = 3)]
    public DoubleValue Thickness { get; set; } = new DoubleValue(null);

    /// <summary>
    /// Gets or sets the thumbnail URI.
    /// </summary>
    /// <example>https://example.com/materials/P2_Gold_Craft_Oak.png</example>
    [JsonProperty(Order = 7)]
    public Uri? Thumbnail { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 50)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    /// <inheritdoc/>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}