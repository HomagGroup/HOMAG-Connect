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
public class SolutionOverview: IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Provides the overview figures.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionOverviewFigures Figures { get; set; } = new();

    /// <summary>
    /// Gets the list of patterns.
    /// </summary>
    [JsonProperty(Order = 20)]
    public IReadOnlyCollection<SolutionPattern> Pattern { get; set; } = new List<SolutionPattern>();

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