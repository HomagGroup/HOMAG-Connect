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
    /// <remarks>
    /// This can be of type string, number, boolean or null or even an JSON object as string.
    /// If it is a number, the formatting is always a dot as decimal separator, regardless of the current culture.
    /// The actual type is determined by the column definition.
    /// </remarks>
    [JsonProperty(Order = 20)]
    public string? Value { get; set; }
}
