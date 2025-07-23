using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Contracts.Request;

public class MaterialAssistRequestEdgebandEntity : MaterialAssistRequestBase
{
    /// <summary>
    /// Gets or sets the current thickness.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0.01, 99.99)]
    public double? CurrentThickness { get; set; }

    /// <summary>
    /// Gets or sets the board code to which the entity should be assigned.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string EdgebandCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0.1, 9999.99)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the management type.
    /// </summary>
    [Required]
    public ManagementType ManagementType { get; set; }
}