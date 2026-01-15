using System.Globalization;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Serializer Settings
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
    /// Default
    /// </summary>
    public static JsonSerializerSettings Default { get; }

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

    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {

            contract.DictionaryKeyResolver = propertyName => propertyName;

            return contract;
        }
    }
}
