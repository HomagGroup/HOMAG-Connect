using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "In general" section of a cutting machine.
/// </summary>
/// <remarks>
/// The machine name is provided by <see cref="MachineDetails.Name" />. A property is <c>null</c> when it is not available
/// for the machine type (for example angular saw, single saw, workplace saw, tapio registered or connected machine).
/// </remarks>
/// <example>
/// { "maxCuttingLength": 4300, "maxTravelProgramFence": 4300, "ripCutBladeThickness": 4.8, "crossCutBladeThickness": 4.8, "downloadOption": "Download", "productionCosts": 120 }
/// </example>
public class CuttingGeneralParameters
{
    /// <summary>
    /// The maximum cutting length of the saw.
    /// Available for workplace saws, single saws and saws which are not angular saws. Must be between 50 mm and 19999.9 mm.
    /// Defaults to the maximum cutting length of the cross-cut saw from the saw configuration.
    /// </summary>
    /// <example>4300</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 1)]
    public double? MaxCuttingLength { get; set; }

    /// <summary>
    /// The maximum travel of the program fence.
    /// Available for all saws except workplace saws. Must be between 50 mm and 19999.9 mm.
    /// </summary>
    /// <example>4300</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 2)]
    public double? MaxTravelProgramFence { get; set; }

    /// <summary>
    /// Indicates whether the angular saw has an automatic outfeed.
    /// Available for angular saws which are not tapio registered. Defaults to the value of the machine configuration.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 3)]
    public bool? AutomaticOutfeed { get; set; }

    /// <summary>
    /// The maximum cutting length of the rip saw.
    /// Available for angular saws which are not tapio registered. Must be between 50 mm and 19999.9 mm.
    /// </summary>
    /// <example>5600</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 4)]
    public double? MaxCuttingLengthRipSaw { get; set; }

    /// <summary>
    /// The maximum cutting length of the cross-cut saw.
    /// Available for angular saws which are not tapio registered. Must be between 50 mm and 19999.9 mm.
    /// </summary>
    /// <example>2200</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 5)]
    public double? MaxCuttingLengthCrossCutSaw { get; set; }

    /// <summary>
    /// The side of the fence used for the optimization.
    /// Available for workplace saws. Defaults to <see cref="CuttingOptimizationDirection.Left" />.
    /// </summary>
    /// <example>Left</example>
    [JsonProperty(Order = 6)]
    public CuttingOptimizationDirection? OptimizationDirection { get; set; }

    /// <summary>
    /// The maximum width of a strip.
    /// Available for workplace saws. Must be between 50 mm and 19999.9 mm. Defaults to 9999 mm.
    /// </summary>
    /// <example>9999</example>
    [Range(50, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 7)]
    public double? MaxStripWidth { get; set; }

    /// <summary>
    /// The saw blade thickness used for rip cuts.
    /// Available for all saws except single saws. Must be between 0 mm and 19999.9 mm. Defaults to 4.8 mm.
    /// </summary>
    /// <example>4.8</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 8)]
    public double? RipCutBladeThickness { get; set; }

    /// <summary>
    /// The saw blade thickness used for cross cuts.
    /// Available for all saws except single saws. Must be between 0 mm and 19999.9 mm. Defaults to 4.8 mm.
    /// </summary>
    /// <example>4.8</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 9)]
    public double? CrossCutBladeThickness { get; set; }

    /// <summary>
    /// The saw blade thickness of a single saw.
    /// Available for single saws only. Must be between 0 mm and 19999.9 mm. Defaults to 4.8 mm.
    /// </summary>
    /// <example>4.4</example>
    [Range(0, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [JsonProperty(Order = 10)]
    public double? SawBladeThickness { get; set; }

    /// <summary>
    /// The preferred position of manual cuts.
    /// Available for robin machines. Defaults to <see cref="ManualCuttingsPosition.PreferredAtBeginning" />.
    /// </summary>
    /// <example>PreferredAtBeginning</example>
    [JsonProperty(Order = 11)]
    public ManualCuttingsPosition? ManualCuttings { get; set; }

    /// <summary>
    /// The way optimization results are transferred to the machine.
    /// Available for machines which are neither productionAssist machines nor connected machines. Defaults to <see cref="MachineDownloadOption.Download" />.
    /// </summary>
    /// <example>Download</example>
    [JsonProperty(Order = 12)]
    public MachineDownloadOption? DownloadOption { get; set; }

    /// <summary>
    /// The production costs of the machine per hour in the currency of the subscription.
    /// Must not be negative. <c>null</c> when no production costs are set.
    /// </summary>
    /// <example>120</example>
    [Range(0, double.MaxValue)]
    [JsonProperty(Order = 13)]
    public double? ProductionCosts { get; set; }
}
