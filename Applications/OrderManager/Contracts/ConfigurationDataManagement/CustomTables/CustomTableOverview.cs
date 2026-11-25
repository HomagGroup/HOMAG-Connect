using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// Overview of a custom table within a library.
/// </summary>
/// <example>
/// { "id": "hinges", "name": "Hinges", "description": "Hinge dimensions" }
/// </example>
[DebuggerDisplay("Id={Id}, Name={Name}")]
public class CustomTableOverview
{
    /// <summary>
    /// Id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Name of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Optional description of the custom table.
    /// </summary>
    [JsonProperty(Order = 30)]
    public string? Description { get; set; }
}
