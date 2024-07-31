using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Client
{
    /// <inheritdoc cref="IProductionManagerClient" />
    public class ProductionManagerClient : ServiceBase, IProductionManagerClient
    {
        #region IProductionManagerClient

        #region Order import

        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderRequest(ImportOrderRequest importOrderRequest, FileInfo projectFile)
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
        public async Task<ImportOrderStateResponse> GetImportOrderState(Guid correlationId)
        {
            if (correlationId == Guid.Empty)
            {
                throw new ArgumentException("The correlationId id must not be empty.", nameof(correlationId));
            }

            var url = $"api/productionManager/orders/import/{correlationId}";

            return await RequestObject<ImportOrderStateResponse>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<Order> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration)
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

                    var orders = await GetOrders(OrderStatus.New, 1000); // TODO: Replace with GetOrder when available in production system.
                    var order = orders.FirstOrDefault(o => o.Id == currentStatus.OrderId);

                    return order ?? throw new InvalidOperationException($"Order with id '{currentStatus.OrderId}' not found.");
                }
                else if (currentStatus.State == ImportState.Error)
                {
                    throw new ValidationException($"Import failed:{currentStatus.ErrorDetails}");
                }
                else if (currentStatus.State is ImportState.Queued or ImportState.InProgress)
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

        #endregion

        #region Order overview

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetOrders(int take, int skip = 0)
        {
            var url = $"/api/productionManager/orders?take={take}&skip={skip}";
            var orders = await RequestEnumerable<Order>(new Uri(url, UriKind.Relative));

            return orders;
        }

        /// <inheritdoc />
        public Task<IEnumerable<Order>> GetOrders(OrderStatus orderStatus, int take, int skip = 0)
        {
            return GetOrders([orderStatus], take, skip);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetOrders(OrderStatus[] orderStatus, int take, int skip = 0)
        {
            var uris = orderStatus
                .Select(o => $"&orderStatus={Uri.EscapeDataString(o.ToString())}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"/api/productionManager/orders?take={take}&skip={skip}" + c)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<Order>(uris);
        }

        #endregion

        #region Order details

        /// <inheritdoc />
        public async Task<OrderDetails> GetOrder(Guid orderId)
        {
            var url = $"/api/productionManager/orders/{orderId}";
            var orders = await RequestObject<OrderDetails>(new Uri(url, UriKind.Relative));

            return orders;
        }

        /// <inheritdoc />
        public Task<OrderDetails> GetOrderByExternalSystemId(string externalSystemId)
        {
            return GetOrderByExternalSystemId([externalSystemId]).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderDetails>> GetOrderByExternalSystemId(string[] externalSystemIds)
        {
            var validExternalSystemIds = externalSystemIds
                .Select(e => e.Trim())
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Distinct();

            var uris = validExternalSystemIds
                .Select(id => $"&externalSystemId={Uri.EscapeDataString(id)}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"/api/productionManager/orders?" + c.Trim('&'))
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderDetails>(uris);
        }

        #endregion

        #region Prediction

        /// <inhertidoc />
        public async Task<EdgebandingPrediction> Predict(EdgebandingPredictionRequest edgebandingPredictionRequest)
        {
            if (edgebandingPredictionRequest == null)
            {
                throw new ArgumentNullException(nameof(edgebandingPredictionRequest));
            }

            if (edgebandingPredictionRequest.PredictionParts == null || !edgebandingPredictionRequest.PredictionParts.Any())
            {
                throw new ArgumentException("The predictionParts must not be null or empty.", nameof(edgebandingPredictionRequest));
            }

            var uri = new Uri("/api/productionManager/predict/edgebanding", UriKind.Relative);

            return await PostObject<EdgebandingPredictionRequest, EdgebandingPrediction>(uri, edgebandingPredictionRequest).ConfigureAwait(true);
        }

        public async Task<CuttingPrediction> Predict(CuttingPredictionRequest cuttingPredictionRequest)
        {
            if (cuttingPredictionRequest == null)
            {
                throw new ArgumentNullException(nameof(cuttingPredictionRequest));
            }

            if (cuttingPredictionRequest.PredictionParts == null || !cuttingPredictionRequest.PredictionParts.Any())
            {
                throw new ArgumentException("The predictionParts must not be null or empty.", nameof(cuttingPredictionRequest));
            }

            var uri = new Uri("/api/productionManager/predict/cutting", UriKind.Relative);

            return await PostObject<CuttingPredictionRequest, CuttingPrediction>(uri, cuttingPredictionRequest).ConfigureAwait(true);
        }

        #endregion

        #endregion

        #region Constructors

        /// <inheritdoc />
        public ProductionManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion
    }
}