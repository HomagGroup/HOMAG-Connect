#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures for costs.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFiguresCosts : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the costs of boards and offcuts in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double? CostsOfBoardsPlusOffcuts { get; set; }

    /// <summary>
    /// Gets the costs of edgebands in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double? CostsOfEdgebands { get; set; }

    /// <summary>
    /// Gets the total material costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 1)]
    public double? MaterialCosts { get; set; }

    /// <summary>
    /// Gets the average material costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double? MaterialCostsPerPart { get; set; }

    /// <summary>
    /// Gets the production costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 4)]
    public double? ProductionCosts { get; set; }

    /// <summary>
    /// Gets the average production costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 4)]
    public double? ProductionCostsPerPart { get; set; }

    /// <summary>
    /// Gets the total costs in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 5)]
    public double? TotalCosts { get; set; }

    /// <summary>
    /// Gets the total costs per part in the currency of the subscription.
    /// </summary>
    [JsonProperty(Order = 6)]
    public double? TotalCostsPerPart { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionOverviewFiguresCosts() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionOverviewFiguresCosts(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;
    }

    #endregion
}