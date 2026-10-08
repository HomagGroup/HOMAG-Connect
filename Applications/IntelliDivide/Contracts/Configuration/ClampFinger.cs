using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes a finger of a clamp.
/// </summary>
/// <example>
/// { "offset": 0, "width": 40 }
/// </example>
public class ClampFinger
{
    /// <summary>
    /// Gets or sets the offset of the finger relative to the position of the clamp.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 1)]
    public double Offset { get; set; }

    /// <summary>
    /// Gets or sets the width of the finger.
    /// </summary>
    /// <example>40</example>
    [JsonProperty(Order = 2)]
    public double Width { get; set; }
}
