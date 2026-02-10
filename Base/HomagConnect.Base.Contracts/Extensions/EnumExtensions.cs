using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;

using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.Base.Contracts.Extensions
{
    /// <summary>
    /// Helpers for localized names and descriptions of enum values via DisplayAttribute or ResourceManager.
    /// </summary>
    public static class EnumExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns a dictionary of enum value to localized display name for the given culture.
        /// </summary>
        public static IDictionary<T, string> GetDisplayNames<T>(CultureInfo culture) where T : Enum
        {
            var enumType = typeof(T);
            var dictionary = new Dictionary<T, string>();

            foreach (var kv in GetDisplayNames(enumType, culture))
            {
                dictionary.Add((T)kv.Key, kv.Value);
            }

            return dictionary;
        }

        /// <summary>
        /// Returns a dictionary of enum value to localized display name for the given culture.
        /// Order is not guaranteed.
        /// </summary>
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
                    dictionary.Add(enumValue, enumDisplayName.CapitalizeFirstLetter());
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Gets the display icon URI for the enum value using the DisplayIconAttribute, or null if not set.
        /// </summary>
        public static Dictionary<T, Uri?> GetDisplayIconUris<T>() where T : Enum
        {
            var enumType = typeof(T);
            var iconUris = new Dictionary<T, Uri?>();

            foreach (var enumValue in Enum.GetValues(enumType))
            {
                var field = enumType.GetField(enumValue.ToString(), BindingFlags.Public | BindingFlags.Static);
                if (field != null)
                {
                    var displayIconAttribute = field.GetCustomAttribute<DisplayIconAttribute>();
                    if (displayIconAttribute != null)
                    {
                        iconUris.Add((T)enumValue, displayIconAttribute.Icon);
                    }
                }
            }

            return iconUris;
        }

        extension<T>(T enumValue) where T : Enum
        {
            /// <summary>
            /// Gets the display icon URI for the enum value using the DisplayIconAttribute, or null if not set.
            /// </summary>
            public Uri? GetDisplayIconUri()
            {
                var enumType = typeof(T);
                var enumName = Enum.GetName(enumType, enumValue);
                if (string.IsNullOrWhiteSpace(enumName))
                {
                    return null;
                }
                var field = enumType.GetField(enumName, BindingFlags.Public | BindingFlags.Static);
                if (field != null)
                {
                    var displayIconAttribute = field.GetCustomAttribute<DisplayIconAttribute>();
                    if (displayIconAttribute != null)
                    {
                        return displayIconAttribute.Icon;
                    }
                }
                return null;
            }

            /// <summary>
            /// Gets a localized description for the enum value using DisplayAttribute (Description or Name),
            /// falling back to ResourceManager "{EnumName}.Description" or "{EnumName}", and finally the enum name.
            /// </summary>
            public string GetLocalizedDescription(CultureInfo culture)
            {
                var enumType = typeof(T);
                var enumName = Enum.GetName(enumType, enumValue);

                if (string.IsNullOrWhiteSpace(enumName))
                {
                    return enumValue.ToString();
                }

                // Try DisplayAttribute first (supports ResourceType for localization)
                var field = enumType.GetField(enumName, BindingFlags.Public | BindingFlags.Static);
                if (field != null)
                {
                    var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttribute != null)
                    {
                        // DisplayAttribute.GetDescription/GetName resolves via ResourceType when set
                        var description = displayAttribute.GetDescription();
                        if (!string.IsNullOrWhiteSpace(description))
                            return description.CapitalizeFirstLetter();

                        var name = displayAttribute.GetName();
                        if (!string.IsNullOrWhiteSpace(name))
                            return name.CapitalizeFirstLetter();
                    }
                }

                // Fallback to ResourceManager if the enum type has ResourceManagerAttribute
                var resourceManager = GetResourceManager(enumType);
                if (resourceManager != null)
                {
                    var resourceDescription = resourceManager.GetString($"{enumName}.Description", culture)
                                              ?? resourceManager.GetString(enumName, culture);
                    if (!string.IsNullOrWhiteSpace(resourceDescription))
                        return resourceDescription.CapitalizeFirstLetter();
                }

                // Final fallback: enum name
                return enumName.CapitalizeFirstLetter();
            }

            /// <summary>
            /// Gets a localized display name for the enum value using DisplayAttribute (Name),
            /// falling back to ResourceManager "{EnumName}", and finally the enum name.
            /// </summary>
            public string GetLocalizedName(CultureInfo culture)
            {
                var enumType = typeof(T);
                var enumName = Enum.GetName(enumType, enumValue);

                if (string.IsNullOrWhiteSpace(enumName))
                {
                    return enumValue.ToString();
                }

                // Try DisplayAttribute first (supports ResourceType for localization)
                var field = enumType.GetField(enumName, BindingFlags.Public | BindingFlags.Static);
                if (field != null)
                {
                    var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttribute != null)
                    {
                        var name = displayAttribute.GetName();
                        if (!string.IsNullOrWhiteSpace(name))
                            return name.CapitalizeFirstLetter();
                    }
                }

                // Fallback to ResourceManager if the enum type has ResourceManagerAttribute
                var resourceManager = GetResourceManager(enumType);
                if (resourceManager != null)
                {
                    var resourceName = resourceManager.GetString(enumName, culture);
                    if (!string.IsNullOrWhiteSpace(resourceName))
                        return resourceName.CapitalizeFirstLetter();
                }

                // Final fallback: enum name
                return enumName.CapitalizeFirstLetter();
            }
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

        private static ResourceManager? GetResourceManager(Type enumType)
        {
            var resourceManagerAttribute = enumType.GetCustomAttribute<ResourceManagerAttribute>();
            return resourceManagerAttribute != null ? new ResourceManager(resourceManagerAttribute.ResourceManagerType) : null;
        }

        #endregion
    }
}