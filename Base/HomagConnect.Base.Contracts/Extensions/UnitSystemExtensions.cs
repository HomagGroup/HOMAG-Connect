using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Extensions;

public static class UnitSystemExtensions
{
    private const int _MillimeterToMeterConversionFactor = 1000;
    private const int _SquareInchToSquareFootConversionFactor = 144;

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

        // Recursively find and convert all properties annotated with ValueDependsOnUnitSystemAttribute
        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        TraverseAndConvertAttributes(clone, unitSystem, applyRounding, visited);

        return clone;
    }

    private static double ConvertMillimeterToMeter(double value) => value / _MillimeterToMeterConversionFactor;

    private static double ConvertSquareInchToSquareFoot(double value) => value / _SquareInchToSquareFootConversionFactor;

    private static void ConvertValueWithBaseUnit(
        PropertyInfo propertyInfo,
        object owner,
        UnitSystem targetUnitSystem,
        ValueDependsOnUnitSystemAttribute unitAttribute,
        bool applyRounding)
    {
        var value = propertyInfo.GetValue(owner);
        if (value == null)
        {
            return;
        }

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

        if (targetUnitSystem == UnitSystem.Imperial)
        {
            var convertedValue = (double)value * unitAttribute.ConversionFactorMetricToImperial;

            if (applyRounding)
            {
                convertedValue = Math.Round(convertedValue, unitAttribute.DecimalsImperial);
            }

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(owner, convertedValue);
            }
        }
        else if (targetUnitSystem == UnitSystem.Metric)
        {
            var convertedValue = (double)value / unitAttribute.ConversionFactorMetricToImperial;

            if (applyRounding)
            {
                convertedValue = Math.Round(convertedValue, unitAttribute.DecimalsMetric);
            }

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(owner, convertedValue);
            }
        }
        else
        {
            throw new InvalidOperationException($"{nameof(UnitSystem)} {targetUnitSystem} is not supported");
        }
    }

    private static ValueDependsOnUnitSystemAttribute? GetUnitSystemAttribute(PropertyInfo propertyInfo, Type concreteType)
    {
        var attribute = propertyInfo
            .GetCustomAttributes()
            .OfType<ValueDependsOnUnitSystemAttribute>()
            .FirstOrDefault();

        if (attribute != null)
        {
            return attribute;
        }

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

    private static bool IsClassWithParameterlessCtor(Type type)
    {
        // Must be a reference type and have a public parameterless constructor
        return type.IsClass && type.GetConstructor(Type.EmptyTypes) != null;
    }   

    private static object SwitchUnitSystemDynamic(IContainsUnitSystemDependentProperties nestedObject, UnitSystem unitSystem, bool applyRounding)
    {
        var runtimeType = nestedObject.GetType();
        var genericSwitchMethod = typeof(UnitSystemExtensions)
            .GetMethod(nameof(SwitchUnitSystem), BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(runtimeType);

        return genericSwitchMethod.Invoke(null, new object[] { nestedObject, unitSystem, applyRounding })!;
    }

    private static void TraverseAndConvertAttributes(object? obj, UnitSystem unitSystem, bool applyRounding, HashSet<object> visited)
    {
        if (obj == null)
        {
            return;
        }

        var type = obj.GetType();
        if (IsSimple(type))
        {
            return;
        }

        if (!visited.Add(obj))
        {
            return; // avoid cycles
        }

        if (obj is IContainsUnitSystemDependentProperties unitAware)
        {
            unitAware.UnitSystem = unitSystem;
        }

        // Enumerables (lists/arrays/etc.): recurse into elements
        if (obj is System.Collections.IDictionary dict)
        {
            foreach (System.Collections.DictionaryEntry entry in dict)
            {
                TraverseAndConvertAttributes(entry.Value, unitSystem, applyRounding, visited);
            }
            return;
        }

        if (obj is System.Collections.IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                TraverseAndConvertAttributes(item, unitSystem, applyRounding, visited);
            }
            return;
        }

        // POCO: convert annotated properties; also recurse into nested objects
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead)
                        .ToArray();

        foreach (var prop in props)
        {
            if (prop.GetIndexParameters().Length > 0)
            {
                continue;
            }

            var unitAttr = GetUnitSystemAttribute(prop, type);
            if (unitAttr != null)
            {
                ConvertValueWithBaseUnit(prop, obj, unitSystem, unitAttr, applyRounding);
            }

            var nested = prop.GetValue(obj);
            if (nested == null)
            {
                continue;
            }

            TraverseAndConvertAttributes(nested, unitSystem, applyRounding, visited);
        }
    }

    private static bool IsSimple(Type type)
    {
        return type.IsPrimitive
               || type.IsEnum
               || type == typeof(string)
               || type == typeof(decimal)
               || type == typeof(DateTime)
               || type == typeof(Guid)
               || type == typeof(TimeSpan);
    }

    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();

        public new bool Equals(object x, object y) => ReferenceEquals(x, y);

        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }

    private static Type? GetEnumerableElementType(Type enumerableType)
    {
        if (enumerableType.IsArray)
        {
            return enumerableType.GetElementType();
        }

        var ienumerableGeneric = enumerableType
            .GetInterfaces()
            .Concat(new[] { enumerableType })
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        return ienumerableGeneric?.GetGenericArguments().FirstOrDefault();
    }

    private static object ConvertEnumerable(System.Collections.IEnumerable source, Type? elementType, UnitSystem unitSystem, bool applyRounding)
    {
        // Preserve dictionaries
        if (source is System.Collections.IDictionary dict)
        {
            return ConvertDictionary(dict, unitSystem, applyRounding);
        }

        var sourceType = source.GetType();

        // Preserve arrays
        if (sourceType.IsArray)
        {
            var array = (Array)source;
            var elemType = elementType ?? sourceType.GetElementType() ?? typeof(object);
            var result = Array.CreateInstance(elemType, array.Length);
            var i = 0;
            foreach (var item in array)
            {
                var convertedItem = DeepConvertNested(item, unitSystem, applyRounding);
                result.SetValue(convertedItem, i++);
            }
            return result;
        }

        // First, materialize converted items into a List<T> as a staging collection
        var listElemType = elementType ?? typeof(object);
        var tempListType = typeof(List<>).MakeGenericType(listElemType);
        var tempList = (System.Collections.IList)Activator.CreateInstance(tempListType)!;
        foreach (var item in source)
        {
            var convertedItem = DeepConvertNested(item, unitSystem, applyRounding);
            tempList.Add(convertedItem!);
        }

        // Try to instantiate the same concrete collection type and populate it
        if (!sourceType.IsInterface && !sourceType.IsAbstract)
        {
            // If it supports non-generic IList, use it
            if (typeof(System.Collections.IList).IsAssignableFrom(sourceType))
            {
                var target = (System.Collections.IList)Activator.CreateInstance(sourceType)!;
                foreach (var item in (System.Collections.IEnumerable)tempList)
                {
                    target.Add(item);
                }
                return target;
            }

            // If it has a parameterless ctor and an Add(T) method, use that
            var parameterlessCtor = sourceType.GetConstructor(Type.EmptyTypes);
            if (parameterlessCtor != null)
            {
                var target = Activator.CreateInstance(sourceType)!;
                var addMethod = sourceType
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(m => m.Name == "Add" && m.GetParameters().Length == 1);

                if (addMethod != null)
                {
                    foreach (var item in (System.Collections.IEnumerable)tempList)
                    {
                        addMethod.Invoke(target, new[] { item });
                    }
                    return target;
                }
            }

            // Support collection types with ctor(IList<T>) or ctor(IEnumerable<T>) e.g., ReadOnlyCollection<T>
            var iListOfT = typeof(IList<>).MakeGenericType(listElemType);
            var iEnumerableOfT = typeof(IEnumerable<>).MakeGenericType(listElemType);

            var ctorIList = sourceType.GetConstructor(new[] { iListOfT });
            if (ctorIList != null)
            {
                return ctorIList.Invoke(new object[] { tempList });
            }

            var ctorIEnumerable = sourceType.GetConstructor(new[] { iEnumerableOfT });
            if (ctorIEnumerable != null)
            {
                return ctorIEnumerable.Invoke(new object[] { tempList });
            }
        }

        // Fallback to List<T>
        return tempList;
    }

    /// <summary>
    /// Recursively visits any nested value:
    /// - If it implements IContainsUnitSystemDependentProperties, converts via SwitchUnitSystemDynamic.
    /// - If it is an enumerable, converts each element recursively (regardless of declared element type).
    /// - If it is a POCO (class), visits its public instance properties and converts nested values in place.
    /// Returns the (potentially converted) object reference.
    /// </summary>
    private static object? DeepConvertNested(object? value, UnitSystem unitSystem, bool applyRounding)
    {
        if (value == null)
        {
            return null;
        }

        if (value is IContainsUnitSystemDependentProperties unitAware)
        {
            return SwitchUnitSystemDynamic(unitAware, unitSystem, applyRounding);
        }

        var type = value.GetType();
        if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid) || type == typeof(TimeSpan))
        {
            return value;
        }

        // Handle IDictionary before IEnumerable
        if (value is System.Collections.IDictionary dictionary)
        {
            return ConvertDictionary(dictionary, unitSystem, applyRounding);
        }

        if (value is System.Collections.IEnumerable enumerable)
        {
            var elementType = GetEnumerableElementType(type);
            return ConvertEnumerable(enumerable, elementType, unitSystem, applyRounding);
        }

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead)
                        .ToArray();

        foreach (var prop in props)
        {
            // Skip indexers
            if (prop.GetIndexParameters().Length > 0)
            {
                continue;
            }

            var nestedValue = prop.GetValue(value);
            if (nestedValue == null)
            {       
                continue;
            }

            var convertedNested = DeepConvertNested(nestedValue, unitSystem, applyRounding);

            if (!ReferenceEquals(convertedNested, nestedValue) && prop.CanWrite)
            {
                prop.SetValue(value, convertedNested);
            }
        }

        return value;
    }

    private static object ConvertDictionary(System.Collections.IDictionary source, UnitSystem unitSystem, bool applyRounding)
    {
        var type = source.GetType();

        var idictType = type
            .GetInterfaces()
            .Concat(new[] { type })
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));

        var keyType = typeof(object);
        var valueType = typeof(object);

        if (idictType != null)
        {
            var args = idictType.GetGenericArguments();
            keyType = args[0];
            valueType = args[1];
        }

        // Prefer same concrete type if possible, else Dictionary<TKey,TValue>
        Type targetType;
        if (type.IsInterface || type.IsAbstract || type.GetConstructor(Type.EmptyTypes) == null)
        {
            targetType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
        }
        else
        {
            targetType = type;
        }

        var result = (System.Collections.IDictionary)Activator.CreateInstance(targetType)!;

        foreach (System.Collections.DictionaryEntry entry in source)
        {
            var convertedValue = DeepConvertNested(entry.Value, unitSystem, applyRounding);
            result[entry.Key] = convertedValue;
        }

        return result;
    }
}