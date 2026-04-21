using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Specifies the output format used when serializing contracts.
/// </summary>
/// <example>Default</example>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum OutputFormat
{
    /// <summary>
    /// Uses the default contract serialization format.
    /// </summary>
    Default,

    /// <summary>
    /// Uses localized property names and formatting for direct usage in Excel / PowerBI.
    /// </summary>
    Localized,

    /// <summary>
    /// Uses a CSV format with localized property names and formatting for direct usage in Excel / PowerBI.
    /// </summary>
    CSV
}