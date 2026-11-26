using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The complete content (all rows) of a custom table, including the current ETag.
/// </summary>
/// <example>
/// { "customTableId": "hinges", "rows": [ { "rowId": "r1", "cells": [ { "columnId": "width", "value": "35.5" } ] } ], "eTag": "\"0x8DC1\"" }
/// </example>
[DebuggerDisplay("CustomTableId={CustomTableId}, Rows={Rows.Count}, ETag={ETag}")]
public class CustomTableData
{
    /// <summary>
    /// Id of the custom table.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string CustomTableId { get; set; } = null!;

    /// <summary>
    /// All rows of the custom table.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableRow> Rows { get; set; } = [];

    /// <summary>
    /// ETag identifying the current version of the content. Pass it back via
    /// <see cref="SetCustomTableDataRequest.ETag" /> to apply a change only when the content has not been modified in the
    /// meantime.
    /// </summary>
    [JsonProperty(Order = 30)]
    public string? ETag { get; set; }
}
