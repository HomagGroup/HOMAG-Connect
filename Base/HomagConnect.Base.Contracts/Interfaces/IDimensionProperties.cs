#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Physical dimensions of a part or board: length, width, and material thickness.
/// Values use the unit system of the containing contract.
/// </summary>
/// <remarks>
/// Dimensions are nullable; implementing contracts can require individual measurements.
/// Range attributes define metric bounds and do not automatically convert those bounds for imperial values.
/// </remarks>
/// <example>
/// { "length": 2800.0, "width": 2070.0, "thickness": 19.0 }
/// </example>
public interface IDimensionProperties : IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Material thickness of the part or board; null represents an unspecified measurement.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>19.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 999.9)]
    public double? Thickness { get; set; }
}