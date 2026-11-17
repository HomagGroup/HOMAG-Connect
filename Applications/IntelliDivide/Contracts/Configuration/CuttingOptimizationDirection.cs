using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Defines the side of the fence from which a workplace saw optimizes the cutting patterns.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum CuttingOptimizationDirection
{
    /// <summary>
    /// The fence is on the left side.
    /// </summary>
    Left,

    /// <summary>
    /// The fence is on the right side.
    /// </summary>
    Right
}
