using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base;

/// <summary>
/// A measured value with an optional tolerance, expressed in the unit system indicated by <see cref="UnitSystem" />.
/// The containing property determines the physical quantity and its unit.
/// </summary>
/// <example>{ "value": 19.0, "tolerance": 0.2, "unitSystem": "Metric" }</example>
public class DoubleValue : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Creates a measured value with an optional tolerance.
    /// </summary>
    /// <param name="value">The measured value, or null when unavailable.</param>
    /// <param name="tolerance">The allowed tolerance, or null when unspecified.</param>
    public DoubleValue(double? value, double? tolerance = null)
    {
        Value = value;
        Tolerance = tolerance;
    }

    /// <summary>
    /// Allowed tolerance for <see cref="Value" />, in the same unit, or null when unspecified.
    /// </summary>
    [JsonProperty(Order = 1)]
    public double? Tolerance { get; set; }

    /// <summary>
    /// Measured value, or null when unavailable.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double? Value { get; set; }

    /// <summary>
    /// Unit system used to interpret the measured value and its tolerance.
    /// </summary>
    [JsonProperty(Order = 3)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}