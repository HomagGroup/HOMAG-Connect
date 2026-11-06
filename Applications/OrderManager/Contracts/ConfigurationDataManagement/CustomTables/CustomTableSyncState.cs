using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The state of a custom table synchronization. A synchronization runs through <see cref="Queued" /> and
/// <see cref="Started" /> and ends with <see cref="Succeeded" />, <see cref="Error" /> or <see cref="Aborted" />.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum CustomTableSyncState
{
    /// <summary>
    /// The state is not known, e.g. because a newer server version returned a state this client does not know yet.
    /// </summary>
    Unknown,

    /// <summary>
    /// The synchronization was accepted and is waiting to be processed.
    /// </summary>
    Queued,

    /// <summary>
    /// The synchronization is running.
    /// </summary>
    Started,

    /// <summary>
    /// The synchronization finished successfully.
    /// </summary>
    Succeeded,

    /// <summary>
    /// The synchronization could not be started or failed. See the error text(s) for details.
    /// </summary>
    Error,

    /// <summary>
    /// The synchronization was aborted before it finished.
    /// </summary>
    Aborted
}
