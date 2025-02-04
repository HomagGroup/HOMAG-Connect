using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange param definition.
    /// </summary>
     [DebuggerDisplay("{Name}: {Value}")]
    public class Param
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlAttribute("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or set the value.
        /// </summary>
        [XmlAttribute("value")]
        public string? Value { get; set; }

        /// <summary>
        /// Sets the value to the given object as xml string
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            switch (value)
            {
                case string x:
                    Value = x;
                    break;
                case Guid x:
                    Value = XmlConvert.ToString(x);
                    break;
                case DateTimeOffset x:
                    Value = XmlConvert.ToString(x);
                    break;
                case DateTime x:
                    Value = XmlConvert.ToString(x, XmlDateTimeSerializationMode.Unspecified);
                    break;
                case byte x:
                    Value = XmlConvert.ToString(x);
                    break;
                case sbyte x:
                    Value = XmlConvert.ToString(x);
                    break;
                case short x:
                    Value = XmlConvert.ToString(x);
                    break;
                case ushort x:
                    Value = XmlConvert.ToString(x);
                    break;
                case int x:
                    Value = XmlConvert.ToString(x);
                    break;
                case uint x:
                    Value = XmlConvert.ToString(x);
                    break;
                case long x:
                    Value = XmlConvert.ToString(x);
                    break;
                case ulong x:
                    Value = XmlConvert.ToString(x);
                    break;
                case float x:
                    Value = XmlConvert.ToString(x);
                    break;
                case double x:
                    Value = XmlConvert.ToString(x);
                    break;
                case decimal x:
                    Value = XmlConvert.ToString(x);
                    break;
                case bool x:
                    Value = XmlConvert.ToString(x);
                    break;
                case char x:
                    Value = XmlConvert.ToString(x);
                    break;
                case TimeSpan x:
                    Value = XmlConvert.ToString(x);
                    break;
                case null:
                    Value = null;
                    break;
                default:
                    throw new InvalidOperationException(
                        $"Unsupported data type '{value.GetType().FullName}' with value: {value}");
            }
        }

    }
}
