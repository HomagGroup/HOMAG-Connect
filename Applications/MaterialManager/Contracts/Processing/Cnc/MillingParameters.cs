using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc;

/// <summary>
/// Material-dependent cutting speed and tooth feed recommendations for milling.
/// </summary>
/// <example>
/// { "suitability": true, "cuttingSpeed": 60.0, "cuttingSpeedMin": 50.0, "cuttingSpeedMax": 70.0,
/// "toothFeed": 0.2, "toothFeedMin": 0.1, "toothFeedMax": 0.3, "unitSystem": "Metric" }
/// </example>
public class MillingParameters : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Recommended cutting speed.
    /// <para>
    /// Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: m/s; for
    /// <see cref="Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft/s.
    /// </para>
    /// </summary>
    [JsonProperty(Order = 2)]
    [ValueDependsOnUnitSystem(BaseUnit.MeterPerSecond)]
    public double CuttingSpeed { get; set; }

    /// <summary>
    /// Upper cutting speed recommendation, when specified.
    /// <para>
    /// Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: m/s; for
    /// <see cref="UnitSystem.Imperial" />: ft/s.
    /// </para>
    /// </summary>
    [JsonProperty(Order = 4)]
    [ValueDependsOnUnitSystem(BaseUnit.MeterPerSecond)]
    public double? CuttingSpeedMax { get; set; }

    /// <summary>
    /// Lower cutting speed recommendation, when specified.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m/s; for <see cref="UnitSystem.Imperial" />: ft/s.</para>
    /// </summary>
    [JsonProperty(Order = 3)]
    [ValueDependsOnUnitSystem(BaseUnit.MeterPerSecond)]
    public double? CuttingSpeedMin { get; set; }

    /// <summary>
    /// Whether these milling parameters are suitable for the associated parameter group.
    /// </summary>
    [JsonProperty(Order = 1)]
    public bool Suitability { get; set; }

    /// <summary>
    /// Recommended feed per tooth.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [JsonProperty(Order = 5)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double ToothFeed { get; set; }

    /// <summary>
    /// Upper feed-per-tooth recommendation, when specified.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [JsonProperty(Order = 7)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? ToothFeedMax { get; set; }

    /// <summary>
    /// Lower feed-per-tooth recommendation, when specified.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [JsonProperty(Order = 6)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? ToothFeedMin { get; set; }

    /// <summary>
    /// Unit system used for cutting speed and tooth feed values.
    /// </summary>
    [JsonProperty(Order = 50)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}