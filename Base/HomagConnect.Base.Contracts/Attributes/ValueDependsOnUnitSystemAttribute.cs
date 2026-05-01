using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;

namespace HomagConnect.Base.Contracts.Attributes;

/// <summary>
/// Marks a numeric property as dependent on the selected unit system and provides conversion, rounding, and display metadata.
/// </summary>
/// <remarks>
/// The annotated value is treated as a metric value. <see cref="ConversionFactorMetricToImperial" /> defines how to convert it
/// to the corresponding imperial display value.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueDependsOnUnitSystemAttribute : Attribute
{
    private const double MillimeterToInchConversionFactor = 0.03937008;

    private const double MeterToFeetConversionFactor = 3.2808399;

    private const double BarToPsiConversionFactor = 14.503773773;

    private const double SquareMeterToSquareFeetConversionFactor = 10.7639104;

    private const double MeterPerSecondToFeetPerSecondConversionFactor = 3.280839895;

    private const double KilogramPerCubicMeterToPoundPerCubicFeetConversionFactor = 0.06242796;

    private const double KilogramToPoundConversionFactor = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDependsOnUnitSystemAttribute" /> class for the specified base unit.
    /// </summary>
    /// <param name="baseUnit">The metric base unit used by the annotated property.</param>
    /// <exception cref="NotImplementedException">Thrown when no conversion metadata is defined for <paramref name="baseUnit" />.</exception>
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit)
    {
        var unitDefinition = GetUnitDefinition(baseUnit);

        BaseUnit = baseUnit;
        ConversionFactorMetricToImperial = unitDefinition.ConversionFactorMetricToImperial;

        DisplayUnitMetric = unitDefinition.DisplayUnitMetric;
        DisplayUnitImperial = unitDefinition.DisplayUnitImperial;

        DecimalsMetric = baseUnit.GetDecimals(UnitSystem.Metric);
        DecimalsImperial = baseUnit.GetDecimals(UnitSystem.Imperial);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueDependsOnUnitSystemAttribute" /> class with custom rounding precision.
    /// </summary>
    /// <param name="baseUnit">The metric base unit used by the annotated property.</param>
    /// <param name="decimalsMetric">The number of decimals to use for metric values.</param>
    /// <param name="decimalsImperial">The number of decimals to use for imperial values.</param>
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit, int decimalsMetric, int decimalsImperial) : this(baseUnit)
    {
        DecimalsMetric = decimalsMetric;
        DecimalsImperial = decimalsImperial;
    }

    /// <summary>
    /// Gets the conversion factor from the metric unit to the imperial unit.
    /// </summary>
    public double ConversionFactorMetricToImperial { get; }

    /// <summary>
    /// Gets the base unit of the property.
    /// </summary>
    public BaseUnit BaseUnit { get; }

    /// <summary>
    /// Gets the number of decimals to round in the metric unit system.
    /// </summary>
    public int DecimalsMetric { get; }

    /// <summary>
    /// Gets the number of decimals to round in the imperial unit system.
    /// </summary>
    public int DecimalsImperial { get; }

    /// <summary>
    /// Gets the display unit for metric values.
    /// </summary>
    public string DisplayUnitMetric { get; }

    /// <summary>
    /// Gets the display unit for imperial values.
    /// </summary>
    public string DisplayUnitImperial { get; }

    private static UnitDefinition GetUnitDefinition(BaseUnit baseUnit)
    {
        return baseUnit switch
        {
            BaseUnit.Millimeter => new UnitDefinition(MillimeterToInchConversionFactor, "mm", "in"),
            BaseUnit.Meter => new UnitDefinition(MeterToFeetConversionFactor, "m", "ft"),
            BaseUnit.Bar => new UnitDefinition(BarToPsiConversionFactor, "bar", "psi"),
            BaseUnit.SquareMeter => new UnitDefinition(SquareMeterToSquareFeetConversionFactor, "m²", "ft²"),
            BaseUnit.MeterPerSecond => new UnitDefinition(MeterPerSecondToFeetPerSecondConversionFactor, "m/s", "ft/s"),
            BaseUnit.KilogramPerCubicMeter => new UnitDefinition(KilogramPerCubicMeterToPoundPerCubicFeetConversionFactor, "kg/m³", "lb/ft³"),
            BaseUnit.Kilogram => new UnitDefinition(KilogramToPoundConversionFactor, "kg", "lb"),
            _ => throw new NotImplementedException($"Conversion factor for {baseUnit} is not defined.")
        };
    }

    private sealed class UnitDefinition(double conversionFactorMetricToImperial, string displayUnitMetric, string displayUnitImperial)
    {
        public double ConversionFactorMetricToImperial { get; } = conversionFactorMetricToImperial;

        public string DisplayUnitMetric { get; } = displayUnitMetric;

        public string DisplayUnitImperial { get; } = displayUnitImperial;
    }
}