using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// Request to replace the entire content of a custom table in a single call. All existing rows that are not part of the
/// request are removed.
/// </summary>
[DebuggerDisplay("Rows={Rows.Count}, ETag={ETag}")]
public class SetCustomTableDataRequest
{
    /// <summary>
    /// Gets or sets the ETag of the custom table content the change is based on, as returned by
    /// <see cref="CustomTableData.ETag" />. When set, the write is only applied if the current content still matches this
    /// ETag; otherwise the call fails and nothing is changed, so a concurrent change is not overwritten. When omitted, no
    /// concurrency check is performed.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? ETag { get; set; }

    /// <summary>
    /// Gets or sets the rows that become the complete new content of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableRow> Rows { get; set; } = [];
}
