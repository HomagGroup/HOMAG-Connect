using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

public interface IDimensionProperties : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the thickness of the part.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.01, 999.9)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Length))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Width))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.9)]
    public double? Width { get; set; }
}