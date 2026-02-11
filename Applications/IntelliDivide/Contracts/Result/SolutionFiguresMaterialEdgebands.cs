#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

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
    [Range(0, double.MaxValue)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? EdgebandLength { get; set; }
}