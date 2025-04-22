using System.Runtime.CompilerServices;
using System.Xml;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Param base extensions.
/// </summary>
public static class ParamBaseExtensions
{
    private const XmlDateTimeSerializationMode _DateTimeSerializationMode = XmlDateTimeSerializationMode.Unspecified;

    private static string ToXmlValue<T>(this T? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        if (typeof(T) == typeof(string))
        {
            return value.ToString();
        }

        if (typeof(T) == typeof(Uri))
        {
            var uri = value as Uri;

            if (uri != null)
            {
                if (uri.IsAbsoluteUri)
                {
                    return uri.AbsoluteUri;
                }

                return uri.OriginalString;
            }

            return string.Empty;
        }

        if (typeof(T) == typeof(DateTime?) || (typeof(T) == typeof(DateTime)))
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

        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (type == typeof(string))
        {
            return Convert.ChangeType(value, type);
        }
        
        if (type == typeof(Uri))
        {
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }

        if (type == typeof(DateTime) || type == typeof(DateTime?))
        {
            return Convert.ChangeType(XmlConvert.ToDateTime(value, _DateTimeSerializationMode), type);
        }

        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            return Convert.ChangeType(XmlConvert.ToDateTimeOffset(value), typeof(DateTimeOffset)); // TODO: Timezone of the subscription needs to be considered. Might become a project file property.
        }

        if (type == typeof(Guid) || type == typeof(Guid?))
        {
            return Convert.ChangeType(XmlConvert.ToGuid(value), typeof(Guid));
        }

        if (type == typeof(int) || type == typeof(int?))
        {
            try
            {
                return Convert.ChangeType(XmlConvert.ToInt32(value), typeof(int));
            }
            catch (FormatException)
            {
                // For backward compability, be more tolerant when converting and and try to obtain the int value from a double value
                var val = XmlConvert.ToDouble(value);
                return Convert.ChangeType(Convert.ToInt32(val), typeof(int));
            }
        }

        if (type == typeof(double) || type == typeof(double?))
        {
            return Convert.ChangeType(XmlConvert.ToDouble(value), typeof(double));
        }

        if (type == typeof(bool) || type == typeof(bool?))
        {
            return Convert.ChangeType(XmlConvert.ToBoolean(value), typeof(bool));
        }

        if (type == typeof(Grain) || type == typeof(Grain?))
        {
            if (value is "" or "0" or "NoGrain" or "None")
            {
                return Grain.None;
            }

            if (value is "1" or "Lengthwise")
            {
                return Grain.Lengthwise;
            }

            if (value is "2" or "Crosswise")
            {
                return Grain.Crosswise;
            }

            throw new NotSupportedException("Value '" + value + "' for Grain is not supported.");
        }

        if (type == typeof(OrderManager.Contracts.OrderItems.Type) || type == typeof(OrderManager.Contracts.OrderItems.Type?))
        {
            if (value.Equals("OrderItem", StringComparison.InvariantCultureIgnoreCase))
            {
                return OrderManager.Contracts.OrderItems.Type.Position;
            }

            if (value.Equals("ProductionOrder", StringComparison.InvariantCultureIgnoreCase))
            {
                return OrderManager.Contracts.OrderItems.Type.Part;
            }

            if (Enum.TryParse(value, true, out OrderManager.Contracts.OrderItems.Type resultType)) 
            {
                return resultType;
            }

            return Enum.ToObject(typeof(OrderManager.Contracts.OrderItems.Type), value);
        }

        throw new NotSupportedException("Type " + type + ", Value, "+value +" is not supported.");
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