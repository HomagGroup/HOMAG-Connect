#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

public interface IDimensionProperties : IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Gets or sets the thickness of the part.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Thickness))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.01, 999.9)]
    public double? Thickness { get; set; }
}