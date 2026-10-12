using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes a clamp of a program fence.
/// </summary>
/// <example>
/// { "number": 1, "position": 120.5, "group": 1, "clampFingers": [ { "offset": 0, "width": 40 } ], "width": 40 }
/// </example>
public class Clamp
{
    /// <summary>
    /// Gets or sets the number of the clamp.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(Order = 1)]
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the distance of the middle of the clamp from the beginning of the program fence.
    /// </summary>
    /// <example>120.5</example>
    [JsonProperty(Order = 2)]
    public double Position { get; set; }

    /// <summary>
    /// Gets or sets the group to which the clamp belongs.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(Order = 3)]
    public int Group { get; set; }

    /// <summary>
    /// Gets or sets the fingers of the clamp.
    /// </summary>
    [JsonProperty(Order = 4)]
    public IList<ClampFinger> ClampFingers { get; set; } = new List<ClampFinger>();

    /// <summary>
    /// Gets or sets the width of the clamp (span of all clamp fingers).
    /// </summary>
    /// <example>40</example>
    [JsonProperty(Order = 5)]
    public double Width { get; set; }
}
