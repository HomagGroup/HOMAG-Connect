#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the overview figures for production.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionOverviewFiguresProduction : IContainsUnitSystemDependentProperties
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution"/> class.
    /// </summary>
    public SolutionOverviewFiguresProduction() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution"/> class with the specified unit system.
    /// </summary>
    public SolutionOverviewFiguresProduction(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;
    }

    #endregion

    /// <summary>
    /// Gets the average book height in mm or inch.
    /// </summary>
    [JsonProperty(Order = 6)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double AverageBookHeight { get; set; }

    /// <summary>
    /// Gets the quantity of cutting cycles.
    /// </summary>
    [JsonProperty(Order = 5)]
    public int Cycles { get; set; }

    /// <summary>
    /// Gets the estimated production time.
    /// </summary>
    [JsonProperty(Order = 1)]
    public TimeSpan ProductionTime { get; set; }

    /// <summary>
    /// Gets the average production time per part in seconds.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double ProductionTimePerPart { get; set; }

    /// <summary>
    /// Gets the quantity of parts.
    /// </summary>
    [JsonProperty(Order = 3)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Gets the quantity of plus parts (optional parts).
    /// </summary>
    [JsonProperty(Order = 4)]
    public int QuantityOfPlusParts { get; set; }

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