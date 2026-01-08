using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Converter;

/// <summary>
/// Json.NET converter that writes enum values as localized display names.
/// Resolution order:
/// 1) DisplayAttribute on enum member (ResourceType + Name)
/// 2) ResourceManager discovered on enum type via custom attribute (e.g., ResourceManagerAttribute)
/// 3) Fallback to raw enum name
/// Deserialization is intentionally not implemented.
/// </summary>
public sealed class LocalizedEnumConverter : JsonConverter
{
    private readonly CultureInfo _Culture;

    /// <summary>
    /// Create a converter using the provided culture for resource lookups.
    /// </summary>
    public LocalizedEnumConverter(CultureInfo? culture)
    {
        _Culture = culture ?? CultureInfo.CurrentUICulture;
    }

    public override bool CanConvert(Type objectType)
    {
        var t = Nullable.GetUnderlyingType(objectType) ?? objectType;
        return t.IsEnum;
    }

    /// <summary>
    /// Deserialization from localized strings to enum is not supported.
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Deserialization from localized enum strings is not implemented.");
    }

    /// <summary>
    /// Writes the localized string for the enum value using the resolution order described above.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var enumType = value.GetType();
        var enumName = Enum.GetName(enumType, value) ?? value.ToString();

        // Prefer [Display] on the enum member
        var field = enumType.GetField(enumName!);
        var displayAttr = field?.GetCustomAttribute<DisplayAttribute>();
        if (displayAttr != null)
        {
            var localized = TryGetDisplayName(displayAttr, _Culture);
            if (!string.IsNullOrWhiteSpace(localized))
            {
                writer.WriteValue(localized);
                return;
            }
        }

        // Else try ResourceManager attached to the enum type
        var rm = TryGetResourceManagerFromEnumType(enumType);
        if (rm != null)
        {
            var s = rm.GetString(enumName!, _Culture);
            if (!string.IsNullOrEmpty(s))
            {
                writer.WriteValue(s);
                return;
            }
        }

        // Fallback to the original enum name
        writer.WriteValue(enumName);
    }

    /// <summary>
    /// Gets the ResourceManager from a resx-generated class exposing a public static ResourceManager property.
    /// </summary>
    private static ResourceManager? GetResourceManager(Type resourceType)
        => resourceType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as ResourceManager;

    /// <summary>
    /// Attempts to resolve DisplayAttribute to a localized string via its ResourceType and Name.
    /// Falls back to DisplayAttribute.GetName().
    /// </summary>
    private static string? TryGetDisplayName(DisplayAttribute display, CultureInfo culture)
    {
        if (display.ResourceType != null && !string.IsNullOrEmpty(display.Name))
        {
            var rm = GetResourceManager(display.ResourceType);
            if (rm != null)
            {
                var s = rm.GetString(display.Name!, culture);
                if (!string.IsNullOrEmpty(s))
                {
                    return s;
                }
            }
        }

        return display.GetName();
    }

    /// <summary>
    /// Finds a custom attribute on the enum type that likely contains a ResourceType (e.g., ResourceManagerAttribute),
    /// extracts the resource class Type and returns its ResourceManager.
    /// No hard dependency on a specific attribute type.
    /// </summary>
    private static ResourceManager? TryGetResourceManagerFromEnumType(Type enumType)
    {
        var attr = enumType.GetCustomAttributes(true)
            .FirstOrDefault(a => a.GetType().Name.IndexOf("ResourceManager", StringComparison.Ordinal) >= 0);
        if (attr == null) return null;

        var typeProp = attr.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(p => p.PropertyType == typeof(Type));

        var resourceClassType = typeProp?.GetValue(attr) as Type;
        return resourceClassType != null ? GetResourceManager(resourceClassType) : null;
    }
}