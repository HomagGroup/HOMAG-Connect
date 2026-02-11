#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Base class for board and offcut.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(SolutionMaterialBoard), BoardTypeType.Board)]
[JsonSubtypes.KnownSubType(typeof(SolutionMaterialOffcut), BoardTypeType.Offcut)]
[JsonSubtypes.KnownSubType(typeof(SolutionMaterialTemplate), BoardTypeType.Template)]
public class SolutionMaterialBoardBase : IDimensionProperties, IMaterialProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    [JsonProperty(Order = 2)]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode
    {
        get;
        set
        {
            field = value.Trimmed();
        }
    } = string.Empty;

    /// <summary>
    /// Gets or sets the total costs.
    /// </summary>
    [JsonProperty(Order = 7)]
    public double? Costs { get; set; }

    /// <summary>
    /// Gets or sets the demand.
    /// </summary>
    [JsonProperty(Order = 6)]
    [Range(0,int.MaxValue)]
    public int Demand { get; set; }

    /// <inheritdoc/>
    [JsonProperty(Order = 3)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MaterialCode
    {
        get;
        set
        {
            field = value.Trimmed();
        }
    } = string.Empty;

    /// <inheritdoc/>
    [JsonProperty(Order = 5)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the board type.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual BoardTypeType Type { get; set; }

    /// <inheritdoc/>
    [JsonProperty(Order = 4)]
    public double? Width { get; set; }


    /// <inheritdoc/>
    public Grain Grain { get; set; }

    /// <inheritdoc/>
    [JsonIgnore]
    public string? Material
    {
        get
        {
            return MaterialCode;
        }
        set
        {
            MaterialCode = value ?? string.Empty;
        }
    }
}