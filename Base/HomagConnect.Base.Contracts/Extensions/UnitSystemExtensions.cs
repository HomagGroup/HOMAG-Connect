using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Extensions;

/// <summary>
/// Extension methods for objects that contain properties that are dependent on the unit system.
/// </summary>
public static class UnitSystemExtensions
{
    private const double _MeterToFeetConversionFactor = 3.280839895;

    private const double _MillimeterToInchConversionFactor = 25.4;

    private const double _PsiToBarConversionFactor = 14.503773773;

    private const double _SquareFeetToSquareMeterConversionFactor = 10.7639104;

    private const int _MillimeterToMeterConversionFactor = 1000;

    private const int _SquareInchToSquareFootConversionFactor = 144;

    /// <summary>
    /// Calculates the area of a rectangle.
    /// </summary>
    public static double? CalculateArea(this UnitSystem unitSystem, double? length, double? width, int? quantity)
    {
        if (length == null || width == null || quantity == null)
        {
            return null;
        }

        if (unitSystem == UnitSystem.Metric)
        {
            return Math.Round(ConvertMillimeterToMeter(length.Value) * ConvertMillimeterToMeter(width.Value) * (int)quantity, 1);
        }

        if (unitSystem == UnitSystem.Imperial)
        {
            return Math.Round(ConvertSquareInchToSquareFoot(length.Value * width.Value) * (int)quantity, 3);
        }

        throw new InvalidOperationException($"{nameof(UnitSystem)} {unitSystem} is not supported");
    }

