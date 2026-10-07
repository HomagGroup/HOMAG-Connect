using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Client;
using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement;
using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

namespace HomagConnect.OrderManager.Client
{
    /// <inheritdoc cref="IConfigurationDataManagementClient" />
    public class ConfigurationDataManagementClient : ClientBase, IConfigurationDataManagementClient
    {
        private static readonly string _BaseRoute = "api/orderManager/configurationDataManagement";
        private static readonly string _LibrariesRoute = $"{_BaseRoute}/libraries";

        /// <inheritdoc />
        public ConfigurationDataManagementClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ConfigurationDataManagementClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ConfigurationDataManagementClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        /// <inheritdoc />
        public async Task<IEnumerable<LibraryOverview>> GetLibraries()
        {
            var uri = new Uri(_LibrariesRoute, UriKind.Relative);
            return await RequestEnumerable<LibraryOverview>(uri) ?? Array.Empty<LibraryOverview>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CustomTableOverview>> GetCustomTables(string libraryId)
        {
            var uri = new Uri($"{CustomTablesRoute(libraryId)}", UriKind.Relative);
            return await RequestEnumerable<CustomTableOverview>(uri) ?? Array.Empty<CustomTableOverview>();
        }

        /// <inheritdoc />
        public async Task<CustomTableStructure?> GetCustomTableStructure(string libraryId, string customTableId)
        {
            var uri = new Uri($"{CustomTableRoute(libraryId, customTableId)}/structure", UriKind.Relative);
            return await RequestObject<CustomTableStructure>(uri);
        }

        /// <inheritdoc />
        public async Task<CustomTableData?> GetCustomTableData(string libraryId, string customTableId)
        {
            var uri = new Uri($"{CustomTableRoute(libraryId, customTableId)}/data", UriKind.Relative);
            return await RequestObject<CustomTableData>(uri);
        }

        /// <inheritdoc />
        public async Task<SetCustomTableDataResult> SetCustomTableData(string libraryId, string customTableId, SetCustomTableDataRequest request)
        {
            var uri = new Uri($"{CustomTableRoute(libraryId, customTableId)}/data", UriKind.Relative);

            return await PutObject<SetCustomTableDataRequest, SetCustomTableDataResult>(uri, request);
        }

        private static string CustomTablesRoute(string libraryId)
        {
            return $"{_LibrariesRoute}/{Uri.EscapeDataString(libraryId)}/customTables";
        }

        private static string CustomTableRoute(string libraryId, string customTableId)
        {
            return $"{CustomTablesRoute(libraryId)}/{Uri.EscapeDataString(customTableId)}";
        }
    }
}
