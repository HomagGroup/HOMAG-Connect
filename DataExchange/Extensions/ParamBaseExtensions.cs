using System.Runtime.CompilerServices;
using System.Xml;

using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Param base extensions.
/// </summary>
public static class ParamBaseExtensions
{
    private const XmlDateTimeSerializationMode _DateTimeSerializationMode = XmlDateTimeSerializationMode.Unspecified;

    internal static string ToXmlValue<T>(this T? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        if (typeof(T) == typeof(string))
        {
            return value.ToString();
        }

        if ((typeof(T) == typeof(DateTime?)) || (typeof(T) == typeof(DateTime)))
        {
            return XmlConvert.ToString((DateTime)Convert.ChangeType(value, typeof(DateTime)), _DateTimeSerializationMode);
        }

        if ((typeof(T) == typeof(DateTimeOffset?)) || (typeof(T) == typeof(DateTimeOffset)))
        {
            return XmlConvert.ToString((DateTimeOffset)Convert.ChangeType(value, typeof(DateTimeOffset)));
        }

        throw new NotSupportedException("Type " + typeof(T) + " is not supported.");
    }

    internal static object? FromXmlValue(this string? value, Type type)
    {
        if (value == null)
        {
            return null;
        }

        if (type == typeof(string))
        {
            return Convert.ChangeType(value, type);
        }

        if (type == typeof(DateTime) || type == typeof(DateTime))
        {
            return Convert.ChangeType(XmlConvert.ToDateTime(value, _DateTimeSerializationMode), type);
        }

        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            return Convert.ChangeType(XmlConvert.ToDateTimeOffset(value), typeof(DateTimeOffset)); // TODO: Timezone of the subscription needs to be considered. Might become a project file property.
        }

        throw new NotSupportedException("Type " + type + " is not supported.");
    }

    /// <summary>
    /// Gets the property value.
    /// </summary>
    public static T? GetPropertyValue<T>(this Order order, [CallerMemberName] string name = "")
    {
        return (order as ParamBase).GetPropertyValue<T>(name);
    }

    /// <summary>
    /// Gets the property value.
    /// </summary>
    public static T? GetPropertyValue<T>(this ParamBase paramBase, [CallerMemberName] string name = "")
    {
        var property = paramBase.Properties.FirstOrDefault(x => x.Name == name);

        var value = property?.Value?.FromXmlValue(typeof(T));

        if (value == null)
        {
            return default;
        }

        return (T)value;
    }

    /// <summary>
    /// Sets the property value.
    /// </summary>
    public static void SetPropertyValue<T>(this ParamBase paramBase, T? value, [CallerMemberName] string name = "")
    {
        var valueAsString = string.Empty;

        if (value != null)
        {
            valueAsString = value.ToXmlValue();
        }

        var param = new Param { Name = name, Value = valueAsString };

        var existingParam = paramBase.Properties.FirstOrDefault(x => x.Name == name);

        if (existingParam != null)
        {
            existingParam.Value = valueAsString;
        }
        else
        {
            paramBase.Properties.Add(param);
        }
    }
}