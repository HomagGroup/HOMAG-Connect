using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The complete content (all rows) of a custom table, including the current ETag.
/// </summary>
[DebuggerDisplay("CustomTableId={CustomTableId}, Rows={Rows.Count}, ETag={ETag}")]
public class CustomTableData
{
    /// <summary>
    /// Gets or sets the id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string CustomTableId { get; set; } = null!;

    /// <summary>
    /// Gets or sets all rows of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableRow> Rows { get; set; } = [];

    /// <summary>
    /// Gets or sets the ETag identifying the current version of the content. Pass it back via
    /// <see cref="SetCustomTableDataRequest.ETag" /> to apply a change only when the content has not been modified in the
    /// meantime.
    /// </summary>
    [JsonProperty(Order = 30)]
    public string? ETag { get; set; }
}
