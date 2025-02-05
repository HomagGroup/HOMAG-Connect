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

        if (property?.Value == null)
        {
            return default;
        }

        if (typeof(T) == typeof(string))
        {
            return (T)Convert.ChangeType(property.Value, typeof(T));
        }

        if (typeof(T) == typeof(DateTime))
        {
            return (T)Convert.ChangeType(XmlConvert.ToDateTime(property.Value, _DateTimeSerializationMode), typeof(T));
        }

        throw new NotSupportedException("Type " + typeof(T) + " is not supported.");
    }

    /// <summary>
    /// Sets the property value.
    /// </summary>
    public static void SetPropertyValue<T>(this ParamBase paramBase, T? value, [CallerMemberName] string name = "")
    {
        var valueAsString = string.Empty;

        if (value != null)
        {
            if (typeof(T) == typeof(string))
            {
                valueAsString = value.ToString();
            }
            else if (typeof(T) == typeof(DateTime))
            {
                valueAsString = XmlConvert.ToString((DateTime)Convert.ChangeType(value, typeof(DateTime)), _DateTimeSerializationMode);
            }
            else if (typeof(T) == typeof(DateTime?))
            {
                valueAsString = XmlConvert.ToString((DateTime)Convert.ChangeType(value, typeof(DateTime)), _DateTimeSerializationMode);
            }
            else
            {
                throw new NotSupportedException("Type " + typeof(T) + " is not supported.");
            }
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