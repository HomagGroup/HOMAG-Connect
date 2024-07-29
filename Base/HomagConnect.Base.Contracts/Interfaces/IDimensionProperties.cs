namespace HomagConnect.Base.Contracts.Interfaces;

public interface IDimensionProperties : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the thickness of the part.
    /// </summary>
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double? Width { get; set; }
}