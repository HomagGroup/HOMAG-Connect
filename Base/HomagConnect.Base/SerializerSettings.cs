using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HomagConnect.Base
{
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
    }

    class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

            contract.DictionaryKeyResolver = propertyName => propertyName;

            return contract;
        }
    }
}