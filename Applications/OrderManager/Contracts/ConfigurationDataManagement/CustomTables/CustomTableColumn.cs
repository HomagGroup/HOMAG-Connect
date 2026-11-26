using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// A column definition of a custom table.
/// </summary>
/// <example>
/// { "id": "width", "name": "Width", "type": "Number", "allowNull": false, "isInput": true }
/// </example>
[DebuggerDisplay("Id={Id}, Name={Name}, Type={Type}")]
public class CustomTableColumn
{
    /// <summary>
    /// Id of the column. Cells reference their column by this id.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Name of the column.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Data type of the column.
    /// </summary>
    [JsonProperty(Order = 30)]
    public CustomTableColumnType Type { get; set; }

    /// <summary>
    /// Value indicating whether the column allows null values.
    /// </summary>
    [JsonProperty(Order = 40)]
    public bool? AllowNull { get; set; }

    /// <summary>
    /// Value indicating whether the column is an input column. Non-input columns are calculated and cannot be
    /// written.
    /// </summary>
    [JsonProperty(Order = 50)]
    public bool IsInput { get; set; }
}
