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
    /// Calculates the area of a rectangle for the given unit system.
    /// - Metric: length/width are provided in millimeters and converted to square meters.
    /// - Imperial: length/width are provided in square inches and converted to square feet.
    /// Returns null if any input is null.
    /// </summary>
    public static double? CalculateArea(this UnitSystem unitSystem, double? length, double? width, int? quantity)
    {
        if (length == null || width == null || quantity == null)
        {
            return null;
        }

        if (unitSystem == UnitSystem.Metric)
        {
            var areaSquareMeters =
                ConvertMillimeterToMeter(length.Value) *
                ConvertMillimeterToMeter(width.Value) *
                quantity.Value;

            return Math.Round(areaSquareMeters, BaseUnit.Meter.GetDecimals(unitSystem));
        }

        if (unitSystem == UnitSystem.Imperial)
        {
            var areaSquareFeet =
                ConvertSquareInchToSquareFoot(length.Value * width.Value) *
                quantity.Value;

            return Math.Round(areaSquareFeet, BaseUnit.Meter.GetDecimals(unitSystem));
        }

        throw new InvalidOperationException($"{nameof(UnitSystem)} {unitSystem} is not supported");
    }

    /// <summary>
    /// Converts the unit system of the given object to the specified unit system.
    /// Behavior:
    /// - Clones the input to avoid mutating the original instance.
    /// - Converts primitive properties marked with ValueDependsOnUnitSystemAttribute.
    /// - Recursively converts nested properties that implement IContainsUnitSystemDependentProperties.
    /// - Converts collections (arrays, IEnumerable&lt;T&gt;, List&lt;T&gt;) of nested unit-system dependent objects.
    /// </summary>
    /// <typeparam name="T">A type implementing IContainsUnitSystemDependentProperties.</typeparam>
    /// <param name="source">The source instance to convert.</param>
    /// <param name="unitSystem">Target unit system.</param>
    /// <param name="applyRounding">Whether to round values using attribute-provided decimal settings.</param>
    /// <returns>A converted clone of the source instance.</returns>
    public static T SwitchUnitSystem<T>(this T source, UnitSystem unitSystem, bool applyRounding)
        where T : class, IContainsUnitSystemDependentProperties, new()
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        // Clone to avoid mutating the original instance
        var serializedObject = JsonConvert.SerializeObject(source);
        var clone = JsonConvert.DeserializeObject<T>(serializedObject)
                   ?? throw new InvalidOperationException("Failed to clone object.");

        // Always set target unit system on the clone
        clone.UnitSystem = unitSystem;

        // Convert all public instance properties
        var propertyInfos = clone.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var propertyInfo in propertyInfos)
        {
            // 1) Convert primitive properties marked with ValueDependsOnUnitSystemAttribute
            var valueDependsOnUnitSystemAttribute = GetUnitSystemAttribute(propertyInfo, clone.GetType());

            if (valueDependsOnUnitSystemAttribute != null)
            {
                ConvertValueWithBaseUnit(propertyInfo, clone, valueDependsOnUnitSystemAttribute, applyRounding);
                continue;
            }

            // 2) Recursively convert nested objects that implement IContainsUnitSystemDependentProperties
            var currentValue = propertyInfo.GetValue(clone);
            if (currentValue == null)
            {
                continue;
            }

            // Single nested object
            if (currentValue is IContainsUnitSystemDependentProperties nested)
            {
                var convertedNested = SwitchUnitSystemDynamic(nested, unitSystem, applyRounding);
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(clone, convertedNested);
                }
                continue;
            }

            // Arrays or lists of nested objects
            if (currentValue is System.Collections.IEnumerable enumerable)
            {
                var elementType = GetEnumerableElementType(propertyInfo.PropertyType);
                if (elementType != null && typeof(IContainsUnitSystemDependentProperties).IsAssignableFrom(elementType))
                {
                    var convertedCollection = ConvertEnumerable(enumerable, elementType, unitSystem, applyRounding);
                    if (propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(clone, convertedCollection);
                    }
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

    /// <summary>
    /// Converts a single numeric property annotated with ValueDependsOnUnitSystemAttribute
    /// using the object's current target unit system (clone.UnitSystem).
    /// </summary>
    private static void ConvertValueWithBaseUnit<T>(
        PropertyInfo propertyInfo,
        T clone,
        ValueDependsOnUnitSystemAttribute unitAttribute,
        bool applyRounding)
        where T : IContainsUnitSystemDependentProperties, new()
    {
        var value = propertyInfo.GetValue(clone);
        if (value == null)
        {
            return;
        }

        // Ensure supported base units
        var supportedBaseUnit =
            unitAttribute.BaseUnit is BaseUnit.Millimeter
            or BaseUnit.SquareMeter
            or BaseUnit.Meter
            or BaseUnit.Bar
            or BaseUnit.MeterPerSecond
            or BaseUnit.KilogramPerCubicMeter;

        if (!supportedBaseUnit)
        {
            throw new NotSupportedException($"Base unit '{unitAttribute.BaseUnit}' is not supported.");
        }

        if (clone.UnitSystem == UnitSystem.Imperial)
        {
            var convertedValue = (double)value * unitAttribute.ConversionFactorMetricToImperial;

            if (applyRounding)
            {
                convertedValue = Math.Round(convertedValue, unitAttribute.DecimalsImperial);
            }

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(clone, convertedValue);
            }
        }
        else if (clone.UnitSystem == UnitSystem.Metric)
        {
            var convertedValue = (double)value / unitAttribute.ConversionFactorMetricToImperial;

            if (applyRounding)
            {
                convertedValue = Math.Round(convertedValue, unitAttribute.DecimalsMetric);
            }

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(clone, convertedValue);
            }
        }
        else
        {
            throw new InvalidOperationException($"{nameof(UnitSystem)} {clone.UnitSystem} is not supported");
        }
    }

    /// <summary>
    /// Finds ValueDependsOnUnitSystemAttribute on the property, searching both the concrete type property
    /// and the corresponding interface property (if any).
    /// </summary>
    private static ValueDependsOnUnitSystemAttribute? GetUnitSystemAttribute(PropertyInfo propertyInfo, Type concreteType)
    {
        // Try attribute on the class property
        var attribute = propertyInfo
            .GetCustomAttributes()
            .OfType<ValueDependsOnUnitSystemAttribute>()
            .FirstOrDefault();

        if (attribute != null)
        {
            return attribute;
        }

        // Try attribute on an interface property (implemented by the concrete type)
        foreach (var implementedInterface in concreteType.GetInterfaces())
        {
            var interfaceProperty = implementedInterface.GetProperty(propertyInfo.Name);
            if (interfaceProperty == null)
            {
                continue;
            }

            attribute = interfaceProperty
                .GetCustomAttributes()
                .OfType<ValueDependsOnUnitSystemAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                return attribute;
            }
        }

        return null;
    }

    /// <summary>
    /// Executes SwitchUnitSystem for a nested object using its runtime type via reflection.
    /// </summary>
    private static object SwitchUnitSystemDynamic(IContainsUnitSystemDependentProperties nestedObject, UnitSystem unitSystem, bool applyRounding)
    {
        var runtimeType = nestedObject.GetType();
        var genericSwitchMethod = typeof(UnitSystemExtensions)
            .GetMethod(nameof(SwitchUnitSystem), BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(runtimeType);

        return genericSwitchMethod.Invoke(null, new object[] { nestedObject, unitSystem, applyRounding })!;
    }

    /// <summary>
    /// Determines the element type of an enumerable (arrays and IEnumerable&lt;T&gt;).
    /// </summary>
    private static Type? GetEnumerableElementType(Type enumerableType)
    {
        // Arrays
        if (enumerableType.IsArray)
        {
            return enumerableType.GetElementType();
        }

        // Generic IEnumerable<T> (from self or interfaces)
        var ienumerableGeneric = enumerableType
            .GetInterfaces()
            .Concat(new[] { enumerableType })
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        return ienumerableGeneric?.GetGenericArguments().FirstOrDefault();
    }

    /// <summary>
    /// Converts each element of an enumerable that implements IContainsUnitSystemDependentProperties.
    /// Returns a List&lt;TElement&gt; of converted elements.
    /// Non-convertible elements are kept as-is.
    /// </summary>
    private static object ConvertEnumerable(System.Collections.IEnumerable source, Type elementType, UnitSystem unitSystem, bool applyRounding)
    {
        var listType = typeof(List<>).MakeGenericType(elementType);
        var convertedList = (System.Collections.IList)Activator.CreateInstance(listType)!;

        foreach (var item in source)
        {
            if (item is IContainsUnitSystemDependentProperties nested)
            {
                var convertedItem = SwitchUnitSystemDynamic(nested, unitSystem, applyRounding);
                convertedList.Add(convertedItem);
            }
            else
            {
                convertedList.Add(item);
            }
        }

        return convertedList;
    }
}