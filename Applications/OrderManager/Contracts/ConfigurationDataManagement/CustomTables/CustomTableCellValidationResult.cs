using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The validation result of a single custom table cell.
/// </summary>
[DebuggerDisplay("ColumnId={ColumnId}, IsValid={IsValid}")]
public class CustomTableCellValidationResult
{
    /// <summary>
    /// Gets or sets the id of the column this validation result belongs to.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string ColumnId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value that was validated.
    /// </summary>
    [JsonProperty(Order = 20)]
    public object? Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cell value is valid.
    /// </summary>
    [JsonProperty(Order = 30)]
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the validation message when the cell value is invalid.
    /// </summary>
    [JsonProperty(Order = 40)]
    public string? Message { get; set; }
}
