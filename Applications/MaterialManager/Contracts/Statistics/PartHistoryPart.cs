using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;

namespace HomagConnect.MaterialManager.Contracts.Statistics;

/// <summary>
/// Part history part
/// </summary>
public class PartHistoryPart : IPartProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Id))]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Identifier))]
    public string? Identifier { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ArticleNumber))]
    public string? ArticleNumber { get; set; }

   /// <inheritdoc/>
    [JsonProperty(Order = 4)]
    public string? Description { get; set; }

    /// <inheritdoc />
    public string? LaminateBottom { get; set; }

    /// <inheritdoc />
    public Grain? LaminateBottomGrain { get; set; }

    /// <inheritdoc />
    public string? LaminateTop { get; set; }

    /// <inheritdoc />
    public Grain? LaminateTopGrain { get; set; }

    /// <inheritdoc />
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    public string? EdgeRight { get; set; }

    /// <inheritdoc />
    public UnitSystem UnitSystem { get; set; }

    /// <inheritdoc />
    public double? Thickness { get; set; }

    /// <inheritdoc />
    public double? Length { get; set; }

    /// <inheritdoc />
    public double? Width { get; set; }

    /// <inheritdoc />
    public string? Material { get; set; }

    /// <inheritdoc />
    public Grain Grain { get; set; }

    /// <inheritdoc />
    public string? CncProgramName1 { get; set; }

    /// <inheritdoc />
    public string? CncProgramName2 { get; set; }

    /// <inheritdoc />
    public string? CncProgramName3 { get; set; }
}