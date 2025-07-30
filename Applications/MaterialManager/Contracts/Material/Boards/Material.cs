using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <inheritdoc />
public class Material : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AdditionalData))]
    [JsonProperty(Order = 80)]
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// The average costs of the material, given in the subscriptions currency / subscription unit system.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_AverageCosts))]
    [JsonProperty(Order = 13)]
    public double? AverageCosts { get; set; }

    /// <summary>
    /// The board parameters for optimization.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_BoardParameterForOptimization))]
    [JsonProperty(Order = 16)]
    public ICollection<string> BoardParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// The coating category of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_CoatingCategory))]
    [JsonProperty(Order = 5)]
    public CoatingCategory CoatingCategory { get; set; }

    /// <summary>
    /// The material code.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Code))]
    [JsonProperty(Order = 1)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The decor codes of the boardTypes of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorCode))]
    [JsonProperty(Order = 9)]
    public ICollection<string> DecorCode { get; set; } = new List<string>();

    /// <summary>
    /// The decor names of the boardTypes of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_DecorName))]
    [JsonProperty(Order = 8)]
    public ICollection<string> DecorName { get; set; } = new List<string>();

    /// <summary>
    /// The density of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Density))]
    [JsonProperty(Order = 14)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Density { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the object has a grain.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_HasGrain))]
    [JsonProperty(Order = 4)]
    public bool HasGrain { get; set; }

    /// <summary>
    /// The manufacturer names of the boardTypes of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Manufacturer))]
    [JsonProperty(Order = 11)]
    public ICollection<string> Manufacturer { get; set; } = new List<string>();

    /// <summary>
    /// The master data comments of the boardTypes of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MasterDataComments))]
    [JsonProperty(Order = 12)]
    public ICollection<string> MasterDataComments { get; set; } = new List<string>();

    /// <summary>
    /// The material Category.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialCategory))]
    [JsonProperty(Order = 2)]
    public BoardMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// The material dependent parameters for optimization.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_MaterialParameterForOptimization))]
    [JsonProperty(Order = 15)]
    public ICollection<string> MaterialParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// The product names of the boardTypes of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_ProductName))]
    [JsonProperty(Order = 10)]
    public ICollection<string> ProductName { get; set; } = new List<string>();

    /// <summary>
    /// The standard quality of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_StandardQuality))]
    [JsonProperty(Order = 6)]
    public StandardQuality? StandardQuality { get; set; }

    /// <summary>
    /// The thickness of the material.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Thickness))]
    [JsonProperty(Order = 3)]
    public DoubleValue Thickness { get; set; } = new DoubleValue(null);

    /// <summary>
    /// Gets or set the thumbnail uri.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_Thumbnail))]
    [JsonProperty(Order = 7)]
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// The unit system used for the material properties.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MaterialProperties_UnitSystem))]
    [JsonProperty(Order = 50)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}