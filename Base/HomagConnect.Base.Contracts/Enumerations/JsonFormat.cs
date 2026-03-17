using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Specifies the JSON output format used when serializing contracts.
/// </summary>
/// <example>Default</example>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum JsonFormat
{
    /// <summary>
    /// Uses the default contract serialization format.
    /// </summary>
    Default,

    /// <summary>
    /// Uses localized property names and formatting for direct usage in Excel / PowerBI.
    /// </summary>
    Localized
}