    /// <summary>
    /// Converts the unit system of the object to the specified unit system. All properties that are marked with
    /// <see cref="ValueDependsOnUnitSystemAttribute" /> are handled.
    /// </summary>
    public static T SwitchUnitSystem<T>(this T o, UnitSystem unitSystem) where T : class, IContainsUnitSystemDependentProperties, new()
    {
        if (o == null)
        {
            throw new ArgumentNullException(nameof(o));
        }

        var serializedObject = JsonConvert.SerializeObject(o);

        var clone = JsonConvert.DeserializeObject<T>(serializedObject) ?? throw new InvalidOperationException("Failed to clone object.");

        if (o.UnitSystem == unitSystem)
        {
            return clone;
        }

        clone.UnitSystem = unitSystem;

        var propertyInfos = clone.GetType().GetProperties();

        foreach (var propertyInfo in propertyInfos)
        {
            var propertyConstraints =
                propertyInfo.GetCustomAttributes().OfType<ValueDependsOnUnitSystemAttribute>().FirstOrDefault();

            if (propertyConstraints != null)
            {
                if (propertyConstraints.BaseUnit == BaseUnit.Millimeter)
                {
                    SwitchBaseUnitMillimeter(propertyInfo, clone, propertyConstraints);
                }
                else if (propertyConstraints.BaseUnit == BaseUnit.SquareMeter)
                {
                    SwitchBaseUnitSquareMeter(propertyInfo, clone, propertyConstraints);
                }
                else if (propertyConstraints.BaseUnit == BaseUnit.Meter)
                {
                    SwitchBaseUnitMeter(propertyInfo, clone, propertyConstraints);
                }
                else if (propertyConstraints.BaseUnit == BaseUnit.Bar)
                {
                    SwitchBaseUnitBar(propertyInfo, clone, propertyConstraints);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        return clone;
    }

    /// <summary>
    /// Converts a value in bar to psi.
    /// </summary>
    private static double? ConvertBarToPsi(object value, int decimals, bool roundValues)
    {
        var result = (double)value * _PsiToBarConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in feet to meter.
    /// </summary>
    private static double? ConvertFeetToMeter(object value, int decimals, bool roundValues)
    {
        var result = (double)value / _MeterToFeetConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in inch to millimeter.
    /// </summary>
    private static double? ConvertInchToMillimeter(object value, int decimals, bool roundValues)
    {
        var result = (double)value * _MillimeterToInchConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in meter to feet.
    /// </summary>
    private static double? ConvertMeterToFeet(object value, int decimals, bool roundValues)
    {
        var result = (double)value * _MeterToFeetConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in millimeter to inch.
    /// </summary>
    private static double? ConvertMillimeterToInch(object value, int decimals, bool roundValues)
    {
        var result = (double)value / _MillimeterToInchConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in millimeter to meter.
    /// </summary>
    private static double ConvertMillimeterToMeter(double value)
    {
        return value / _MillimeterToMeterConversionFactor;
    }

    /// <summary>
    /// Converts a value in psi to bar.
    /// </summary>
    private static double? ConvertPsiToBar(object value, int decimals, bool roundValues)
    {
        var result = (double)value / _PsiToBarConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in square feet to square meter.
    /// </summary>
    private static double? ConvertSquareFeetToSquareMeter(object value, int decimals, bool roundValues)
    {
        var result = (double)value / _SquareFeetToSquareMeterConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    /// <summary>
    /// Converts a value in square inch to square foot.
    /// </summary>
    private static double ConvertSquareInchToSquareFoot(double value)
    {
        return value / _SquareInchToSquareFootConversionFactor;
    }

    /// <summary>
    /// Converts a value in square meter to square feet.
    /// </summary>
    private static double? ConvertSquareMeterToSquareFeet(object value, int decimals, bool roundValues)
    {
        var result = (double)value * _SquareFeetToSquareMeterConversionFactor;
        return roundValues ? Math.Round(result, decimals) : result;
    }

    private static void SwitchBaseUnitBar<T>(PropertyInfo propertyInfo, T clone, ValueDependsOnUnitSystemAttribute propertyConstraints) where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);

        if (value != null)
        {
            if (clone.UnitSystem == UnitSystem.Imperial)
            {
                propertyInfo.SetValue(clone, ConvertBarToPsi(value, propertyConstraints.ImperialDecimalPrecision, propertyConstraints.RoundValues));
            }
            else if (clone.UnitSystem == UnitSystem.Metric)
            {
                propertyInfo.SetValue(clone, ConvertPsiToBar(value, propertyConstraints.MetricDecimalPrecision, propertyConstraints.RoundValues));
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
            }
        }
    }

    private static void SwitchBaseUnitMeter<T>(PropertyInfo propertyInfo, T clone, ValueDependsOnUnitSystemAttribute propertyConstraints) where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);

        if (value != null)
        {
            if (clone.UnitSystem == UnitSystem.Imperial)
            {
                propertyInfo.SetValue(clone, ConvertMeterToFeet(value, propertyConstraints.ImperialDecimalPrecision, propertyConstraints.RoundValues));
            }
            else if (clone.UnitSystem == UnitSystem.Metric)
            {
                propertyInfo.SetValue(clone, ConvertFeetToMeter(value, propertyConstraints.MetricDecimalPrecision, propertyConstraints.RoundValues));
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
            }
        }
    }

    private static void SwitchBaseUnitMillimeter<T>(PropertyInfo propertyInfo,
        T clone,
        ValueDependsOnUnitSystemAttribute propertyConstraints)
        where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);

        if (value != null)
        {
            if (clone.UnitSystem == UnitSystem.Imperial)
            {
                propertyInfo.SetValue(clone, ConvertMillimeterToInch(value, propertyConstraints.ImperialDecimalPrecision, propertyConstraints.RoundValues));
            }
            else if (clone.UnitSystem == UnitSystem.Metric)
            {
                propertyInfo.SetValue(clone, ConvertInchToMillimeter(value, propertyConstraints.MetricDecimalPrecision, propertyConstraints.RoundValues));
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
            }
        }
    }

    private static void SwitchBaseUnitSquareMeter<T>(PropertyInfo propertyInfo, T clone, ValueDependsOnUnitSystemAttribute propertyConstraints) where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);

        if (value != null)
        {
            if (clone.UnitSystem == UnitSystem.Imperial)
            {
                propertyInfo.SetValue(clone, ConvertSquareMeterToSquareFeet(value, propertyConstraints.ImperialDecimalPrecision, propertyConstraints.RoundValues));
            }
            else if (clone.UnitSystem == UnitSystem.Metric)
            {
                propertyInfo.SetValue(clone, ConvertSquareFeetToSquareMeter(value, propertyConstraints.MetricDecimalPrecision, propertyConstraints.RoundValues));
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
            }
        }
    }
}