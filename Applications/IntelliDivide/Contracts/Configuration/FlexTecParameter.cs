using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the additional parameters of a FlexTec (HPS) saw.
/// </summary>
/// <example>
/// {
///   "activeBufferLength": 3000,
///   "activeBufferMinimalStripLength": 400,
///   "activeBufferDistanceBetweenStrips": 50,
///   "passiveBufferLength": 2500,
///   "passiveBufferWidth": 1200,
///   "passiveBufferDistanceBetweenParts": 30,
///   "passiveBufferOverhang": 100,
///   "passiveBufferDistanceToEdge": 20
/// }
/// </example>
public class FlexTecParameter
{
    /// <summary>
    /// Gets or sets the length of the active buffer.
    /// </summary>
    /// <example>3000</example>
    [JsonProperty(Order = 1)]
    public double ActiveBufferLength { get; set; }

    /// <summary>
    /// Gets or sets the minimal length of a strip for the active buffer.
    /// </summary>
    /// <example>400</example>
    [JsonProperty(Order = 2)]
    public double ActiveBufferMinimalStripLength { get; set; }

    /// <summary>
    /// Gets or sets the minimal distance between strips in the active buffer.
    /// </summary>
    /// <example>50</example>
    [JsonProperty(Order = 3)]
    public double ActiveBufferDistanceBetweenStrips { get; set; }

    /// <summary>
    /// Gets or sets the length of the passive buffer.
    /// </summary>
    /// <example>2500</example>
    [JsonProperty(Order = 4)]
    public double PassiveBufferLength { get; set; }

    /// <summary>
    /// Gets or sets the width of the passive buffer.
    /// </summary>
    /// <example>1200</example>
    [JsonProperty(Order = 5)]
    public double PassiveBufferWidth { get; set; }

    /// <summary>
    /// Gets or sets the minimal distance between parts in the passive buffer.
    /// </summary>
    /// <example>30</example>
    [JsonProperty(Order = 6)]
    public double PassiveBufferDistanceBetweenParts { get; set; }

    /// <summary>
    /// Gets or sets the maximal overhang for parts longer than the width of the passive buffer.
    /// </summary>
    /// <example>100</example>
    [JsonProperty(Order = 7)]
    public double PassiveBufferOverhang { get; set; }

    /// <summary>
    /// Gets or sets the minimal distance between a part and the edge of the passive buffer.
    /// </summary>
    /// <example>20</example>
    [JsonProperty(Order = 8)]
    public double PassiveBufferDistanceToEdge { get; set; }

    /// <summary>
    /// Gets or sets the parameters, keyed by their original (German) intelliDivide names.
    /// </summary>
    [JsonProperty(Order = 9)]
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets or sets the configurations of the MSM components (e.g. stacking stations, buffers).
    /// </summary>
    [JsonProperty(Order = 10)]
    public IList<MsmComponentConfiguration> MsmComponentConfigurations { get; set; } = new List<MsmComponentConfiguration>();
}
