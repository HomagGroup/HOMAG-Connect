using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement;

/// <summary>
/// Overview of a configuration data management library.
/// </summary>
[DebuggerDisplay("Name={Name}")]
public class LibraryOverview
{
    /// <summary>
    /// Gets or sets the name of the library. The name is the unique identifier of the library (there is no separate id).
    /// </summary>
    [JsonProperty(Order = 10)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional description of the library.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? Description { get; set; }
}
