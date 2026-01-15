using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomagConnect.Base.Contracts.Converter
{
    /// <summary>
    /// We use this converter in order to extend in the future enum entries without breaking the deserialization of old
    /// assembly versions
    /// See also: https://stackoverflow.com/questions/22752075/how-can-i-ignore-unknown-enum-values-during-json-deserialization
    /// </summary>
    public class TolerantEnumConverter : StringEnumConverter
    {

        /// <inheritdoc />
        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                // We use the fallback value if the enum value is unknown
            }

            var isNullable = IsNullableType(objectType);
            var enumType = (isNullable ? Nullable.GetUnderlyingType(objectType) : objectType) ?? throw new NotSupportedException();
            var names = Enum.GetNames(enumType);
            var enumName = Array.Find(names, (n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase))) ?? names[0];
            return Enum.Parse(enumType, enumName);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => writer.WriteValue(value?.ToString());

        private static bool IsNullableType(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}