using System;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Converts a <see cref="GrainMatchTemplateReference" /> to and from JSON.
/// </summary>
public class GrainMatchTemplateReferenceConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // we currently support only writing of JSON
        var s = (string)reader.Value;

        if (string.IsNullOrWhiteSpace(s))
        {
            return null;
        }

        return GrainMatchTemplateReference.FromString(s);
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        writer.WriteValue(value.ToString());
    }
}