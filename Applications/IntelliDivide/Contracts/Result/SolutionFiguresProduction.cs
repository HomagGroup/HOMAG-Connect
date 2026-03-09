#nullable enable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the key figures for the production of a solution.
/// </summary>
/// <example>
/// {
///   "output": {
///     "quantityOfParts": 112,
///     "productionTime": "02:15:16"
///   },
///   "handling": {
///     "recuts": 25,
///     "averageBookHeight": 26.56
///   }
/// }
/// </example>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionFiguresProduction
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    /// <example>
    /// { "customProperty": "custom value" }
    /// </example>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets the production key figures for handling.
    /// </summary>
    /// <example>
    /// { "recuts": 25, "averageBookHeight": 26.56 }
    /// </example>
    [JsonProperty(Order = 20)]
    public SolutionFiguresProductionHandling Handling { get; set; } = new();

    /// <summary>
    /// Gets the production key figures for output.
    /// </summary>
    /// <example>
    /// { "quantityOfParts": 112, "productionTime": "02:15:16" }
    /// </example>
    [JsonProperty(Order = 10)]
    public SolutionFiguresProductionOutput Output { get; set; } = new();
}