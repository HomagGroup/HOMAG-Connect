using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The validation result of a single custom table cell.
/// </summary>
/// <example>
/// { "columnId": "width", "value": "abc", "isValid": false, "message": "Value must be a number." }
/// </example>
[DebuggerDisplay("ColumnId={ColumnId}, IsValid={IsValid}")]
public class CustomTableCellValidationResult
{
    /// <summary>
    /// Id of the column this validation result belongs to.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string ColumnId { get; set; } = null!;

    /// <summary>
    /// Value that was validated.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? Value { get; set; }

    /// <summary>
    /// Value indicating whether the cell value is valid.
    /// </summary>
    [JsonProperty(Order = 30)]
    public bool IsValid { get; set; }

    /// <summary>
    /// Validation message when the cell value is invalid.
    /// </summary>
    [JsonProperty(Order = 40)]
    public string? Message { get; set; }
}
