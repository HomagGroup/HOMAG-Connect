using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

public interface IDimensionProperties : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the thickness of the part.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Length))]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Width))]
    public double? Width { get; set; }
}