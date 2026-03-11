using System.Globalization;

using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Provides shared JSON serializer settings for HOMAG Connect contracts.
/// </summary>
public static class SerializerSettings
{
    static SerializerSettings()
    {
        var settings = new JsonSerializerSettings
        {
#if DEBUG
            Formatting = Formatting.Indented,
#endif
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            ContractResolver = new CamelCaseExceptDictionaryKeysResolver()
        };
        settings.Converters.Add(new StringEnumConverter());
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Error;

        Default = settings;
    }

    /// <summary>
    /// Gets the default JSON serializer settings used for contract serialization.
    /// Uses camelCase property names, preserves dictionary keys, parses dates as <see cref="DateTimeOffset" />,
    /// ignores <c>null</c> values, and serializes enums as strings.
    /// </summary>
    /// <example>var json = JsonConvert.SerializeObject(contract, SerializerSettings.Default);</example>
    public static JsonSerializerSettings Default { get; }

    /// <summary>
    /// Creates JSON serializer settings that use localized property names for the specified culture.
    /// Includes <c>null</c> values and uses ISO 8601 date formatting.
    /// </summary>
    /// <param name="cultureInfo">The culture to use for localized property names.</param>
    /// <returns>A localized <see cref="JsonSerializerSettings" /> instance.</returns>
    public static JsonSerializerSettings Localized(CultureInfo cultureInfo)
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new LocalizedContractResolver(cultureInfo),
            Formatting = Formatting.Indented,

            // Keep ISO 8601 with offset, round-trip
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,

            // Include null values to see all properties
            NullValueHandling = NullValueHandling.Include
        };
    }

    private class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            var contract = base.CreateDictionaryContract(objectType);

            contract.DictionaryKeyResolver = propertyName => propertyName;

            return contract;
        }
    }
}