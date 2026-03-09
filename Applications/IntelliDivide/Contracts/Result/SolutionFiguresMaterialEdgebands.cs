#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Provides the key figures for material edgebands.
/// </summary>
/// <example>
/// {
///   "edgebandLength": 148.95
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresMaterialEdgebands
{
    /// <summary>
    /// Gets the total edgeband length.
    /// Unit: meters for <see cref="UnitSystem.Metric"/> and feet for <see cref="UnitSystem.Imperial"/>.
    /// </summary>
    /// <example>148.95</example>
    [Range(0, double.MaxValue)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? EdgebandLength { get; set; }
}