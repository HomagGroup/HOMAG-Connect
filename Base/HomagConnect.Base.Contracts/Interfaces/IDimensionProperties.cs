#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines the dimensional properties of a part or board.
/// Includes the inherited <see cref="IDimensionPropertiesLengthWidth.Length" /> and <see cref="IDimensionPropertiesLengthWidth.Width" /> values,
/// plus thickness.
/// </summary>
public interface IDimensionProperties : IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Gets or sets the thickness.
    /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
    /// </summary>
    /// <example>19.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.01, 999.9)]
    public double? Thickness { get; set; }
}