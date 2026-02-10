#nullable enable

using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the key figures for the production of a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProduction : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets the production key figures for output.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresProductionOutput Output { get; set; } = new();

    /// <summary>
    /// Gets the production key figures for handling.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionFiguresProductionHandling Handling { get; set; } = new();

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc/>
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion
}