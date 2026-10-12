using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement;

/// <summary>
/// Client for the configuration data management (cDM) functions of the OrderManager, giving read and write access to the
/// custom tables of a library.
/// </summary>
public interface IConfigurationDataManagementClient
{
    /// <summary>
    /// Gets the list of available configuration data management libraries.
    /// </summary>
    Task<IEnumerable<LibraryOverview>> GetLibraries();

    /// <summary>
    /// Gets the list of custom tables of the specified library.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    Task<IEnumerable<CustomTableOverview>> GetCustomTables(string libraryId);

    /// <summary>
    /// Reads the structure (column definitions) of a custom table, including its ETag.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    Task<CustomTableStructure?> GetCustomTableStructure(string libraryId, string customTableId);

    /// <summary>
    /// Reads the complete content (all rows) of a custom table, including its ETag.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    Task<CustomTableData?> GetCustomTableData(string libraryId, string customTableId);

    /// <summary>
    /// Replaces the complete content of a custom table in a single call. All existing rows that are not part of the request
    /// are removed. When <see cref="SetCustomTableDataRequest.ETag" /> is set, the change is only applied if the current
    /// content still matches it; otherwise the call fails and nothing is changed, so a concurrent change is not overwritten.
    /// </summary>
    /// <param name="libraryId">The name of the library, which is its unique identifier.</param>
    /// <param name="customTableId">The id of the custom table.</param>
    /// <param name="request">The new content together with the optional ETag to check against.</param>
    Task<SetCustomTableDataResult> SetCustomTableData(string libraryId, string customTableId, SetCustomTableDataRequest request);
}
