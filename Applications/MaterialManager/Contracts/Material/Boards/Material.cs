using System;
using System.Collections.Generic;

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
    [JsonProperty(Order = 80)]
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// The average costs of the material, given in the subscriptions currency / subscription unit system.
    /// </summary>
    [JsonProperty(Order = 13)]
    public double? AverageCosts { get; set; }

    /// <summary>
    /// The board parameters for optimization.
    /// </summary>
    [JsonProperty(Order = 16)]
    public ICollection<string> BoardParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// The coating category of the material.
    /// </summary>
    [JsonProperty(Order = 5)]
    public CoatingCategory CoatingCategory { get; set; }

    /// <summary>
    /// The material code.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The decor codes of the boardTypes of the material.
    /// </summary>
    [JsonProperty(Order = 9)]
    public ICollection<string> DecorCode { get; set; } = new List<string>();

    /// <summary>
    /// The decor names of the boardTypes of the material.
    /// </summary>
    [JsonProperty(Order = 8)]
    public ICollection<string> DecorName { get; set; } = new List<string>();

    /// <summary>
    /// The density of the material.
    /// </summary>
    [JsonProperty(Order = 14)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Density { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the object has a grain.
    /// </summary>
    [JsonProperty(Order = 4)]
    public bool HasGrain { get; set; }

    /// <summary>
    /// The manufacturer names of the boardTypes of the material.
    /// </summary>
    [JsonProperty(Order = 11)]
    public ICollection<string> Manufacturer { get; set; } = new List<string>();

    /// <summary>
    /// The master data comments of the boardTypes of the material.
    /// </summary>
    [JsonProperty(Order = 12)]
    public ICollection<string> MasterDataComments { get; set; } = new List<string>();

    /// <summary>
    /// The material Category.
    /// </summary>
    [JsonProperty(Order = 2)]
    public BoardMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// The material dependent parameters for optimization.
    /// </summary>
    [JsonProperty(Order = 15)]
    public ICollection<string> MaterialParameterForOptimization { get; set; } = new List<string>();

    /// <summary>
    /// The product names of the boardTypes of the material.
    /// </summary>
    [JsonProperty(Order = 10)]
    public ICollection<string> ProductName { get; set; } = new List<string>();

    /// <summary>
    /// The standard quality of the material.
    /// </summary>
    [JsonProperty(Order = 6)]
    public StandardQuality? StandardQuality { get; set; }

    /// <summary>
    /// The thickness of the material.
    /// </summary>
    [JsonProperty(Order = 3)]
    public DoubleValue Thickness { get; set; } = new DoubleValue(null);

    /// <summary>
    /// Gets or set the thumbnail uri.
    /// </summary>
    [JsonProperty(Order = 7)]
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// The unit system used for the material properties.
    /// </summary>
    [JsonProperty(Order = 50)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}