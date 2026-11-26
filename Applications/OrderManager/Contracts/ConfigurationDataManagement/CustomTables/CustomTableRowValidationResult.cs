using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The validation result of a single custom table row, carrying the per-cell validation results.
/// </summary>
/// <example>
/// { "rowId": "r1", "cells": [ { "columnId": "width", "value": "35.5", "isValid": true } ] }
/// </example>
[DebuggerDisplay("RowId={RowId}, Cells={Cells.Count}")]
public class CustomTableRowValidationResult
{
    /// <summary>
    /// Id of the row this validation result belongs to. May be null for rows without a known id.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? RowId { get; set; }

    /// <summary>
    /// Per-cell validation results of the row.
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<CustomTableCellValidationResult> Cells { get; set; } = [];
}
