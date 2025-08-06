using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Lots;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

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

            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result, SerializerSettings.Default);

            return responseObject ?? new ImportOrderResponse();
        }

        /// <inheritdoc />
        public async Task<ImportOrderStateResponse?> GetImportOrderState(Guid correlationId)
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

                    var order = await GetOrder(currentStatus.OrderId.Value);

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
        public async Task<IEnumerable<Order>?> GetOrders(int take, int skip = 0)
        {
            var url = $"/api/productionManager/orders?take={take}&skip={skip}";
            var orders = await RequestEnumerable<Order>(new Uri(url, UriKind.Relative));

            return orders;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetOrders(OrderStatus orderStatus, int take, int skip = 0)
        {
            return await GetOrders([orderStatus], take, skip);
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

        /// <inheritdoc />
        public async Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers)
        {
            var validOrderNumbers = orderNumbers
                .Select(e => e.Trim())
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Distinct();

            var uris = validOrderNumbers
                .Select(id => $"&orderNumber={Uri.EscapeDataString(id)}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"/api/productionManager/orders?" + c.Trim('&'))
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderDetails>(uris);
        }

        #endregion

        #region Order item

        /// <inheritdoc />
        public async Task<ProductionItemBase[]?> GetOrderItems(string[] identifiers)
        {
            const string parameter = "identifier";
            const string endpoint = "api/productionManager/orderItems";

            var uris = identifiers.Select(i => i.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(code => $"&{parameter}={Uri.EscapeDataString(code)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            return (await RequestEnumerableAsync<ProductionItemBase>(uris)).ToArray();
        }

        #endregion

        #region Order details

        /// <inheritdoc />
        public async Task<OrderDetails?> GetOrder(Guid orderId)
        {
            var url = $"/api/productionManager/orders/{orderId}";
            var order = await RequestObject<OrderDetails>(new Uri(url, UriKind.Relative));

            return order;
        }

        /// <inheritdoc />
        public Task<OrderDetails> GetOrder(string orderNumber)
        {
            return GetOrders([orderNumber]).FirstOrDefaultAsync();
        }

        #endregion

        #region Order release

        /// <inheritdoc />
        public async Task ReleaseOrder(Guid orderId)
        {
            var url = $"/api/productionManager/orders/{orderId}/release";
            var response = await PatchObject(new Uri(url, UriKind.Relative));

            response.EnsureSuccessStatusCode();
        }

        /// <inheritdoc />
        public async Task ResetReleaseOrder(Guid orderId)
        {
            var url = $"/api/productionManager/orders/{orderId}/resetRelease";
            var response = await PatchObject(new Uri(url, UriKind.Relative));

            response.EnsureSuccessStatusCode();
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

        /// <inhertidoc />
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

        /// <inhertidoc />
        public async Task<CncPrediction> Predict(CncPredictionRequest cncPredictionRequest)
        {
            if (cncPredictionRequest == null)
            {
                throw new ArgumentNullException(nameof(cncPredictionRequest));
            }

            if (cncPredictionRequest.PredictionParts == null || !cncPredictionRequest.PredictionParts.Any())
            {
                throw new ArgumentException("The predictionParts must not be null or empty.", nameof(cncPredictionRequest));
            }

            var uri = new Uri("/api/productionManager/predict/cnc", UriKind.Relative);

            return await PostObject<CncPredictionRequest, CncPrediction>(uri, cncPredictionRequest).ConfigureAwait(true);
        }

        #endregion

        #region Order deletion

        /// <inheritdoc />
        public async Task DeleteOrderByOrderId(Guid orderId)
        {
            await DeleteOrdersByOrderIds([orderId]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderIds(Guid[] orderIds)
        {
            var uri = new StringBuilder($"/api/productionManager/orders?orderId={orderIds[0]}");
            for (var i = 1; i < orderIds.Length; i++)
            {
                uri.Append($"&orderId={orderIds[i]}");
            }

            await DeleteObject(new Uri(uri.ToString(), UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteOrderByOrderNumber(string orderNumber)
        {
            await DeleteOrdersByOrderNumbers([orderNumber]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderNumbers(string[] orderNumbers)
        {
            var uri = new StringBuilder($"/api/productionManager/orders?orderNumber={Uri.EscapeDataString(orderNumbers[0])}");
            for (var i = 1; i < orderNumbers.Length; i++)
            {
                uri.Append($"&orderNumber={Uri.EscapeDataString(orderNumbers[i])}");
            }

            await DeleteObject(new Uri(uri.ToString(), UriKind.Relative));
        }

        #endregion Order deletion

        #region Lot deletion

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotByLotId(Guid lotId)
        {
            await DeleteOrDecomposeLotsByLotIds([lotId]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotsByLotIds(Guid[] lotIds)
        {
            var uri = new StringBuilder($"/api/productionManager/lots?lotId={lotIds[0]}");
            for (var i = 1; i < lotIds.Length; i++)
            {
                uri.Append($"&lotId={lotIds[i]}");
            }

            await DeleteObject(new Uri(uri.ToString(), UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotByLotName(string lotName)
        {
            await DeleteOrDecomposeLotsByLotNames([lotName]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotsByLotNames(string[] lotNames)
        {
            var uri = new StringBuilder($"/api/productionManager/lots?lotName={Uri.EscapeDataString(lotNames[0])}");
            for (var i = 1; i < lotNames.Length; i++)
            {
                uri.Append($"&lotName={Uri.EscapeDataString(lotNames[i])}");
            }

            await DeleteObject(new Uri(uri.ToString(), UriKind.Relative));
        }

        #endregion Lot deletion

        #region Lot creation

        /// <inheritdoc />
        public async Task<CreateLotResponse> CreateLotRequest(CreateLotRequest createLotRequest)
        {
            const string uri = "api/productionManager/lots";

            var response = await PostObject(new Uri(uri, UriKind.Relative), createLotRequest);
            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<CreateLotResponse>(result, SerializerSettings.Default);
            return responseObject ?? new CreateLotResponse();
        }

        #endregion

        #region Lot overview

        /// <inheritdoc />
        public async Task<IEnumerable<Lot>?> GetLots(int take, int skip = 0)
        {
            var url = $"/api/productionManager/lots?take={take}&skip={skip}";
            var lots = await RequestEnumerable<Lot>(new Uri(url, UriKind.Relative));

            return lots;
        }

        /// <inheritdoc />
        public async Task<LotDetails?> GetLotDetails(string identifier)
        {
            var url = $"/api/productionManager/lots/{Uri.EscapeDataString(identifier)}";
            var lotDetail = await RequestObject<LotDetails>(new Uri(url, UriKind.Relative));

            return lotDetail;
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