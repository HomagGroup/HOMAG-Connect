using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// Overview of a custom table within a library.
/// </summary>
[DebuggerDisplay("Id={Id}, Name={Name}")]
public class CustomTableOverview
{
    /// <summary>
    /// Gets or sets the id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional description of the custom table.
    /// </summary>
    [JsonProperty(Order = 30)]
    public string? Description { get; set; }
}
