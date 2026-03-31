using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.Base.TestBase.Helpers;

/// <summary>
/// Provides helper methods for creating instances and populating public properties with generic test values.
/// </summary>
public static class PublicPropertyValueInitializer
{
    private const string _MetricEnumValueName = "Metric";

    /// <summary>
    /// Creates an instance of the specified <paramref name="type" /> using a supported constructor.
    /// </summary>
    public static object CreateInstance(Type type)
    {
        if (TryCreateInstance(type, out var instance))
        {
            return instance!;
        }

        throw new InvalidOperationException($"No supported constructor was found for '{type.FullName}'.");
    }

    /// <summary>
    /// Attempts to create an instance of the specified <paramref name="type" /> using a supported constructor.
    /// </summary>
    public static bool TryCreateInstance(Type type, out object? instance)
    {
        if (TryCreateWithParameterlessConstructor(type, out instance))
        {
            return true;
        }

        if (TryCreateWithUnitSystemConstructor(type, out instance))
        {
            return true;
        }

        instance = null;
        return false;
    }

    /// <summary>
    /// Populates all public writable properties of the specified <paramref name="instance" /> with generic values based on their data types.
    /// </summary>
    public static void PopulatePublicProperties(object instance)
    {
        PopulatePublicProperties(instance, new HashSet<Type>());
    }

    private static bool TryCreateWithParameterlessConstructor(Type type, out object? instance)
    {
        var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
        if (parameterlessConstructor == null)
        {
            instance = null;
            return false;
        }

        instance = parameterlessConstructor.Invoke(null);
        return true;
    }

    private static bool TryCreateWithUnitSystemConstructor(Type type, out object? instance)
    {
        var unitSystemConstructor = type
            .GetConstructors()
            .FirstOrDefault(constructor => IsUnitSystemConstructor(constructor.GetParameters()));

        if (unitSystemConstructor == null)
        {
            instance = null;
            return false;
        }

        var parameterType = unitSystemConstructor.GetParameters()[0].ParameterType;
        var metricValue = Enum.Parse(parameterType, _MetricEnumValueName);
        instance = unitSystemConstructor.Invoke([metricValue]);
        return true;
    }

    private static bool IsUnitSystemConstructor(ParameterInfo[] parameters)
    {
        return parameters.Length == 1 &&
               parameters[0].ParameterType.IsEnum &&
               string.Equals(parameters[0].ParameterType.Name, "UnitSystem", StringComparison.Ordinal) &&
               Enum.GetNames(parameters[0].ParameterType).Contains(_MetricEnumValueName, StringComparer.Ordinal);
    }

    private static void PopulatePublicProperties(object instance, ISet<Type> visitedTypes)
    {
        var instanceType = instance.GetType();
        if (!visitedTypes.Add(instanceType))
        {
            return;
        }

        foreach (var property in instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.GetIndexParameters().Length > 0)
            {
                continue;
            }

            if (property.SetMethod?.IsPublic != true)
            {
                PopulateExistingNestedProperty(instance, property, visitedTypes);
                continue;
            }

            var propertyValue = CreateGenericValue(property.PropertyType, property.Name, visitedTypes);
            property.SetValue(instance, propertyValue);

            if (propertyValue != null && ShouldPopulateNestedObject(property.PropertyType))
            {
                PopulatePublicProperties(propertyValue, visitedTypes);
            }
        }

