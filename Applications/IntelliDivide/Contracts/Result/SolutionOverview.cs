#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview data.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverview : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Provides the overview figures.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionOverviewFigures Figures { get; set; }

    /// <summary>
    /// Gets the list of patterns.
    /// </summary>
    [JsonProperty(Order = 20)]
    public IReadOnlyCollection<SolutionPattern> Pattern { get; set; } = new List<SolutionPattern>();

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionOverview() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionOverview(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;

        Figures = new SolutionOverviewFigures(unitSystem);
    }

    #endregion
}