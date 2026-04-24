using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extension methods for localization.
/// </summary>
public static class LocalizationExtensions
{
    extension(object o)
    {
        /// <summary>
        /// Gets the localized display value of a boolean property in the specified object.
        /// </summary>
        public string GetBooleanPropertyDisplayValue(string propertyName)
        {
            return o.GetBooleanPropertyDisplayValue(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the localized display values of a boolean property in the specified object.
        /// </summary>
        public IDictionary<bool, string> GetBooleanPropertyDisplayValues(string propertyName)
        {
            return o.GetBooleanPropertyDisplayValues(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the localized display values of a boolean property in the specified object.
        /// </summary>
        public IDictionary<bool, string> GetBooleanPropertyDisplayValues(string propertyName, CultureInfo culture)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            ValidateBooleanProperty(o.GetType(), propertyName);

            return new Dictionary<bool, string>
            {
                { true, GetBooleanDisplayValue(o.GetType(), propertyName, true, culture) },
                { false, GetBooleanDisplayValue(o.GetType(), propertyName, false, culture) }
            };
        }

        /// <summary>
        /// Gets the localized display value of a boolean property in the specified object.
        /// </summary>
        public string GetBooleanPropertyDisplayValue(string propertyName, CultureInfo culture)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            ValidateBooleanProperty(o.GetType(), propertyName);

            var propertyInfo = GetPropertyInfo(o.GetType(), propertyName);

            var propertyValue = propertyInfo.GetValue(o);

            if (propertyValue == null)
            {
                return string.Empty;
            }

            return GetBooleanDisplayValue(o.GetType(), propertyName, (bool)propertyValue, culture);
        }
    }

    /// <summary>
    /// Gets the localized display value for a specific boolean value of the specified property.
    /// </summary>
    private static string GetBooleanDisplayValue(Type type, string propertyName, bool booleanValue, CultureInfo culture)
    {
        var booleanValueDisplayAttribute = GetPropertyAttributes<BooleanValueDisplayAttribute>(type, propertyName)
            .FirstOrDefault(attribute => attribute.Value == booleanValue);

        if (booleanValueDisplayAttribute != null)
        {
            var localizedDisplayValue = GetResourceValue(booleanValueDisplayAttribute.ResourceType, booleanValueDisplayAttribute.ResourceName, culture);

            if (!string.IsNullOrWhiteSpace(localizedDisplayValue))
            {
                return localizedDisplayValue;
            }
        }

        var fallbackResourceName = booleanValue
            ? nameof(Contracts.Resources.BooleanValue_Yes)
            : nameof(Contracts.Resources.BooleanValue_No);

        var fallbackDisplayValue = GetResourceValue(typeof(Contracts.Resources), fallbackResourceName, culture);

        if (!string.IsNullOrWhiteSpace(fallbackDisplayValue))
        {
            return fallbackDisplayValue;
        }

        return booleanValue ? "Yes" : "No";
    }

    extension(object o)
    {
        /// <summary>
        /// Gets the display name of the specified property in the specified object.
        /// </summary>
        public string GetPropertyDisplayName(string propertyName)
        {
            return o.GetType().GetPropertyDisplayName(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the display name of the specified property in the specified object.
        /// </summary>
        public string GetPropertyDisplayName(string propertyName, CultureInfo culture)
        {
            return o.GetType().GetPropertyDisplayName(propertyName, culture);
        }
    }

    /// <summary>
    /// Gets the display name of the specified property in the specified object.
    /// </summary>
    public static string GetPropertyDisplayName(this Type type, string propertyName, CultureInfo culture)
    {
        return FirstCharToUpper(GetLocalizedPropertyDisplayName(type, propertyName, culture));
    }

    extension(object o)
    {
        /// <summary>
        /// Gets the display names of all properties in the specified object.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames()
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            return o.GetPropertyDisplayNames(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the display names of all properties in the specified object in the specified culture.
        /// </summary>
        public IDictionary<string, string> GetPropertyDisplayNames(CultureInfo culture)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            return o.GetType().GetPropertyDisplayNames(culture);
        }
    }

    /// <summary>
    /// Gets the display names of all properties in the specified type.
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
    /// Gets the display names of all properties in the specified type in the specified culture.
    /// </summary>
    public static IDictionary<string, string> GetPropertyDisplayNames(this Type type, CultureInfo culture)
    {
        var propertyDisplayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var propertyNames = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(p => p.Name).Select(p => p.Name);

        foreach (var propertyName in propertyNames)
        {
            propertyDisplayNames.Add(propertyName, FirstCharToUpper(GetLocalizedPropertyDisplayName(type, propertyName, culture)));
        }

        return propertyDisplayNames;
    }



    #region Private Methods

    private static DisplayAttribute? GetDisplayAttribute(Type type, string propertyName)
    {
        return GetPropertyAttribute<DisplayAttribute>(type, propertyName);
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

        foreach (var i in type.GetInterfaces())
        {
            propertyInfo = i.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var interfaceAttribute = propertyInfo.GetCustomAttribute<TAttribute>(true);

                if (interfaceAttribute != null)
                {
                    return interfaceAttribute;
                }
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

        foreach (var i in type.GetInterfaces())
        {
            propertyInfo = i.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var interfaceAttributes = propertyInfo.GetCustomAttributes<TAttribute>(true).ToArray();

                if (interfaceAttributes.Length > 0)
                {
                    return interfaceAttributes;
                }
            }
        }

        return Enumerable.Empty<TAttribute>();
    }

    private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
    {
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(p => p.Name);
        var propertyInfo = propertyInfos.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

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

    private static string GetLocalizedPropertyDisplayName(Type type, string propertyName, CultureInfo culture)
    {
        var displayAttribute = GetDisplayAttribute(type, propertyName);

        if (displayAttribute == null)
        {
            return propertyName;
        }

        var propertyDisplayName = GetPropertyDisplayName(displayAttribute, culture);
        return string.IsNullOrWhiteSpace(propertyDisplayName) ? propertyName : propertyDisplayName;
    }
    
    private static string GetPropertyDisplayName(DisplayAttribute displayAttribute, CultureInfo culture)
    {
        var propertyDisplayName = "";

        var resourceType = displayAttribute.ResourceType;
        var resourceName = displayAttribute.Name;

        if (resourceType != null && !string.IsNullOrWhiteSpace(resourceName))
        {
            var resourceManager = new ResourceManager(resourceType);
            var resourceValue = resourceManager.GetString(resourceName, culture);

            if (!string.IsNullOrWhiteSpace(resourceValue))
            {
                propertyDisplayName = resourceValue;
            }
        }
        else 
        {
            propertyDisplayName = displayAttribute.GetName();
        }

        return propertyDisplayName;
    }

    private static string GetResourceValue(Type resourceType, string resourceName, CultureInfo culture)
    {
        var resourceManager = GetResourceManager(resourceType);

        if (resourceManager == null)
        {
            return string.Empty;
        }

        return resourceManager.GetString(resourceName, culture) ?? string.Empty;
    }

    private static ResourceManager? GetResourceManager(Type resourceType)
    {
        var resourceManagerProperty = resourceType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
        return resourceManagerProperty?.GetValue(null) as ResourceManager;
    }

    private static string FirstCharToUpper(string property)
    {
        return string.IsNullOrEmpty(property) ? string.Empty : $"{property[0].ToString().ToUpper()}{property.Substring(1)}";
    }

    #endregion
}