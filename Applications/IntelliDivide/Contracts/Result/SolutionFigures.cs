#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Represents the key figures available in a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFigures : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the material key figures for the solution.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionFiguresMaterial Material { get; set; }

    /// <summary>
    /// Gets the production key figures for the solution.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresProduction Production { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionFigures() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionFigures(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;

        Production = new SolutionFiguresProduction(unitSystem);
        Material = new SolutionFiguresMaterial(unitSystem);
    }

    #endregion
}