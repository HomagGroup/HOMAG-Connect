using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace HomagConnect.OrderManager.Client
{
    /// <inheritdoc cref="IOrderManagerClient" />
    public class OrderManagerClient : ServiceBase, IOrderManagerClient
    {
        private static readonly string _BaseRoute = "api/orderManager";
        private static readonly string _OrderRoute = $"{_BaseRoute}/orders";

        /// <inheritdoc />
        public OrderManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #region Order overview

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>?> GetOrders(int take, int skip = 0)
        {
            var url = $"{_OrderRoute}?take={take}&skip={skip}";
            return await RequestEnumerable<OrderOverview>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>> GetOrders(OrderState orderState, int take, int skip = 0)
        {
            return await GetOrders([orderState], take, skip);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>> GetOrders(OrderState[] orderState, int take, int skip = 0)
        {
            var uris = orderState
                .Select(o => $"&orderStatus={Uri.EscapeDataString(o.ToString())}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"{_OrderRoute}?take={take}&skip={skip}" + c)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderOverview>(uris);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderOverview>> GetOrders(string[] orderNumbers)
        {
            var uris = orderNumbers
                .Select(code => $"&orderNumber={Uri.EscapeDataString(code)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_OrderRoute}" + parameter)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderOverview>(uris);
        }

        #endregion

        #region Order details

        /// <inheritdoc />
        public async Task<OrderDetails?> GetOrder(Guid orderId)
        {
            var uri = $"{_OrderRoute}/{orderId}";
            return await RequestObject<OrderDetails>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<OrderDetails?> GetOrder(string orderNumber)
        {
            var uri = $"{_OrderRoute}/{Uri.EscapeDataString(orderNumber)}";
            return await RequestObject<OrderDetails>(new Uri(uri, UriKind.Relative));
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

            var uri = $"{_OrderRoute}/import";

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
            var uri = $"{_OrderRoute}/import";

            var response = await PostObject(new Uri(uri, UriKind.Relative), order);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />
        public async Task<ImportOrderStateResponse?> GetImportOrderState(Guid correlationId)
        {
            var uri = $"{_OrderRoute}/import/{correlationId}";
            return await RequestObject<ImportOrderStateResponse>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<OrderDetails> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration)
        {
            var timeout = DateTime.Now + maxDuration;

            while (DateTime.Now < timeout)
            {
                var currentStatus = await GetImportOrderState(correlationId);

                if (currentStatus.State == ImportState.Succeeded)
                {
                    if (currentStatus.OrderId == null)
                    {
                        throw new InvalidOperationException("Import succeeded but no order id was returned.");
                    }

                    var order = await GetOrder(currentStatus.OrderId.Value);

                    return order ?? throw new InvalidOperationException($"Order with id '{currentStatus.OrderId}' not found.");
                }

                if (currentStatus.State == ImportState.Error)
                {
                    throw new ValidationException($"Import failed:{currentStatus.ErrorDetails}");
                }

                if (currentStatus.State is ImportState.Queued or ImportState.InProgress)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                else
                {
                    throw new NotSupportedException($"Unexpected import state: {currentStatus.State}");
                }
            }

            throw new TimeoutException();
        }

        /// <inheritdoc />
        public async Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Group group, FileReference[] fileReferences)
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

            var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));

            if (missingReference != null)
            {
                throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
            }

            var uri = $"{_OrderRoute}/{orderNumber}/groups";

            var request = new HttpRequestMessage { Method = HttpMethod.Post };
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(group, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(group));

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

        /// <inheritdoc />
        public async Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Put };

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{projectFile.FullName}' was not found.");
            }

            var fileName = projectFile.Name;

            using var stream = projectFile.OpenRead();

            var uri = $"{_OrderRoute}/{orderNumber}/groups";
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

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

            var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));

            if (missingReference != null)
            {
                throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
            }

            var uri = $"{_OrderRoute}/import";

            var request = new HttpRequestMessage { Method = HttpMethod.Post };
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(order, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(order));

            foreach (var fileReference in fileReferences)
            {
                httpContent.Add(fileReference);
            }

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderIds(IEnumerable<Guid> orderIds)
        {
            if (orderIds == null)
            {
                throw new ArgumentNullException(nameof(orderIds));
            }

            var distinctOrderIds = orderIds.Distinct()
                .OrderBy(b => b).ToList();

            if (!distinctOrderIds.Any())
            {
                throw new ArgumentNullException(nameof(orderIds), "At least one order id must be passed.");
            }

            var uris = distinctOrderIds
                .Select(id => $"&orderId={id}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_OrderRoute}" + parameter)
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderNumbers(IEnumerable<string> orderNumbers)
        {
            if (orderNumbers == null)
            {
                throw new ArgumentNullException(nameof(orderNumbers));
            }

            var distinctOrderNumbers = orderNumbers.Where(b => !string.IsNullOrWhiteSpace(b))
                .Distinct()
                .OrderBy(b => b).ToList();

            if (!distinctOrderNumbers.Any())
            {
                throw new ArgumentNullException(nameof(orderNumbers), "At least one order number must be passed.");
            }

            var uris = distinctOrderNumbers
                .Select(number => $"&orderNumber={number}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_OrderRoute}" + parameter)
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderId(Guid orderId)
        {
            await DeleteOrdersByOrderIds([orderId]);
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderNumber(string orderNumber)
        {
            await DeleteOrdersByOrderNumbers([orderNumber]);
        }

        #endregion
    }
}