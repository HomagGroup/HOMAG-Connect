using System.Globalization;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Converter
{
    /// <summary>
    /// We use this converter in order to extend in the future enum entries without breaking the deserialization of old
    /// assembly versions
    /// See also: https://stackoverflow.com/questions/22752075/how-can-i-ignore-unknown-enum-values-during-json-deserialization
    /// </summary>
    public class TolerantEnumConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            if (IsNullableType(objectType))
            {
                var underlyingType = Nullable.GetUnderlyingType(objectType);

                if (underlyingType != null)
                {
                    return underlyingType.IsEnum;
                }
            }
            else
            {
                return objectType.IsEnum;
            }

            return false;
        }

        /// <inheritdoc />
        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (reader.Value == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var flag = IsNullableType(objectType);
            var enumType = flag ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (enumType == null)
            {
                throw new NotSupportedException();
            }

            var names = Enum.GetNames(enumType);

            if (reader.TokenType == JsonToken.String)
            {
                var enumText = reader.Value.ToString();

                if (!string.IsNullOrEmpty(enumText))
                {
                    string str = names.FirstOrDefault(n => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        return Enum.Parse(enumType, str);
                    }
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                var int32 = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);

                var enumValues = Enum.GetValues(enumType).OfType<int>();

                if (enumValues == null)
                {
                    throw new NotSupportedException();
                }

                if (enumValues.Contains(int32))
                {
                    return Enum.Parse(enumType, int32.ToString());
                }
            }

            if (flag)
            {
                return null;
            }

            var str1 = names.FirstOrDefault(n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase)) ?? names[0];

            return Enum.Parse(enumType, str1);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => writer.WriteValue(value?.ToString());

        private static bool IsNullableType(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}