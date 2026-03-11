using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines length and width properties for parts or boards.
/// </summary>
public interface IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Gets or sets the length.
    /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
    /// </summary>
    /// <example>2800</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Length))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
    /// </summary>
    /// <example>2070</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Width))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Width { get; set; }
}