using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;

using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.Base.Extensions
{
    public static class EnumExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the display names of all enum values in the specified culture.
        /// </summary>
        public static IDictionary<T, string> GetDisplayNames<T>(CultureInfo culture) where T : Enum
        {
            var enumType = typeof(T);
            var dictionary = new Dictionary<T, string>();

            foreach (var keyValuePair in GetDisplayNames(enumType, culture))
            {
                dictionary.Add((T)keyValuePair.Key, keyValuePair.Value);
            }

            return dictionary;
        }

        public static Dictionary<object, string> GetDisplayNames(Type enumType, CultureInfo culture)
        {
            var enumValues = Enum.GetValues(enumType);
            var dictionary = new Dictionary<object, string>();
            var resourceManager = GetResourceManager(enumType);

            foreach (var enumValue in enumValues)
            {
                var enumName = Enum.GetName(enumType, enumValue);
                var enumDisplayName = enumValue.ToString();

                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    if (TryGetDisplayNameFromResourceManager(enumValue, enumType, resourceManager, culture, out var resourceManagerDisplayName))
                    {
                        enumDisplayName = resourceManagerDisplayName;
                    }
                    else if (TryGetDisplayNameFromDisplayAttribute(enumValue, enumType, out var enumDisplayAttributeName))
                    {
                        enumDisplayName = enumDisplayAttributeName;
                    }
                    else
                    {
                        enumDisplayName = enumName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(enumDisplayName))
                {
                    dictionary.Add(enumValue, CapitalizeFirstLetter(enumDisplayName));
                }
            }

            return dictionary;
        }

        #endregion

        #region Private Methods

        private static bool TryGetDisplayNameFromResourceManager(object enumValue, Type type, ResourceManager? resourceManager, CultureInfo cultureInfo, out string? enumDisplayName)
        {
            enumDisplayName = string.Empty;

            if (resourceManager != null)
            {
                var enumName = Enum.GetName(type, enumValue);

                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    enumDisplayName = resourceManager.GetString(enumName, cultureInfo) ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(enumDisplayName))
                    {
                        return true;
                    }
                }

                if (!string.IsNullOrWhiteSpace(enumDisplayName))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryGetDisplayNameFromDisplayAttribute(object enumValue, Type type, out string? enumDisplayName)
        {
            enumDisplayName = string.Empty;

            var enumName = Enum.GetName(type, enumValue);

            if (!string.IsNullOrWhiteSpace(enumName))
            {
                var field = type.GetField(enumName);

                if (field != null)
                {
                    var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

                    if (displayAttribute != null)
                    {
                        enumDisplayName = displayAttribute.GetDescription();

                        if (!string.IsNullOrWhiteSpace(enumDisplayName))
                        {
                            return true;
                        }

                        enumDisplayName = displayAttribute.GetName();

                        if (!string.IsNullOrWhiteSpace(enumDisplayName))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var stringArray = input.ToCharArray();

            if (char.IsLower(stringArray[0]))
            {
                stringArray[0] = char.ToUpper(stringArray[0]);
            }

            return new string(stringArray);
        }

        private static ResourceManager? GetResourceManager(Type enumType)
        {
            var resourceManagerAttribute = enumType.GetCustomAttribute<ResourceManagerAttribute>();

            return resourceManagerAttribute != null ? new ResourceManager(resourceManagerAttribute.ResourceManagerType) : null;
        }

        #endregion
    }
}