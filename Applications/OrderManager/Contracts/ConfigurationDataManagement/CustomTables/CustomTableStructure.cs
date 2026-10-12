using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The structure (column definitions) of a custom table, including the current ETag.
/// </summary>
[DebuggerDisplay("CustomTableId={CustomTableId}, Columns={Columns.Count}")]
public class CustomTableStructure
{
    /// <summary>
    /// Gets or sets the id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string CustomTableId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the column definitions of the custom table.
    /// </summary>
    [JsonProperty(Order = 30)]
    public Collection<CustomTableColumn> Columns { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether all input entries must be unique. 
    /// If true, the system will enforce uniqueness of input values across all rows for the defined input columns.
    /// Default is false.
    /// </summary>
    [JsonProperty(Order = 40)]
    public bool? AllInputEntriesMustBeUnique { get; set; }

    /// <summary>
    /// Gets or sets the ETag identifying the current version of the structure. Use it to detect structure changes.
    /// </summary>
    [JsonProperty(Order = 50)]
    public string? ETag { get; set; }
}
