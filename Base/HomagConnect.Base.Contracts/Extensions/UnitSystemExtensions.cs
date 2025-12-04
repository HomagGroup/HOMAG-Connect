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
            return Math.Round(ConvertMillimeterToMeter(length.Value) * ConvertMillimeterToMeter(width.Value) * (int)quantity, BaseUnit.Meter.GetDecimals(unitSystem));
        }

        if (unitSystem == UnitSystem.Imperial)
        {
            return Math.Round(ConvertSquareInchToSquareFoot(length.Value * width.Value) * (int)quantity, BaseUnit.Meter.GetDecimals(unitSystem));
        }

        throw new InvalidOperationException($"{nameof(UnitSystem)} {unitSystem} is not supported");
    }

    /// <summary>
    /// Converts the unit system of the object to the specified unit system. All properties that are marked with
    /// <see cref="ValueDependsOnUnitSystemAttribute" /> are handled.
    /// </summary>
    public static T SwitchUnitSystem<T>(this T o, UnitSystem unitSystem, bool applyRounding) where T : class, IContainsUnitSystemDependentProperties, new()
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
            var valueDependsOnUnitSystemAttribute =
                propertyInfo.GetCustomAttributes().OfType<ValueDependsOnUnitSystemAttribute>().FirstOrDefault();

            if (valueDependsOnUnitSystemAttribute != null)
            {
                if (valueDependsOnUnitSystemAttribute.BaseUnit is BaseUnit.Millimeter or BaseUnit.SquareMeter or BaseUnit.Meter or BaseUnit.Bar or BaseUnit.MeterPerSecond or BaseUnit.KilogramPerCubicMeter)
                {
                    SwitchBaseUnit(propertyInfo, clone, valueDependsOnUnitSystemAttribute, applyRounding);
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
    /// Converts a value in millimeter to meter.
    /// </summary>
    private static double ConvertMillimeterToMeter(double value)
    {
        return value / _MillimeterToMeterConversionFactor;
    }

    /// <summary>
    /// Converts a value in square inch to square foot.
    /// </summary>
    private static double ConvertSquareInchToSquareFoot(double value)
    {
        return value / _SquareInchToSquareFootConversionFactor;
    }

    private static void SwitchBaseUnit<T>(PropertyInfo propertyInfo, T clone, ValueDependsOnUnitSystemAttribute valueDependsOnUnitSystemAttribute, bool applyRounding)
        where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);

        if (value != null)
        {
            if (clone.UnitSystem == UnitSystem.Imperial)
            {
                var convertedValue = (double)value * valueDependsOnUnitSystemAttribute.ConversionFactorMetricToImperial;

                if (applyRounding)
                {
                    convertedValue = Math.Round(convertedValue, valueDependsOnUnitSystemAttribute.DecimalsImperial);
                }

                propertyInfo.SetValue(clone, convertedValue);
            }
            else if (clone.UnitSystem == UnitSystem.Metric)
            {
                var convertedValue = (double)value / valueDependsOnUnitSystemAttribute.ConversionFactorMetricToImperial;

                if (applyRounding)
                {
                    convertedValue = Math.Round(convertedValue, valueDependsOnUnitSystemAttribute.DecimalsMetric);
                }

                propertyInfo.SetValue(clone, convertedValue);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
            }
        }
    }
}