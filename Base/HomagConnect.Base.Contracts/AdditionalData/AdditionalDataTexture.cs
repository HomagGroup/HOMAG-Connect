using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Additional data image.
/// </summary>
public class AdditionalDataTexture : AdditionalDataEntity, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    public Grain Grain { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Length { get; set; }

    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Texture;

    /// <summary>
    /// Gets or sets the width
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Width { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />

    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion
}