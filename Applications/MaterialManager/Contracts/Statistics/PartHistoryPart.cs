using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;

namespace HomagConnect.MaterialManager.Contracts.Statistics;

/// <summary>
/// Represents part details contained in a material history entry.
/// </summary>
/// <example>
/// { "id": "PART-1001", "identifier": "4711-01", "articleNumber": "ART-100200", "description": "Left side panel", "material": "P2_Gold_Craft_Oak", "length": 720.0, "width": 480.0, "thickness": 19.0, "grain": "Lengthwise", "unitSystem": "Metric" }
/// </example>
public class PartHistoryPart : IPartProperties, ISupportsLocalizedSerialization, ISupportsAdditionalProperties, IContainsUnitSystemDependentProperties
{
    /// <inheritdoc/>
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the part.
    /// </summary>
    /// <example>PART-1001</example>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Id))]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the business identifier of the part.
    /// </summary>
    /// <example>4711-01</example>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Identifier))]
    public string? Identifier { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    /// <example>ART-100200</example>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ArticleNumber))]
    public string? ArticleNumber { get; set; }

   /// <inheritdoc/>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Description))]
    public string? Description { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottom))]
    public string? LaminateBottom { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateBottomGrain))]
    public Grain? LaminateBottomGrain { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTop))]
    public string? LaminateTop { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LaminateTopGrain))]
    public Grain? LaminateTopGrain { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeBack))]
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeDiagram))]
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeFront))]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeLeft))]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EdgeRight))]
    public string? EdgeRight { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UnitSystem))]
    public UnitSystem UnitSystem { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    public double? Thickness { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Length))]
    public double? Length { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Width))]
    public double? Width { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Material))]
    public string? Material { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Grain))]
    public Grain Grain { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName1))]
    public string? CncProgramName1 { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName2))]
    public string? CncProgramName2 { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName3))]
    public string? CncProgramName3 { get; set; }
}