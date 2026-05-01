using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Shared helpers for localization extension methods.
/// </summary>
public static partial class LocalizationExtensions
{
    private static Type GetObjectType(object o)
    {
        if (o == null)
        {
            throw new ArgumentNullException(nameof(o));
        }

        return o.GetType();
    }

    private static TAttribute? GetPropertyAttribute<TAttribute>(Type type, string propertyName)
        where TAttribute : Attribute
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

        if (propertyInfo != null)
        {
            var propertyAttribute = propertyInfo.GetCustomAttribute<TAttribute>(true);

            if (propertyAttribute != null)
            {
                return propertyAttribute;
            }
        }

        foreach (var interfaceType in type.GetInterfaces())
        {
            propertyInfo = interfaceType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                continue;
            }

            var interfaceAttribute = propertyInfo.GetCustomAttribute<TAttribute>(true);

            if (interfaceAttribute != null)
            {
                return interfaceAttribute;
            }
        }

        return null;
    }

    private static IEnumerable<TAttribute> GetPropertyAttributes<TAttribute>(Type type, string propertyName)
        where TAttribute : Attribute
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

        if (propertyInfo != null)
        {
            var propertyAttributes = propertyInfo.GetCustomAttributes<TAttribute>(true).ToArray();

            if (propertyAttributes.Length > 0)
            {
                return propertyAttributes;
            }
        }

        foreach (var interfaceType in type.GetInterfaces())
        {
            propertyInfo = interfaceType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                continue;
            }

            var interfaceAttributes = propertyInfo.GetCustomAttributes<TAttribute>(true).ToArray();

            if (interfaceAttributes.Length > 0)
            {
                return interfaceAttributes;
            }
        }

        return Enumerable.Empty<TAttribute>();
    }

    private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
    {
        var propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .OrderBy(property => property.Name)
            .FirstOrDefault(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
        {
            throw new ArgumentException(@$"Property '{propertyName}' not found in type '{type.Name}'.", nameof(propertyName));
        }

        return propertyInfo;
    }

    private static void ValidateBooleanProperty(Type type, string propertyName)
    {
        var propertyInfo = GetPropertyInfo(type, propertyName);
        var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

        if (propertyType != typeof(bool))
        {
            throw new ArgumentException(@$"Property '{propertyName}' in type '{type.Name}' is not a boolean property.", nameof(propertyName));
        }
    }

    private static string GetResourceValue(Type resourceType, string resourceName, System.Globalization.CultureInfo culture)
    {
        var resourceManager = GetResourceManager(resourceType);

        if (resourceManager == null)
        {
            return string.Empty;
        }

        return resourceManager.GetString(resourceName, culture) ?? string.Empty;
    }

    private static string GetDisplayUnit(ValueDependsOnUnitSystemAttribute attribute, UnitSystem unitSystem, Type type)
    {
        return unitSystem switch
        {
            UnitSystem.Metric => attribute.DisplayUnitMetric,
            UnitSystem.Imperial => attribute.DisplayUnitImperial,
            _ => throw new InvalidOperationException($"Unsupported unit system '{unitSystem}' in object of type '{type.Name}'.")
        };
    }

    private static ResourceManager? GetResourceManager(Type resourceType)
    {
        var resourceManagerProperty = resourceType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
        return resourceManagerProperty?.GetValue(null) as ResourceManager;
    }
}
