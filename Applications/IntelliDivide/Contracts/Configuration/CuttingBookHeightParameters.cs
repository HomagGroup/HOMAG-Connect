using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "Book height" section of a cutting machine.
/// </summary>
/// <remarks>
/// The section is not available for circular saws, TKS machines and batch size one saws which are not operated unmanned.
/// </remarks>
/// <example>
/// { "useMaterialManagerBookHeights": false, "maximumBookHeight": 95 }
/// </example>
public class CuttingBookHeightParameters
{
    /// <summary>
    /// Indicates whether the material-dependent book heights from materialManager are used instead of the machine value.
    /// Available with an intelliDivide Cutting Advanced or Premium license; otherwise <c>null</c>. Defaults to <c>false</c>.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 1)]
    public bool? UseMaterialManagerBookHeights { get; set; }

    /// <summary>
    /// The maximum book height. Must not be negative and must not exceed the maximum book height of the machine settings.
    /// Defaults to the maximum book height of the saw material configuration or, if not set, the maximum cutting height of the saw.
    /// </summary>
    /// <example>95</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 2)]
    public double? MaximumBookHeight { get; set; }
}
