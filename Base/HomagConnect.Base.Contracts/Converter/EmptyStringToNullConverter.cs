using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Converter
{
    public class EmptyStringToNullConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            writer.WriteValue(string.IsNullOrEmpty(value) ? null : value);
        }
    }
}
