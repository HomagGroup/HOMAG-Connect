#nullable enable
using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the key figures for the materials of a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterial 
{
    /// <summary>
    /// Gets the material key figures for boards and offcuts.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresMaterialBoardsOffcuts BoardsAndOffcuts { get; set; } = new();

    /// <summary>
    /// Gets the material key figures for boards and offcuts.
    /// </summary>
    [JsonProperty(Order = 11)]
    public SolutionFiguresMaterialEdgebands Edgebands { get; set; } = new();

    /// <summary>
    /// Gets a list of material key figures for waste per material code.
    /// </summary>
    [JsonProperty(Order = 20)]
    public IReadOnlyCollection<SolutionFiguresMaterialWasteOffcuts> WasteOffcutsPerMaterial { get; set; } = new List<SolutionFiguresMaterialWasteOffcuts>();

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}