using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Additional data image.
/// </summary>
public class AdditionalDataTexture : AdditionalDataEntity, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Grain))]
    public Grain Grain { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Length))]
    public double Length { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Type))]
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Texture;

    /// <summary>
    /// Gets or sets the width
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Width))]
    public double Width { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [Display(ResourceType = typeof(Resources), Name = nameof(Width))]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion
}