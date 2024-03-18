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
        public override bool CanConvert(Type objectType) => (this.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum;

        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            bool flag = this.IsNullableType(objectType);
            Type enumType = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
            string[] names = Enum.GetNames(enumType);
            if (reader.TokenType == JsonToken.String)
            {
                string enumText = reader.Value.ToString();
                if (!string.IsNullOrEmpty(enumText))
                {
                    string str = ((IEnumerable<string>)names).FirstOrDefault<string>((Func<string, bool>)(n => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase)));
                    if (str != null)
                        return Enum.Parse(enumType, str);
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                int int32 = Convert.ToInt32(reader.Value);
                if (((IEnumerable<int>)Enum.GetValues(enumType)).Contains<int>(int32))
                    return Enum.Parse(enumType, int32.ToString());
            }

            if (flag)
                return (object)null;
            string str1 = ((IEnumerable<string>)names).FirstOrDefault<string>((Func<string, bool>)(n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase))) ??
                          ((IEnumerable<string>)names).First<string>();
            return Enum.Parse(enumType, str1);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(value.ToString());

        private bool IsNullableType(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}