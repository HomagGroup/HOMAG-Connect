using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomagConnect.Base.Contracts.Converter;

/// <summary>
/// A Json.NET contract resolver that localizes property names using
/// DisplayAttribute and ensures enums are serialized using a display-name based converter.
/// Usage:
/// var settings = new JsonSerializerSettings
/// {
/// ContractResolver = new LocalizedDisplayNameContractResolver(CultureInfo.GetCultureInfo("de"))
/// };
/// JsonConvert.SerializeObject(obj, settings);
/// </summary>
public sealed class LocalizedContractResolver : DefaultContractResolver
{
    private readonly CultureInfo _Culture;

    /// <summary>
    /// Initializes a new instance with the desired culture for display name lookup.
    /// </summary>
    /// <param name="culture">The culture used for localization; falls back to CurrentUICulture when null.</param>
    public LocalizedContractResolver(CultureInfo? culture)
    {
        _Culture = culture ?? CultureInfo.CurrentUICulture;
        // Keep original JSON property naming unless a DisplayAttribute provides a localized name
        NamingStrategy = new DefaultNamingStrategy();
    }

    /// <summary>
    /// Creates a JsonProperty and applies localization rules for property names and enum converters.
    /// </summary>
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        // Localize the property name via [Display(ResourceType=..., Name=...)]
        var displayAttr = member.GetCustomAttribute<DisplayAttribute>();
        if (displayAttr != null)
        {
            var localized = TryGetDisplayName(displayAttr, _Culture);
            if (!string.IsNullOrWhiteSpace(localized))
            {
                prop.PropertyName = localized!;
            }
        }

        // Ensure enums use DisplayEnumConverter (overrides any [JsonConverter] on the enum type)
        if (prop.PropertyType != null)
        {
            var concreteType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            if (concreteType.IsEnum)
            {
                prop.Converter = new LocalizedEnumConverter(_Culture);
            }
        }

        return prop;
    }

    /// <summary>
    /// Gets the ResourceManager from a resx-generated class, which exposes
    /// a public static ResourceManager property.
    /// </summary>
    private static ResourceManager? GetResourceManager(Type resourceType)
    {
        var prop = resourceType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
        return prop?.GetValue(null) as ResourceManager;
    }

    /// <summary>
    /// Resolves DisplayAttribute to a localized string using its ResourceType and Name.
    /// Falls back to DisplayAttribute.GetName() if resource lookup fails.
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

        // Fallback to attribute’s own name (uses CurrentUICulture internally)
        return display.GetName();
    }
}