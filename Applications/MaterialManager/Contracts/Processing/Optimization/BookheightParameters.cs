using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Material-dependent settings for the height of boards cut together as a book.
/// </summary>
/// <example>{ "mode": "LimitedBookHeight", "limitedBookHeight": 80.0, "unitSystem": "Metric" }</example>
public class BookHeightParameters : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Book height limit used with <see cref="BookHeightMode.LimitedBookHeight" />, when specified.
    /// <para>
    /// Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: mm; for
    /// <see cref="Base.Contracts.Enumerations.UnitSystem.Imperial" />
    /// : inch.
    /// </para>
    /// </summary>
    [JsonProperty(Order = 13)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? LimitedBookHeight { get; set; }

    /// <summary>
    /// Legacy maximum book height; its specific-value mode is obsolete.
    /// <para>
    /// Unit for <see cref="Base.Contracts.Enumerations.UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />
    /// : inch.
    /// </para>
    /// </summary>
    [Obsolete("This parameter is obsolete because its mode was removed. Please see BookHeightMode for the available modes")]
    [JsonProperty(Order = 12)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? MaximumBookHeight { get; set; }

    /// <summary>
    /// Legacy deduction from the maximum saw blade projection; its mode is obsolete.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm; for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Obsolete("This parameter is obsolete because its mode was removed. Please see BookHeightMode for the available modes")]
    [JsonProperty(Order = 11)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? MaximumSawBladeProjectionDeduction { get; set; }

    /// <summary>
    /// Strategy used to determine the book height.
    /// See <see cref="BookHeightMode" /> for the supported strategies and obsolete alternatives.
    /// </summary>
    [JsonProperty(Order = 10)]
    public BookHeightMode Mode { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 30)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}