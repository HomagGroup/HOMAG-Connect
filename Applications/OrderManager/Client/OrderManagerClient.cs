using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace HomagConnect.OrderManager.Client
{
    /// <inheritdoc cref="IOrderManagerClient" />
    public class OrderManagerClient : ServiceBase, IOrderManagerClient
    {
        /// <inheritdoc />
        public OrderManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #region Order overview

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>> GetOrders(int take, int skip = 0)
        {
            var url = $"/api/orderManager/orders?take={take}&skip={skip}";
            var orders = await RequestEnumerable<OrderOverview>(new Uri(url, UriKind.Relative));

            return orders;
        }

        /// <inheritdoc />
        public Task<IEnumerable<OrderOverview>> GetOrders(OrderState orderState, int take, int skip = 0)
        {
            return GetOrders([orderState], take, skip);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>> GetOrders(OrderState[] orderState, int take, int skip = 0)
        {
            var uris = orderState
                .Select(o => $"&orderStatus={Uri.EscapeDataString(o.ToString())}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"/api/orderManager/orders?take={take}&skip={skip}" + c)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderOverview>(uris);
        }

        /// <inheritdoc />
        public Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Order details

        /// <inheritdoc />
        public Task<OrderDetails> GetOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<OrderDetails> GetOrder(string orderNumber)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Order import

        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderRequest(FileInfo projectFile)
        {
            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{projectFile.FullName}' was not found.");
            }

            const string uri = "api/orderManager/orders";

            using var stream = projectFile.OpenRead();

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri, UriKind.Relative));

            using var requestContent = new StreamContent(stream);

            request.Content = requestContent;
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(content);
            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(await reader.ReadToEndAsync());

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order)
        {
            const string uri = "api/orderManager/orders";

            var response = await PostObject(new Uri(uri, UriKind.Relative), order);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />
        public Task<ImportOrderStateResponse> GetImportOrderState(Guid correlationId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<OrderOverview> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Group group, FileReference[] referencedFiles)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order, FileReference[] fileReferences)
        {
            if (fileReferences == null)
            {
                throw new ArgumentNullException(nameof(fileReferences));
            }

            var missingFile = fileReferences.FirstOrDefault(f => !f.FileInfo.Exists);

            if (missingFile != null)
            {
                throw new FileNotFoundException($"File '{missingFile.FileInfo.FullName}' was not found.");
            }

            var missingReference= fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));

            if (missingReference != null)
            {
                throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
            }
            
            const string uri = "api/orderManager/orders";

            var request = new HttpRequestMessage { Method = HttpMethod.Post };
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(order, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(order));

            foreach (var fileReference in fileReferences)
            {
                var fileStream = fileReference.FileInfo.OpenRead();

                HttpContent streamContent = new StreamContent(fileStream);
                httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
            }

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }

        #endregion
    }
}