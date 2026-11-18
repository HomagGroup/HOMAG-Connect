using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "Tension trims" section of a cutting machine.
/// </summary>
/// <remarks>
/// The section is available when the user is allowed to edit tension trims. The tension trim sizes are only applied and
/// validated when <see cref="UseTensionTrims" /> is <c>true</c> and the material-dependent tension trims are not used.
/// </remarks>
/// <example>
/// { "useMaterialManagerTensionTrims": false, "useTensionTrims": true, "tensionTrimType": "SimpleTensionTrim", "tensionTrimWidth": 30, "tensionTrimMinDistance": 150, "tensionTrimMinCutLength": 2000, "tensionTrimSlotDistance": 20, "tensionTrimMiddleBridge": 30 }
/// </example>
public class CuttingTensionTrimParameters
{
    /// <summary>
    /// Indicates whether the material-dependent tension trims from materialManager are used instead of the machine values.
    /// Available with an intelliDivide Cutting Advanced or Premium license; otherwise <c>null</c>. Defaults to <c>false</c>.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 1)]
    public bool? UseMaterialManagerTensionTrims { get; set; }

    /// <summary>
    /// Indicates whether tension trims are used. Defaults to <c>false</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 2)]
    public bool? UseTensionTrims { get; set; }

    /// <summary>
    /// The type of the tension trims.
    /// Available when the machine supports slot-centered and bridge-centered tension trims. Defaults to <see cref="TensionTrimType.SimpleTensionTrim" />.
    /// </summary>
    /// <example>SimpleTensionTrim</example>
    [JsonProperty(Order = 3)]
    public TensionTrimType? TensionTrimType { get; set; }

    /// <summary>
    /// The width of a tension trim including the saw blade. Must be greater than 15 mm and less than 50 mm. Defaults to 30 mm.
    /// </summary>
    /// <example>30</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 4)]
    public double? TensionTrimWidth { get; set; }

    /// <summary>
    /// The minimum distance between two tension trims. Must be greater than 100 mm and less than 1000 mm. Defaults to 150 mm.
    /// </summary>
    /// <example>150</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 5)]
    public double? TensionTrimMinDistance { get; set; }

    /// <summary>
    /// The minimum cutting length from which tension trims are used. Must be greater than 500 mm and less than 9999.9 mm. Defaults to 2000 mm.
    /// </summary>
    /// <example>2000</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 6)]
    public double? TensionTrimMinCutLength { get; set; }

    /// <summary>
    /// The slot distance in percent. Supported values are 10, 20, 30 and 40. Defaults to 20.
    /// </summary>
    /// <example>20</example>
    [JsonProperty(Order = 7)]
    public int? TensionTrimSlotDistance { get; set; }

    /// <summary>
    /// The middle bridge in percent. Supported values are 10 to 90 in steps of 10. Defaults to 30.
    /// </summary>
    /// <example>30</example>
    [JsonProperty(Order = 8)]
    public int? TensionTrimMiddleBridge { get; set; }
}
