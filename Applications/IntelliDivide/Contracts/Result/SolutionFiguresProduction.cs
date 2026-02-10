#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the key figures for the production of a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProduction : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the production key figures for handling.
    /// </summary>
    [JsonProperty(Order = 20)]
    public SolutionFiguresProductionHandling Handling { get; set; }

    /// <summary>
    /// Gets the production key figures for output.
    /// </summary>
    [JsonProperty(Order = 10)]
    public SolutionFiguresProductionOutput Output { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionFiguresProduction() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionFiguresProduction(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;

        Output = new SolutionFiguresProductionOutput(unitSystem);
        Handling = new SolutionFiguresProductionHandling(unitSystem);
    }

    #endregion
}