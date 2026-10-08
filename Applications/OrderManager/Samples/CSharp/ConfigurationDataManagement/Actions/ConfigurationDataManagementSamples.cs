using System.Collections.ObjectModel;

using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement;
using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

namespace HomagConnect.OrderManager.Samples.ConfigurationDataManagement.Actions
{
    /// <summary>
    /// Sample class which shows how to read and write configuration data management custom tables.
    /// </summary>
    public static class ConfigurationDataManagementSamples
    {
        /// <summary>
        /// Get the list of available libraries.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<IEnumerable<LibraryOverview>> GetLibraries(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraries = await configurationDataManagement.GetLibraries();

            return libraries;
        }

        /// <summary>
        /// Get the list of custom tables of a library.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<IEnumerable<CustomTableOverview>> GetCustomTables(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraryId = "Default"; // the library name is its identifier

            var customTables = await configurationDataManagement.GetCustomTables(libraryId);

            return customTables;
        }

        /// <summary>
        /// Read the structure (column definitions) of a custom table.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<CustomTableStructure?> GetCustomTableStructure(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraryId = "Default";
            var customTableId = "MyCustomTable";

            var structure = await configurationDataManagement.GetCustomTableStructure(libraryId, customTableId);

            return structure;
        }

        /// <summary>
        /// Read the complete content (all rows) of a custom table.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<CustomTableData?> GetCustomTableData(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraryId = "Default";
            var customTableId = "MyCustomTable";

            var data = await configurationDataManagement.GetCustomTableData(libraryId, customTableId);

            return data;
        }

        /// <summary>
        /// Replace the complete content of a custom table with new rows.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<SetCustomTableDataResult> SetCustomTableData(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraryId = "Default";
            var customTableId = "MyCustomTable";

            var request = new SetCustomTableDataRequest
            {
                Rows =
                [
                    new CustomTableRow
                    {
                        // RowId omitted => a new id is generated.
                        Cells =
                        [
                            new CustomTableCell { ColumnId = "Name", Value = "Row 1" },
                            new CustomTableCell { ColumnId = "Width", Value = 100 }
                        ]
                    },
                    new CustomTableRow
                    {
                        // RowId set => the existing row keeps this id.
                        RowId = "1",
                        Cells =
                        [
                            new CustomTableCell { ColumnId = "Name", Value = "Row 2" },
                            new CustomTableCell { ColumnId = "Width", Value = 200 }
                        ]
                    }
                ]
            };

            var result = await configurationDataManagement.SetCustomTableData(libraryId, customTableId, request);

            return result;
        }

        /// <summary>
        /// Read the current content, change it, and write it back using the ETag so a concurrent change is not overwritten.
        /// </summary>
        /// <param name="configurationDataManagement"></param>
        public static async Task<SetCustomTableDataResult?> UpdateCustomTableDataWithETag(IConfigurationDataManagementClient configurationDataManagement)
        {
            var libraryId = "Default";
            var customTableId = "MyCustomTable";

            var current = await configurationDataManagement.GetCustomTableData(libraryId, customTableId);

            if (current == null)
            {
                return null;
            }

            current.Rows.Add(new CustomTableRow
            {
                Cells = new Collection<CustomTableCell>
                {
                    new CustomTableCell { ColumnId = "Name", Value = "Added row" }
                }
            });

            var request = new SetCustomTableDataRequest
            {
                // Pass back the ETag from the read so the write is rejected if the content changed in the meantime.
                ETag = current.ETag,
                Rows = current.Rows
            };

            var result = await configurationDataManagement.SetCustomTableData(libraryId, customTableId, request);

            return result;
        }
    }
}
