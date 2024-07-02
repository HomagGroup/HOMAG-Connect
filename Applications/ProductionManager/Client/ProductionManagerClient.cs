using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Client
{
    /// <inheritdoc cref="IProductionManagerClient" />
    public class ProductionManagerClient : ServiceBase, IProductionManagerClient
    {
        #region IProductionManagerClient

        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderAsync(ImportOrderRequest importOrderRequest, FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{projectFile.FullName}' was not found.");
            }

            var fileName = projectFile.Name;

            using var stream = projectFile.OpenRead();

            const string uri = "api/productionManager/orders/import";
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(importOrderRequest, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(importOrderRequest));

            HttpContent streamContent = new StreamContent(stream);
            httpContent.Add(streamContent, fileName, fileName);

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />

        public async Task<ImportOrderStateResponse> GetImportOrderStateAsync(Guid correlationId)
        {
            if (correlationId == Guid.Empty)
            {
                throw new ArgumentException("The correlationId id must not be empty.", nameof(correlationId));
            }

            var url = $"api/productionManager/orders/import/{correlationId}";

            return await RequestObject<ImportOrderStateResponse>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var url = $"/api/productionManager/orders";
            var orders = await RequestEnumerable<Order>(new Uri(url, UriKind.Relative));

            return orders;
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