using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The current state and progress of a custom table synchronization.
/// </summary>
[DebuggerDisplay("CorrelationId={CorrelationId}, State={State}, Progress={Progress}")]
public class CustomTableSyncStateData
{
    /// <summary>
    /// The point in time the synchronization was requested.
    /// </summary>
    [JsonProperty(Order = 10)]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// The current state of the synchronization.
    /// </summary>
    [JsonProperty(Order = 20)]
    public CustomTableSyncState State { get; set; }

    /// <summary>
    /// The id returned by the start request, which identifies the synchronization.
    /// </summary>
    /// <remarks>
    /// Can be an empty string if the synchronization could not be started.
    /// </remarks>
    [JsonProperty(Order = 30)]
    public string CorrelationId { get; set; } = string.Empty;

    /// <summary>
    /// The name of the library containing the synchronized custom table.
    /// </summary>
    [JsonProperty(Order = 40)]
    public string? LibraryId { get; set; }

    /// <summary>
    /// The id of the synchronized custom table.
    /// </summary>
    [JsonProperty(Order = 50)]
    public string? CustomTableId { get; set; }

    /// <summary>
    /// The materialManager data source the custom table is synchronized with.
    /// </summary>
    [JsonProperty(Order = 60)]
    public CustomTableSyncDataSource DataSource { get; set; }

    /// <summary>
    /// The progress of the synchronization in percent (0 to 100).
    /// </summary>
    [JsonProperty(Order = 70)]
    public double Progress { get; set; }

    /// <summary>
    /// A localized description of the current processing step.
    /// </summary>
    [JsonProperty(Order = 80)]
    public string ProgressInfo { get; set; } = string.Empty;

    /// <summary>
    /// The errors that occurred during the synchronization. Empty if no error occurred.
    /// </summary>
    [JsonProperty(Order = 90)]
    public IList<string> ErrorTexts { get; set; } = [];

    /// <summary>
    /// The warnings that occurred during the synchronization. Empty if no warning occurred.
    /// </summary>
    [JsonProperty(Order = 100)]
    public IList<string> WarningTexts { get; set; } = [];
}
