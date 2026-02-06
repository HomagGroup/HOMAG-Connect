using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;

namespace HomagConnect.Base.Contracts.Attributes;

/// <summary>
/// Defines the behavior of a property in different unit systems.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueDependsOnUnitSystemAttribute : Attribute
{
    private const double _MillimeterToInchConversionFactor = 0.03937008;

    private const double _MeterToFeetConversionFactor = 3.2808399;

    private const double _BarToPsiConversionFactor = 14.503773773;

    private const double _SquareMeterToSquareFeetConversionFactor = 10.7639104;

    private const double _MeterPerSecondToFeetPerSecondConversionFactor = 3.280839895;

    private const double _KilogramPerCubicMeterToPoundPerCubicFeetConversionFactor = 0.06242796;

    /// <summary>
    /// Attribute to define the unit system of a property.
    /// </summary>
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit)
    {
        BaseUnit = baseUnit;

        DecimalsMetric = baseUnit.GetDecimals(UnitSystem.Metric);
        DecimalsImperial = baseUnit.GetDecimals(UnitSystem.Imperial);

        if (BaseUnit == BaseUnit.Millimeter)
        {
            ConversionFactorMetricToImperial = _MillimeterToInchConversionFactor;
        }
        else if (BaseUnit == BaseUnit.Meter)
        {
            ConversionFactorMetricToImperial = _MeterToFeetConversionFactor;
        }
        else if (BaseUnit == BaseUnit.Bar)
        {
            ConversionFactorMetricToImperial = _BarToPsiConversionFactor;
        }
        else if (BaseUnit == BaseUnit.SquareMeter)
        {
            ConversionFactorMetricToImperial = _SquareMeterToSquareFeetConversionFactor;
        }
        else if (BaseUnit == BaseUnit.MeterPerSecond)
        {
            ConversionFactorMetricToImperial = _MeterPerSecondToFeetPerSecondConversionFactor;
        }
        else if (BaseUnit == BaseUnit.KilogramPerCubicMeter)
        {
            ConversionFactorMetricToImperial = _KilogramPerCubicMeterToPoundPerCubicFeetConversionFactor;
        }
        else
        {
            throw new NotImplementedException($"Conversion factor for {baseUnit} is not defined.");
        }
    }

    /// <summary>
    /// Attribute to define the unit system of a property.
    /// </summary>
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit, int decimalsMetric, int decimalsImperial) : this(baseUnit)
    {
        DecimalsMetric = decimalsMetric;
        DecimalsImperial = decimalsImperial;
    }

    /// <summary>
    /// Gets to conversion factor from the metric to the imperial unit system.
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
}