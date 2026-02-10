#nullable enable
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material boards and offcuts.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialEdgebands : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets the total edgeband length in meters or feet.
    /// </summary>
    public double? EdgebandLength { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc/>
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion

}