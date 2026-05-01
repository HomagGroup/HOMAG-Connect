using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extension methods for localized property value formatting.
/// </summary>
public static partial class LocalizationExtensions
{
    extension(object o)
    {
        /// <summary>
        /// Gets the formatted value of the specified property by using the current UI culture.
        /// </summary>
        /// <param name="propertyName">The name of the property to format.</param>
        /// <returns>The formatted property value, or <see langword="null" /> when the property or its value is not available.</returns>
        public string? GetFormattedValue(string propertyName)
        {
            return o.GetFormattedValue(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the formatted value of the specified property in the specified culture.
        /// </summary>
        /// <param name="propertyName">The name of the property to format.</param>
        /// <param name="culture">The culture used to format the property value.</param>
        /// <returns>The formatted property value, or <see langword="null" /> when the property or its value is not available.</returns>
        public string? GetFormattedValue(string propertyName, CultureInfo culture)
        {
            var type = GetObjectType(o);
            var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

            if (propertyInfo == null)
            {
                return null;
            }

            var propertyValue = propertyInfo.GetValue(o);

            if (propertyValue == null)
            {
                return null;
            }

            var displayFormatAttribute = propertyInfo.GetCustomAttribute<DisplayFormatAttribute>();

            if (!string.IsNullOrWhiteSpace(displayFormatAttribute?.DataFormatString))
            {
                return string.Format(culture, displayFormatAttribute.DataFormatString, propertyValue);
            }

            return Convert.ToString(propertyValue, culture);
        }
    }

    extension(IContainsUnitSystemDependentProperties o)
    {
        /// <summary>
        /// Gets the formatted value of the specified property by using the current UI culture and optionally appends the current unit-system display unit.
        /// </summary>
        /// <param name="propertyName">The name of the property to format.</param>
        /// <param name="includingUnitSystem">A value indicating whether the metric or imperial display unit should be appended.</param>
        /// <returns>The formatted property value, or <see langword="null" /> when the property or its value is not available.</returns>
        public string? GetFormattedValue(string propertyName, bool includingUnitSystem)
        {
            return o.GetFormattedValue(propertyName, includingUnitSystem, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the formatted value of the specified property in the specified culture and optionally appends the current unit-system display unit.
        /// </summary>
        /// <param name="propertyName">The name of the property to format.</param>
        /// <param name="includingUnitSystem">A value indicating whether the metric or imperial display unit should be appended.</param>
        /// <param name="culture">The culture used to format the property value.</param>
        /// <returns>The formatted property value, or <see langword="null" /> when the property or its value is not available.</returns>
        public string? GetFormattedValue(string propertyName, bool includingUnitSystem, CultureInfo culture)
        {
            var type = GetObjectType(o);
            var formattedValue = o.GetFormattedValue(propertyName, culture);

            if (!includingUnitSystem || string.IsNullOrEmpty(formattedValue))
            {
                return formattedValue;
            }

            var valueDependsOnUnitSystemAttribute = GetPropertyAttribute<ValueDependsOnUnitSystemAttribute>(type, propertyName);

            if (valueDependsOnUnitSystemAttribute == null)
            {
                return formattedValue;
            }

            return $"{formattedValue} {GetDisplayUnit(valueDependsOnUnitSystemAttribute, o.UnitSystem, type)}";
        }
    }
}
