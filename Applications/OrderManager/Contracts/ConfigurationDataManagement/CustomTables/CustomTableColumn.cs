using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// A column definition of a custom table.
/// </summary>
[DebuggerDisplay("Id={Id}, Name={Name}, Type={Type}")]
public class CustomTableColumn
{
    /// <summary>
    /// Gets or sets the id of the column. Cells reference their column by this id.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    [JsonProperty(Order = 30)]
    public CustomTableColumnType Type { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column allows null values.
    /// </summary>
    [JsonProperty(Order = 40)]
    public bool? AllowNull { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is an input column. Non-input columns are calculated and cannot be
    /// written.
    /// </summary>
    [JsonProperty(Order = 50)]
    public bool IsInput { get; set; }
}
