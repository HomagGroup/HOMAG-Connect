using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extension methods for localized property display names.
/// </summary>
public static partial class LocalizationExtensions
{
    extension(object o)
    {
        /// <summary>
        /// Gets the display name of the specified property by using the current UI culture.
        /// </summary>
        public string GetPropertyDisplayName(string propertyName)
        {
            return GetObjectType(o).GetPropertyDisplayName(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the display name of the specified property in the specified culture.
        /// </summary>
        public string GetPropertyDisplayName(string propertyName, CultureInfo culture)
        {
            return GetObjectType(o).GetPropertyDisplayName(propertyName, culture);
        }

        /// <summary>
        /// Gets the display names of all properties by using the current UI culture.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames()
        {
            return o.GetPropertyDisplayNames(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the display names of all properties in the specified culture.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames(CultureInfo culture)
        {
            return GetObjectType(o).GetPropertyDisplayNames(culture);
        }

        /// <summary>
        /// Gets the display name of the specified property by using the current UI culture.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames(string propertyName)
        {
            return o.GetPropertyDisplayNames(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the display name of the specified property in the specified culture.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames(string propertyName, CultureInfo culture)
        {
            return GetObjectType(o).GetPropertyDisplayNames(propertyName, culture);
        }
    }

    extension(IContainsUnitSystemDependentProperties o)
    {
        /// <summary>
        /// Gets the display names of all properties in the specified culture and optionally appends unit-system display units.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames(CultureInfo culture, bool includingUnitSystem)
        {
            var type = GetObjectType(o);
            var propertyDisplayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(propertyInfo => propertyInfo.Name);

            foreach (var propertyInfo in propertyInfos)
            {
                var displayName = GetLocalizedPropertyDisplayName(type, propertyInfo.Name, culture);

                if (includingUnitSystem)
                {
                    displayName = AppendDisplayUnit(displayName, propertyInfo, o.UnitSystem, type);
                }

                propertyDisplayNames.Add(propertyInfo.Name, FirstCharToUpper(displayName));
            }

            return propertyDisplayNames;
        }
    }

    /// <summary>
    /// Gets the display names of all properties in the specified type by using the current UI culture.
    /// </summary>
    public static IDictionary<string, string> GetPropertyDisplayNames<T>()
    {
        return typeof(T).GetPropertyDisplayNames(CultureInfo.CurrentUICulture);
    }

    /// <summary>
    /// Gets the display names of all properties in the specified type in the specified culture.
    /// </summary>
    public static IDictionary<string, string> GetPropertyDisplayNames<T>(CultureInfo culture)
    {
        return typeof(T).GetPropertyDisplayNames(culture);
    }

    /// <summary>
    /// Gets the display name of the specified property in the specified culture.
    /// </summary>
    public static string GetPropertyDisplayName(this Type type, string propertyName, CultureInfo culture)
    {
        return FirstCharToUpper(GetLocalizedPropertyDisplayName(type, propertyName, culture));
    }

    /// <summary>
    /// Gets the display names of all properties in the specified type in the specified culture.
    /// </summary>
    public static IDictionary<string, string> GetPropertyDisplayNames(this Type type, CultureInfo culture)
    {
        var propertyDisplayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var propertyNames = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(propertyInfo => propertyInfo.Name).Select(propertyInfo => propertyInfo.Name);

        foreach (var propertyName in propertyNames)
        {
            propertyDisplayNames.Add(propertyName, FirstCharToUpper(GetLocalizedPropertyDisplayName(type, propertyName, culture)));
        }

        return propertyDisplayNames;
    }

    private static string GetLocalizedPropertyDisplayName(Type type, string propertyName, CultureInfo culture)
    {
        var displayAttribute = GetPropertyAttribute<DisplayAttribute>(type, propertyName);

        if (displayAttribute == null)
        {
            return propertyName;
        }

        var propertyDisplayName = GetPropertyDisplayName(displayAttribute, culture);
        return string.IsNullOrWhiteSpace(propertyDisplayName) ? propertyName : propertyDisplayName;
    }

    private static string GetPropertyDisplayName(DisplayAttribute displayAttribute, CultureInfo culture)
    {
        var resourceType = displayAttribute.ResourceType;
        var resourceName = displayAttribute.Name;

        if (resourceType != null && !string.IsNullOrWhiteSpace(resourceName))
        {
            var resourceManager = new ResourceManager(resourceType);
            var resourceValue = resourceManager.GetString(resourceName, culture);

            if (!string.IsNullOrWhiteSpace(resourceValue))
            {
                return resourceValue;
            }
        }

        return displayAttribute.GetName();
    }

    private static string AppendDisplayUnit(string displayName, PropertyInfo propertyInfo, Contracts.Enumerations.UnitSystem unitSystem, Type declaringType)
    {
        var attribute = propertyInfo.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>(true);

        if (attribute == null)
        {
            return displayName;
        }

        return $"{displayName} ({GetDisplayUnit(attribute, unitSystem, declaringType)})";
    }

    private static string FirstCharToUpper(string property)
    {
        return string.IsNullOrEmpty(property) ? string.Empty : $"{property[0].ToString().ToUpper()}{property.Substring(1)}";
    }
}
