using HomagConnect.Base.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extension methods for localized boolean display values.
/// </summary>
public static partial class LocalizationExtensions
{
    extension(object o)
    {
        /// <summary>
        /// Gets the localized display value of a boolean property by using the current UI culture.
        /// </summary>
        public string GetBooleanPropertyDisplayValue(string propertyName)
        {
            return o.GetBooleanPropertyDisplayValue(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets both localized display values of a boolean property by using the current UI culture.
        /// </summary>
        public IDictionary<bool, string> GetBooleanPropertyDisplayValues(string propertyName)
        {
            return o.GetBooleanPropertyDisplayValues(propertyName, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets both localized display values of a boolean property in the specified culture.
        /// </summary>
        public IDictionary<bool, string> GetBooleanPropertyDisplayValues(string propertyName, CultureInfo culture)
        {
            var type = GetObjectType(o);
            ValidateBooleanProperty(type, propertyName);

            return new Dictionary<bool, string>
            {
                { true, GetBooleanDisplayValue(type, propertyName, true, culture) },
                { false, GetBooleanDisplayValue(type, propertyName, false, culture) }
            };
        }

        /// <summary>
        /// Gets the localized display value of a boolean property in the specified culture.
        /// </summary>
        public string GetBooleanPropertyDisplayValue(string propertyName, CultureInfo culture)
        {
            var type = GetObjectType(o);
            ValidateBooleanProperty(type, propertyName);

            var propertyValue = GetPropertyInfo(type, propertyName).GetValue(o);

            if (propertyValue == null)
            {
                return string.Empty;
            }

            return GetBooleanDisplayValue(type, propertyName, (bool)propertyValue, culture);
        }
    }

    private static string GetBooleanDisplayValue(Type type, string propertyName, bool booleanValue, CultureInfo culture)
    {
        var attributeDisplayValue = GetPropertyAttributes<BooleanValueDisplayAttribute>(type, propertyName)
            .Where(attribute => attribute.Value == booleanValue)
            .Select(attribute => GetResourceValue(attribute.ResourceType, attribute.ResourceName, culture))
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));

        if (!string.IsNullOrWhiteSpace(attributeDisplayValue))
        {
            return attributeDisplayValue;
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
}
