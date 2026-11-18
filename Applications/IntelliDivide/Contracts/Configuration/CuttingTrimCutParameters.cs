using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "Trim cuts" section of a cutting machine. All dimensions include the saw blade.
/// </summary>
/// <remarks>
/// When <see cref="UseMaterialManagerTrims" /> is <c>true</c>, the trim cuts are taken from the materials in materialManager
/// and the machine values are not used.
/// </remarks>
/// <example>
/// { "useMaterialManagerTrims": false, "trimFront": 15, "trimBack": 15, "trimLeft": 15, "trimRight": 15, "trimRecutsFront": 0, "trimRecutsBack": 0, "reducibleTrims": true }
/// </example>
public class CuttingTrimCutParameters
{
    /// <summary>
    /// Indicates whether the material-dependent trim cuts from materialManager are used instead of the machine values.
    /// Available with an intelliDivide Cutting Advanced or Premium license; otherwise <c>null</c>. Defaults to <c>false</c>.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 1)]
    public bool? UseMaterialManagerTrims { get; set; }

    /// <summary>
    /// The minimal trim cut at the front of rip cuts. Must be between 0 mm and 19999.9 mm. Defaults to 15 mm.
    /// </summary>
    /// <example>15</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 2)]
    public double? TrimFront { get; set; }

    /// <summary>
    /// The minimal trim cut at the back of rip cuts. Must be between 0 mm and 19999.9 mm. Defaults to 15 mm.
    /// </summary>
    /// <example>15</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 3)]
    public double? TrimBack { get; set; }

    /// <summary>
    /// The minimal trim cut on the left side (back of cross cuts). Must be between 0 mm and 19999.9 mm. Defaults to 15 mm.
    /// </summary>
    /// <example>15</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 4)]
    public double? TrimLeft { get; set; }

    /// <summary>
    /// The minimal trim cut on the right side (front of cross cuts). Must be between 0 mm and 19999.9 mm. Defaults to 15 mm.
    /// </summary>
    /// <example>15</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 5)]
    public double? TrimRight { get; set; }

    /// <summary>
    /// The minimal trim cut at the front of recuts. Must be between 0 mm and 19999.9 mm.
    /// </summary>
    /// <example>0</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 6)]
    public double? TrimRecutsFront { get; set; }

    /// <summary>
    /// The minimal trim cut at the back of recuts. Must be between 0 mm and 19999.9 mm.
    /// </summary>
    /// <example>0</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 7)]
    public double? TrimRecutsBack { get; set; }

    /// <summary>
    /// Indicates whether the optimization may reduce the trim cuts when this improves the result. Defaults to <c>true</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 8)]
    public bool? ReducibleTrims { get; set; }
}
