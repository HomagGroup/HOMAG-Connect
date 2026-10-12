using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// A single cell of a custom table row, addressing a column by its id and carrying its value.
/// </summary>
[DebuggerDisplay("ColumnId={ColumnId}, Value={Value}")]
public class CustomTableCell
{
    /// <summary>
    /// Gets or sets the id of the column this cell belongs to.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string ColumnId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the cell. For attachment or model columns this is a reference to an existing image id.
    /// </summary>
    [JsonProperty(Order = 20)]
    public object? Value { get; set; }
}
