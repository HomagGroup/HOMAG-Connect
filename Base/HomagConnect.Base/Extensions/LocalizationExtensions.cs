using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extension methods for localization.
/// </summary>
public static class LocalizationExtensions
{
    /// <summary>
    /// Gets the display name of the specified property in the specified object.
    /// </summary>
    public static string GetPropertyDisplayName(this object o, string propertyName)
    {
        return o.GetType().GetPropertyDisplayName(propertyName, CultureInfo.CurrentUICulture);
    }

    /// <summary>
    /// Gets the display name of the specified property in the specified object.
    /// </summary>
    public static string GetPropertyDisplayName(this object o, string propertyName, CultureInfo culture)
    {
        return o.GetType().GetPropertyDisplayName(propertyName, culture);
    }

    /// <summary>
    /// Gets the display name of the specified property in the specified object.
    /// </summary>
    public static string GetPropertyDisplayName(this Type type, string propertyName, CultureInfo culture)
    {
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(p => p.Name);
        var propertyInfo = propertyInfos.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
        {
            throw new ArgumentException(@$"Property '{propertyName}' not found in type '{type.Name}'.", nameof(propertyName));
        }
        
        var propertyDisplayName = "";
        var displayAttribute = GetDisplayAttribute(type, propertyName);

        if (displayAttribute != null)
        {
            propertyDisplayName = GetPropertyDisplayName(displayAttribute, culture);
        }

        if (string.IsNullOrWhiteSpace(propertyDisplayName))
        {
            propertyDisplayName = propertyName;
        }

        return propertyDisplayName;
    }

    /// <summary>
    /// Gets the display names of all properties in the specified object.
    /// </summary>
    public static IDictionary<string, string> GetPropertyDisplayNames(this object o)
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
    public static IDictionary<string, string> GetPropertyDisplayNames(this object o, CultureInfo culture)
    {
        if (o == null)
        {
            throw new ArgumentNullException(nameof(o));
        }

        return o.GetType().GetPropertyDisplayNames(culture);
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
            var propertyDisplayName = "";

            var displayAttribute = GetDisplayAttribute(type, propertyName);
          
            if (displayAttribute != null)
            {
                propertyDisplayName = GetPropertyDisplayName(displayAttribute, culture);
            }

            if (string.IsNullOrWhiteSpace(propertyDisplayName))
            {
                propertyDisplayName = propertyName;
            }

            propertyDisplayNames.Add(propertyName, propertyDisplayName);
        }

        return propertyDisplayNames;
    }



    #region Private Methods

    private static DisplayAttribute? GetDisplayAttribute(Type type, string propertyName)
    {
        // Check the class itself

        DisplayAttribute displayAttribute = null;
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

        if (propertyInfo != null)
        {
            displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>(true);

            if  (displayAttribute != null)
            {
                return displayAttribute;
            }
        }


        // Check the interfaces implemented by the class

        foreach (var i in type.GetInterfaces())
        {
            propertyInfo = i.GetProperty(propertyName);

           if (propertyInfo != null)
           {
               displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>(true);

               if (displayAttribute != null)
               {
                   return displayAttribute;
               }
            }
        }

        return null;
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

    #endregion
}