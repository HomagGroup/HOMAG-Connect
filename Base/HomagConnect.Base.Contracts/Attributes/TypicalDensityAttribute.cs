using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Attributes;

/// <summary>
/// Attribute to specify the typical density of a board material category.
/// </summary>
public class TypicalDensityAttribute : Attribute
{
    /// <summary>
    /// Defines the typical density for a board material category in kg/m³.
    /// </summary>
    public TypicalDensityAttribute(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the typical density in kg/m³.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
    public double Value { get; set; }
}