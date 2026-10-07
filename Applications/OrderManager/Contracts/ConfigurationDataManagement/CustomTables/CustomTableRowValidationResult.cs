using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The validation result of a single custom table row, carrying the per-cell validation results.
/// </summary>
[DebuggerDisplay("RowId={RowId}, Cells={Cells.Count}")]
public class CustomTableRowValidationResult
{
    /// <summary>
    /// Gets or sets the id of the row this validation result belongs to. May be null for rows without a known id.
    /// </summary>
    [JsonProperty(Order = 10)]
    public Guid? RowId { get; set; }

    /// <summary>
    /// Gets or sets the per-cell validation results of the row.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableCellValidationResult> Cells { get; set; } = [];
}
