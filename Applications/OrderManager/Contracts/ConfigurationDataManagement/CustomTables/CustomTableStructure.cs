using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The structure (column definitions) of a custom table, including the current ETag.
/// </summary>
/// <example>
/// { "customTableId": "hinges", "name": "Hinges", "columns": [ { "id": "width", "name": "Width", "type": "Number", "allowNull": false, "isInput": true } ], "allInputEntriesMustBeUnique": false, "eTag": "\"0x8DC1\"" }
/// </example>
[DebuggerDisplay("CustomTableId={CustomTableId}, Columns={Columns.Count}")]
public class CustomTableStructure
{
    /// <summary>
    /// Id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string CustomTableId { get; set; } = null!;

    /// <summary>
    /// Name of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? Name { get; set; }

    /// <summary>
    /// Column definitions of the custom table.
    /// </summary>
    [JsonProperty(Order = 30)]
    public Collection<CustomTableColumn> Columns { get; set; } = [];

    /// <summary>
    /// Value indicating whether all input entries must be unique. 
    /// If true, the system will enforce uniqueness of input values across all rows for the defined input columns.
    /// Default is false.
    /// </summary>
    [JsonProperty(Order = 40)]
    public bool? AllInputEntriesMustBeUnique { get; set; }

    /// <summary>
    /// ETag identifying the current version of the structure. Use it to detect structure changes.
    /// </summary>
    [JsonProperty(Order = 50)]
    public string? ETag { get; set; }
}
