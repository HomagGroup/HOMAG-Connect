#nullable enable
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material boards and offcuts.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialEdgebands
{
    /// <summary>
    /// Gets the total edgeband length in meters or feet.
    /// </summary>
    public double? EdgebandLength { get; set; }
}