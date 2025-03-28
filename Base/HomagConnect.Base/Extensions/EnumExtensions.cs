using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.Base.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display names of all enum values in the specified culture.
        /// </summary>
        public static IDictionary<T, string> GetDisplayNames<T>(CultureInfo culture) where T : Enum
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType);
            var dictionary = new Dictionary<T, string>();
            var resourceManager = GetResourceManager(enumType);

            foreach (var enumValue in enumValues.OfType<T>())
            {
                var enumName = Enum.GetName(enumType, enumValue);
                var enumDisplayName = enumValue.ToString();

                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    if (enumValue.TryGetDisplayNameFromResourceManager(resourceManager, culture, out var resourceManagerDisplayName))
                    {
                        enumDisplayName = resourceManagerDisplayName;
                    }
                    else if (enumValue.TryGetDisplayNameFromDisplayAttribute(out var enumDisplayAttributeName))
                    {
                        enumDisplayName = enumDisplayAttributeName;
                    }
                    else
                    {
                        enumDisplayName = enumName;
                    }
                }

                dictionary.Add(enumValue, enumDisplayName ?? enumValue.ToString());
            }

            return dictionary;
        }

        private static bool TryGetDisplayNameFromDisplayAttribute<T>(this T enumValue, out string? enumDisplayName) where T : Enum
        {
            enumDisplayName = string.Empty;

            var enumName = Enum.GetName(typeof(T), enumValue);

            if (!string.IsNullOrWhiteSpace(enumName))
            {
                var field = typeof(T).GetField(enumName);

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

        private static bool TryGetDisplayNameFromResourceManager<T>(this T enumValue, ResourceManager? resourceManager, CultureInfo cultureInfo, out string? enumDisplayName) where T : Enum
        {
            enumDisplayName = string.Empty;

            if (resourceManager != null)
            {
                var enumName = Enum.GetName(typeof(T), enumValue);

                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    enumDisplayName = resourceManager.GetString(enumName, cultureInfo) ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(enumDisplayName))
                    {
                        enumDisplayName = CapitalizeFirstLetter(enumDisplayName);

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
    }
}