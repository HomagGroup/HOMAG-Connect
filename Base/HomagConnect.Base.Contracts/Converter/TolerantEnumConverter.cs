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

                foreach (var value in ((IEnumerable<int>)Enum.GetValues(enumType)))
                {
                    if (Equals(value, int32))
                        return Enum.Parse(enumType, int32.ToString());
                }
            }

            if (flag)
                return (object)null;
            string first = names.Where((Func<string, bool>)(n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase))).FirstOrDefault();

            string str1 = first ??
                          ((IEnumerable<string>)names).First<string>();
            return Enum.Parse(enumType, str1);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(value.ToString());

        private bool IsNullableType(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}