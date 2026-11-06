using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement;

/// <summary>
/// Client for the configuration data management (cDM) functions of the OrderManager, giving read and write access to the
/// custom tables of a library and starting their synchronization with materialManager.
/// </summary>
/// <remarks>
/// All methods throw a <see cref="HomagConnect.Base.Contracts.Exceptions.ProblemDetailsException" /> when the request is
/// rejected, for example because the library or custom table does not exist.
/// </remarks>
public interface IConfigurationDataManagementClient
{
    /// <summary>
    /// Gets the list of available configuration data management libraries.
    /// </summary>
    /// <returns>The available libraries; an empty list if there are none.</returns>
    Task<IEnumerable<LibraryOverview>> GetLibraries();

    /// <summary>
    /// Gets the list of custom tables of the specified library.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <returns>The custom tables of the library; an empty list if the library has none.</returns>
    Task<IEnumerable<CustomTableOverview>> GetCustomTables(string libraryId);

    /// <summary>
    /// Reads the structure (column definitions) of a custom table, including its ETag.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    /// <returns>The column definitions and ETag of the custom table, or <c>null</c> if the response contains no data.</returns>
    Task<CustomTableStructure?> GetCustomTableStructure(string libraryId, string customTableId);

    /// <summary>
    /// Reads the complete content (all rows) of a custom table, including its ETag.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    /// <returns>The rows and ETag of the custom table, or <c>null</c> if the response contains no data.</returns>
    Task<CustomTableData?> GetCustomTableData(string libraryId, string customTableId);

    /// <summary>
    /// Replaces the complete content of a custom table in a single call. All existing rows that are not part of the request
    /// are removed. When <see cref="SetCustomTableDataRequest.ETag" /> is set, the change is only applied if the current
    /// content still matches it; otherwise the call fails and nothing is changed, so a concurrent change is not overwritten.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    /// <param name="request">The new content together with the optional ETag to check against.</param>
    /// <returns>The outcome of the update.</returns>
    Task<SetCustomTableDataResult> SetCustomTableData(string libraryId, string customTableId, SetCustomTableDataRequest request);

    /// <summary>
    /// Starts the synchronization of a custom table linked to materialManager. The synchronization runs asynchronously; use
    /// <see cref="CustomTableSyncResult.CorrelationId" /> with <see cref="GetCustomTableSyncState" /> to follow its progress.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table to synchronize.</param>
    /// <returns>
    /// The result of the start request. If the synchronization cannot be started, for example because the custom table is not
    /// linked, <see cref="CustomTableSyncResult.State" /> is <see cref="CustomTableSyncState.Error" /> and
    /// <see cref="CustomTableSyncResult.ErrorText" /> contains the reason; no exception is thrown in this case.
    /// </returns>
    Task<CustomTableSyncResult> StartCustomTableSync(string libraryId, string customTableId);

    /// <summary>
    /// Gets the current state and progress of a custom table synchronization.
    /// </summary>
    /// <param name="correlationId">The correlation id returned by <see cref="StartCustomTableSync" />.</param>
    /// <returns>The state, progress, errors and warnings of the synchronization.</returns>
    /// <exception cref="HomagConnect.Base.Contracts.Exceptions.ProblemDetailsException">The correlation id is unknown.</exception>
    Task<CustomTableSyncStateData?> GetCustomTableSyncState(string correlationId);
}
