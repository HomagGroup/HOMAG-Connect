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
public class PartHistoryPart : IPartProperties, ISupportsLocalizedSerialization
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