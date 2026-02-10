#nullable enable
using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFigures : IContainsUnitSystemDependentProperties
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution"/> class.
    /// </summary>
    public SolutionOverviewFigures() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution"/> class with the specified unit system.
    /// </summary>
    public SolutionOverviewFigures(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;
    }

    #endregion

    /// <summary>
    /// Gets the overview figures for costs.
    /// </summary>
    [JsonProperty(Order = 30)]
    public SolutionOverviewFiguresCosts Costs { get; set; } = new();

    /// <summary>
    /// Gets the overview figures for material.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionOverviewFiguresMaterial Material { get; set; } = new();

    /// <summary>
    /// Gets the overview figures for the production.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionOverviewFiguresProduction Production { get; set; } = new();

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <inheritdoc/>
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }
}