using System.Collections.ObjectModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The result of a <see cref="SetCustomTableDataRequest" />. When <see cref="Succeeded" /> is <c>false</c>, nothing was
/// stored and <see cref="InvalidRows" /> contains the rows that failed validation.
/// </summary>
[DebuggerDisplay("Succeeded={Succeeded}, InvalidRows={InvalidRows.Count}")]
public class SetCustomTableDataResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the content was replaced successfully. If <c>false</c>, no change was
    /// persisted.
    /// </summary>
    [JsonProperty(Order = 10)]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets or sets an optional message describing why the call did not succeed, e.g. the edit lock is held by another user
    /// or the data source is not supported.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the rows that failed validation together with their per-cell validation results. Empty on success.
    /// </summary>
    [JsonProperty(Order = 30)]
    public Collection<CustomTableRowValidationResult> InvalidRows { get; set; } = [];
}
