#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the key figures for the materials of a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterial : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the material key figures for boards and offcuts.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresMaterialBoardsOffcuts BoardsAndOffcuts { get; set; }

    /// <summary>
    /// Gets the material key figures for boards and offcuts.
    /// </summary>
    [JsonProperty(Order = 11)]
    public SolutionFiguresMaterialEdgebands Edgebands { get; set; }

    /// <summary>
    /// Gets a list of material key figures for waste per material code.
    /// </summary>
    [JsonProperty(Order = 20)]
    public IReadOnlyCollection<SolutionFiguresMaterialWasteOffcuts> WasteOffcutsPerMaterial { get; set; } = new List<SolutionFiguresMaterialWasteOffcuts>();

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionFiguresMaterial() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionFiguresMaterial(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;

        BoardsAndOffcuts = new SolutionFiguresMaterialBoardsOffcuts(unitSystem);
        Edgebands = new SolutionFiguresMaterialEdgebands(unitSystem);
    }

    #endregion
}