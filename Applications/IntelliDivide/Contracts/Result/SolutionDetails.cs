#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Represents a solution with details.
/// </summary>
[Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(SolutionDetails))]
public class SolutionDetails : Solution
{
    /// <summary>
    /// Gets or sets the exports (<see cref="SolutionExportType" /> of the solution.
    /// </summary>
    [JsonProperty(Order = 70)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(Exports))]
    public IReadOnlyCollection<SolutionExportType> Exports { get; set; } = new List<SolutionExportType>();

    /// <summary>
    /// Gets or sets the key figures of the solution.
    /// </summary>
    [JsonProperty(Order = 40)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(KeyFigures))]
    public SolutionFigures KeyFigures { get; set; } = new();

    /// <summary>
    /// Gets or sets the material of the solution.
    /// </summary>
    [JsonProperty(Order = 30)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(Material))]
    public SolutionMaterial Material { get; set; } = new();

    /// <summary>
    /// Gets or sets the parts of the solution.
    /// </summary>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(Parts))]
    public IReadOnlyCollection<SolutionPart> Parts { get; set; } = new List<SolutionPart>();

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionDetails() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionDetails(UnitSystem unitSystem) : base(unitSystem) { }

    #endregion
}