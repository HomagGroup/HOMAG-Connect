using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "Offcuts" section of a cutting machine.
/// </summary>
/// <remarks>
/// When <see cref="UseMaterialManagerOffcuts" /> is <c>true</c>, the offcut sizes are taken from the materials in materialManager
/// and the machine values are not used. The minimum sizes and the rating of large offcuts must be at least as large as the
/// ones of small offcuts.
/// </remarks>
/// <example>
/// { "useMaterialManagerOffcuts": false, "smallOffcutsEnabled": true, "smallOffcutMinimumLength": 500, "smallOffcutMinimumWidth": 300, "smallOffcutMinimumArea": 0.15, "smallOffcutPercentage": 30, "largeOffcutsEnabled": true, "largeOffcutMinimumLength": 2000, "largeOffcutMinimumWidth": 800, "largeOffcutMinimumArea": 1.6, "largeOffcutPercentage": 80 }
/// </example>
public class CuttingOffcutParameters
{
    /// <summary>
    /// Indicates whether the material-dependent offcut settings from materialManager are used instead of the machine values.
    /// Available with an intelliDivide Cutting Advanced or Premium license; otherwise <c>null</c>. Defaults to <c>false</c>.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 1)]
    public bool? UseMaterialManagerOffcuts { get; set; }

    /// <summary>
    /// Indicates whether small (manually stored) offcuts are generated. Defaults to <c>true</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 2)]
    public bool? SmallOffcutsEnabled { get; set; }

    /// <summary>
    /// The minimum length of a small offcut. Must be between 50 mm and 19999.9 mm and less than <see cref="LargeOffcutMinimumLength" />.
    /// </summary>
    /// <example>500</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 3)]
    public double? SmallOffcutMinimumLength { get; set; }

    /// <summary>
    /// The minimum width of a small offcut. Must be between 50 mm and 19999.9 mm and less than <see cref="LargeOffcutMinimumWidth" />.
    /// </summary>
    /// <example>300</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 4)]
    public double? SmallOffcutMinimumWidth { get; set; }

    /// <summary>
    /// The minimum area of a small offcut. Must be between 0 m² and 19999.9 m² and less than <see cref="LargeOffcutMinimumArea" />.
    /// </summary>
    /// <example>0.15</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [JsonProperty(Order = 5)]
    public double? SmallOffcutMinimumArea { get; set; }

    /// <summary>
    /// The rating of a small offcut in percent. Supported values are 0 to 100 in steps of 10 and must not exceed
    /// <see cref="LargeOffcutPercentage" />. Defaults to 30.
    /// </summary>
    /// <example>30</example>
    [Range(0, 100)]
    [JsonProperty(Order = 6)]
    public int? SmallOffcutPercentage { get; set; }

    /// <summary>
    /// Indicates whether large (automatically stored) offcuts are generated. Defaults to <c>true</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 7)]
    public bool? LargeOffcutsEnabled { get; set; }

    /// <summary>
    /// The minimum length of a large offcut. Must be between 50 mm and 19999.9 mm. Defaults to 2000 mm.
    /// </summary>
    /// <example>2000</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 8)]
    public double? LargeOffcutMinimumLength { get; set; }

    /// <summary>
    /// The minimum width of a large offcut. Must be between 50 mm and 19999.9 mm. Defaults to 800 mm.
    /// </summary>
    /// <example>800</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 9)]
    public double? LargeOffcutMinimumWidth { get; set; }

    /// <summary>
    /// The minimum area of a large offcut. Must be between 0 m² and 19999.9 m². Defaults to 1.6 m².
    /// </summary>
    /// <example>1.6</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
    [JsonProperty(Order = 10)]
    public double? LargeOffcutMinimumArea { get; set; }

    /// <summary>
    /// The rating of a large offcut in percent. Supported values are 0 to 100 in steps of 10. Defaults to 80.
    /// </summary>
    /// <example>80</example>
    [Range(0, 100)]
    [JsonProperty(Order = 11)]
    public int? LargeOffcutPercentage { get; set; }
}
