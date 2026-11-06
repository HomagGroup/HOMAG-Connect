using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The result of starting a custom table synchronization. Use <see cref="CorrelationId" /> to query the progress of the
/// synchronization.
/// </summary>
[DebuggerDisplay("State={State}, CorrelationId={CorrelationId}")]
public class CustomTableSyncResult
{
    /// <summary>
    /// The state of the synchronization after the start request, typically <see cref="CustomTableSyncState.Queued" />.
    /// <see cref="CustomTableSyncState.Error" /> means the synchronization could not be started, e.g. because the custom
    /// table is not linked to materialManager; see <see cref="ErrorText" />.
    /// </summary>
    [JsonProperty(Order = 10)]
    public CustomTableSyncState State { get; set; }

    /// <summary>
    /// The id to query the state of the synchronization with. <c>null</c> if the synchronization could not be started.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? CorrelationId { get; set; }

    /// <summary>
    /// The reason why the synchronization could not be started. Only set when <see cref="State" /> is
    /// <see cref="CustomTableSyncState.Error" />.
    /// </summary>
    [JsonProperty(Order = 30)]
    public string? ErrorText { get; set; }
}
