using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Client
{
    /// <inheritdoc cref="IProductionManagerClient" />
    public class ProductionManagerClient : ServiceBase, IProductionManagerClient
    {
        #region IProductionManagerClient

        /// <inheritdoc />
        public async Task<ImportOrderResult> ImportOrderAsync(ImportOrderRequest importOrderRequest, FileInfo projectFile)
        {
            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{projectFile.FullName}' not found.");
            }

            return await Task.FromResult(new ImportOrderResult());
        }

        /// <inheritdoc />
        public async Task<ImportOrderStateResponse> GetImportOrderStateAsync(string correlationId)
        {
            return await Task.FromResult(new ImportOrderStateResponse());
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await Task.FromResult(Array.Empty<Order>());
        }

        #endregion

        #region Constructors

        /// <inheritdoc />
        public ProductionManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionId, string authorizationKey) : base(subscriptionId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionId, string authorizationKey, Uri? baseUri) : base(subscriptionId, authorizationKey, baseUri) { }

        #endregion
    }
}