using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// A single row of a custom table.
/// </summary>
/// <example>
/// { "rowId": "r1", "cells": [ { "columnId": "width", "value": "35.5" } ] }
/// </example>
[DebuggerDisplay("RowId={RowId}, Cells={Cells.Count}")]
public class CustomTableRow
{
    /// <summary>
    /// Id of the row. Optional. When set and known, the existing row keeps this id; when omitted, a new id
    /// is generated.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? RowId { get; set; }

    /// <summary>
    /// Cells of the row. Each cell addresses a column by its id. Missing input columns are stored as empty
    /// values.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableCell> Cells { get; set; } = [];
}