        visitedTypes.Remove(instanceType);
    }

    private static void PopulateExistingNestedProperty(object instance, PropertyInfo property, ISet<Type> visitedTypes)
    {
        var currentValue = property.GetValue(instance);
        if (currentValue != null && ShouldPopulateNestedObject(property.PropertyType))
        {
            PopulatePublicProperties(currentValue, visitedTypes);
        }
    }

    private static object? CreateGenericValue(Type propertyType, string propertyName, ISet<Type> visitedTypes)
    {
        var underlyingType = Nullable.GetUnderlyingType(propertyType);
        if (underlyingType != null)
        {
            return CreateGenericValue(underlyingType, propertyName, visitedTypes);
        }

        if (string.Equals(propertyName, nameof(ISupportsAdditionalProperties.AdditionalProperties), StringComparison.Ordinal))
        {
            return null;
        }

        if (propertyType == typeof(string))
        {
            return $"{propertyName}Value";
        }

        if (propertyType == typeof(bool))
        {
            return true;
        }

        if (IsIntegerType(propertyType))
        {
            return Convert.ChangeType(1, propertyType);
        }

        if (IsFloatingPointType(propertyType))
        {
            return Convert.ChangeType(1.5, propertyType);
        }

        if (propertyType == typeof(Guid))
        {
            return Guid.Parse("11111111-1111-1111-1111-111111111111");
        }

        if (propertyType == typeof(DateTime))
        {
            return new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        }

        if (propertyType == typeof(DateTimeOffset))
        {
            return new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero);
        }

        if (propertyType == typeof(TimeSpan))
        {
            return TimeSpan.FromMinutes(1);
        }

        if (propertyType == typeof(Uri))
        {
            return new Uri("https://example.com/resource");
        }

        if (propertyType == typeof(IDictionary<string, object>) || propertyType == typeof(Dictionary<string, object>))
        {
            return new Dictionary<string, object>
            {
                ["customProperty"] = "custom value"
            };
        }

        if (propertyType.IsEnum)
        {
            return GetEnumValue(propertyType);
        }

        if (propertyType.IsArray)
        {
            return CreateArrayValue(propertyType, propertyName, visitedTypes);
        }

        if (TryCreateReadOnlyCollectionValue(propertyType, out var readOnlyCollectionValue))
        {
            return readOnlyCollectionValue;
        }

        if (TryGetEnumerableElementType(propertyType, out var enumerableElementType))
        {
            return CreateEnumerableValue(enumerableElementType, propertyName, visitedTypes);
        }

        if (propertyType.IsClass && propertyType != typeof(object) && !visitedTypes.Contains(propertyType))
        {
            return TryCreateInstance(propertyType, out var contractInstance) ? contractInstance : null;
        }

        return null;
    }

    private static bool IsIntegerType(Type propertyType)
    {
        return propertyType == typeof(byte) ||
               propertyType == typeof(short) ||
               propertyType == typeof(int) ||
               propertyType == typeof(long);
    }

    private static bool IsFloatingPointType(Type propertyType)
    {
        return propertyType == typeof(float) ||
               propertyType == typeof(double) ||
               propertyType == typeof(decimal);
    }

    private static object? CreateArrayValue(Type propertyType, string propertyName, ISet<Type> visitedTypes)
    {
        var elementType = propertyType.GetElementType();
        if (elementType == null)
        {
            return null;
        }

        var array = Array.CreateInstance(elementType, 1);
        array.SetValue(CreateGenericValue(elementType, propertyName, visitedTypes), 0);
        return array;
    }

    private static bool TryCreateReadOnlyCollectionValue(Type propertyType, out object? value)
    {
        if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(IReadOnlyCollection<>))
        {
            value = null;
            return false;
        }

        var elementType = propertyType.GetGenericArguments()[0];
        var listType = typeof(List<>).MakeGenericType(elementType);
        value = Activator.CreateInstance(listType);
        return true;
    }

    private static object CreateEnumerableValue(Type enumerableElementType, string propertyName, ISet<Type> visitedTypes)
    {
        var listType = typeof(List<>).MakeGenericType(enumerableElementType);
        var list = (IList)Activator.CreateInstance(listType)!;

        var elementValue = CreateGenericValue(enumerableElementType, propertyName, visitedTypes);
        if (elementValue != null)
        {
            list.Add(elementValue);
        }

        return list;
    }

    private static bool ShouldPopulateNestedObject(Type propertyType)
    {
        var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        return underlyingType.IsClass &&
               underlyingType != typeof(string) &&
               underlyingType != typeof(Uri) &&
               underlyingType != typeof(object) &&
               !typeof(IEnumerable).IsAssignableFrom(underlyingType);
    }

    private static bool TryGetEnumerableElementType(Type propertyType, out Type elementType)
    {
        if (!propertyType.IsGenericType)
        {
            elementType = null!;
            return false;
        }

        var genericTypeDefinition = propertyType.GetGenericTypeDefinition();
        if (genericTypeDefinition != typeof(IEnumerable<>) &&
            genericTypeDefinition != typeof(IReadOnlyCollection<>) &&
            genericTypeDefinition != typeof(ICollection<>) &&
            genericTypeDefinition != typeof(IList<>) &&
            genericTypeDefinition != typeof(List<>))
        {
            elementType = null!;
            return false;
        }

        elementType = propertyType.GetGenericArguments()[0];
        return true;
    }

    private static object GetEnumValue(Type enumType)
    {
        var values = Enum.GetValues(enumType).Cast<object>().ToArray();
        return values.Length > 1 ? values[1] : values[0];
    }
}
