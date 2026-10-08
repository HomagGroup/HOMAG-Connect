using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes a program fence of a saw.
/// </summary>
/// <example>
/// { "number": 1, "clamps": [ { "number": 1, "position": 120.5, "group": 1, "clampFingers": [ { "offset": 0, "width": 40 } ], "width": 40 } ] }
/// </example>
public class ProgramFence
{
    /// <summary>
    /// Gets or sets the number of the program fence. If there are more than two fences, number 1 is the power concept fence.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(Order = 1)]
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the clamps of the program fence.
    /// </summary>
    [JsonProperty(Order = 2)]
    public IList<Clamp> Clamps { get; set; } = new List<Clamp>();
}
