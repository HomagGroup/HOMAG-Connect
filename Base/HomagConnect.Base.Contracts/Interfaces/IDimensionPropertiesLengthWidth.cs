using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Length and width of a part or board, expressed in the unit system of the containing contract.
/// </summary>
/// <remarks>
/// Measurements are nullable; implementing contracts can require them.
/// Range attributes define metric bounds and do not automatically convert those bounds for imperial values.
/// </remarks>
/// <example>
/// { "length": 2800.0, "width": 2070.0 }
/// </example>
public interface IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Length of the part or board; null represents an unspecified measurement.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2800</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Length))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Length { get; set; }

    /// <summary>
    /// Width of the part or board; null represents an unspecified measurement.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2070</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Width))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Width { get; set; }
}