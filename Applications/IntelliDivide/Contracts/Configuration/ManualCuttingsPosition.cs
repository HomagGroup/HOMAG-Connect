using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Defines where manual cuts are preferably placed in the cutting sequence of a saw.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ManualCuttingsPosition
{
    /// <summary>
    /// Manual cuts are preferably placed at the beginning.
    /// </summary>
    PreferredAtBeginning,

    /// <summary>
    /// Manual cuts are preferably placed at the end.
    /// </summary>
    PreferredAtEnd
}
