using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Defines the unit system.
/// </summary>
[ResourceManager(typeof(UnitSystemDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum UnitSystem
{
    /// <summary>
    /// Metric unit system.
    /// </summary>
    Metric = 0,

    /// <summary>
    /// Imperial unit system.
    /// </summary>
    Imperial = 1
}