using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Lots;
using HomagConnect.ProductionManager.Contracts.OrderProgress;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using HomagConnect.ProductionManager.Contracts.ProductionProtocol;
using HomagConnect.ProductionManager.Contracts.Rework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ValidationException(result);
            }

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

        /// <inheritdoc />
        public async Task<ImportOrderStateResponse> ImportViaTemplateAsync(string templateId, FileInfo importFile, string? orderName = null)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            var requestUri = $"api/productionManager/orders/import/templates/{templateId}";

            if (!string.IsNullOrEmpty(orderName))
            {
                requestUri += $"?orderName={Uri.EscapeDataString(orderName)}";
            }

            if (!importFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{importFile.FullName}' was not found.");
            }

            var fileName = importFile.Name;

            using var stream = importFile.OpenRead();
            request.RequestUri = new Uri(requestUri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            HttpContent streamContent = new StreamContent(stream);
            httpContent.Add(streamContent, fileName, fileName);
            request.Content = httpContent;

            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseObject = JsonConvert.DeserializeObject<ImportOrderStateResponse>(result, SerializerSettings.Default);
            return responseObject ?? new ImportOrderStateResponse();
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

        /// <inheritdoc />
        public async Task<IEnumerable<ImportTemplate>?> GetImportTemplates()
        {
            var url = "/api/productionManager/orders/import/templates";
            return await RequestEnumerable<ImportTemplate>(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Order item

        /// <inheritdoc />
        public async Task<ProductionItemBase[]?> GetOrderItems(string[] identifiers)
        {
            const string parameter = "identifier";
            const string endpoint = "/api/productionManager/orderItems";

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
            var endpoint = "/api/productionManager/orders";

            var uris = orderIds
                .Select(id => $"&orderId={id}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri);
            }
        }

        /// <inheritdoc />
        public async Task DeleteOrderByOrderNumber(string orderNumber)
        {
            await DeleteOrdersByOrderNumbers([orderNumber]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrdersByOrderNumbers(string[] orderNumbers)
        {
            const string endpoint = "/api/productionManager/orders";

            var uris = orderNumbers.Select(i => i.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(orderNumber => $"&orderNumber={Uri.EscapeDataString(orderNumber)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri);
            }
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
            var endpoint = "/api/productionManager/lots";

            var uris = lotIds
                .Select(id => $"&lotId={id}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri);
            }
        }

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotByLotName(string lotName)
        {
            await DeleteOrDecomposeLotsByLotNames([lotName]).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteOrDecomposeLotsByLotNames(string[] lotNames)
        {
            var endpoint = "/api/productionManager/lots";

            var uris = lotNames.Select(i => i.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(lotName => $"&lotName={Uri.EscapeDataString(lotName)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            foreach (var uri in uris)
            {
                await DeleteObject(uri);
            }
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

        #region Rework

        /// <inheritdoc />
        public async Task<IEnumerable<Rework>?> GetRequestedReworks()
        {
            return await GetCurrentReworks([ReworkState.Pending]);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Rework>?> GetApprovedReworks()
        {
            return await GetCurrentReworks([ReworkState.Approved]);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Rework>?> GetCompletedReworks()
        {
            return await GetCurrentReworks([ReworkState.Rejected, ReworkState.Transferred]);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Rework>?> GetCurrentReworks(ReworkState[]? states, DateTimeOffset? capturedAtFrom = null, DateTimeOffset? capturedAtTo = null, int take = int.MaxValue, int skip = 0)
        {
            var url = "/api/productionManager/reworks";

            var queryParameters = new List<string>();

            if (states is { Length: > 0 })
            {
                queryParameters.AddRange(states.Select(reworkState => $"state={Uri.EscapeDataString(reworkState.ToString())}"));
            }

            if (capturedAtFrom.HasValue)
            {
                queryParameters.Add($"capturedAtFrom={Uri.EscapeDataString(capturedAtFrom.Value.ToString("o", CultureInfo.InvariantCulture))}");
            }

            if (capturedAtTo.HasValue)
            {
                queryParameters.Add($"capturedAtTo={Uri.EscapeDataString(capturedAtTo.Value.ToString("o", CultureInfo.InvariantCulture))}");
            }

            if (skip > 0)
            {
                queryParameters.Add($"skip={skip}");
            }

            if (take > 0 && take != int.MaxValue)
            {
                queryParameters.Add($"take={take}");
            }

            queryParameters.Add($"completed=true"); // TODO: remove when API supports non-completed reworks

            if (queryParameters.Count > 0)
            {
                url += $"?{string.Join("&", queryParameters)}";
            }

            var completedReworks = await RequestEnumerable<Rework>(new Uri(url, UriKind.Relative));

            return completedReworks;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Rework>> GetReworks(
            DateTime? from = null,
            DateTime? to = null,
            int? daysBack = null,
            ReworkState? state = null,
            string? identifier = null,
            string? reworkId = null,
            int? take = null,
            int? skip = null)
        {
            var url = "/api/productionManager/statistics/reworks";

            var queryParameters = new List<string>();

            if (from.HasValue)
            {
                queryParameters.Add($"from={Uri.EscapeDataString(from.Value.ToString("o", CultureInfo.InvariantCulture))}");
            }

            if (to.HasValue)
            {
                queryParameters.Add($"to={Uri.EscapeDataString(to.Value.ToString("o", CultureInfo.InvariantCulture))}");
            }

            if (daysBack.HasValue)
            {
                queryParameters.Add($"daysBack={daysBack.Value}");
            }

            if (state.HasValue)
            {
                queryParameters.Add($"state={Uri.EscapeDataString(state.Value.ToString())}");
            }

            if (!string.IsNullOrWhiteSpace(identifier))
            {
                queryParameters.Add($"identifier={Uri.EscapeDataString(identifier)}");
            }

            if (!string.IsNullOrWhiteSpace(reworkId))
            {
                queryParameters.Add($"reworkId={Uri.EscapeDataString(reworkId)}");
            }

            if (take.HasValue)
            {
                queryParameters.Add($"take={take.Value}");
            }

            if (skip.HasValue && skip.Value > 0)
            {
                queryParameters.Add($"skip={skip.Value}");
            }

            if (queryParameters.Count > 0)
            {
                url += $"?{string.Join("&", queryParameters)}";
            }

            var reworks = await RequestEnumerable<Rework>(new Uri(url, UriKind.Relative));

            return reworks ?? Enumerable.Empty<Rework>();
        }       

        #endregion Rework

        #region ProductionProtocol

        /// <inheritdoc />
        public async Task<IEnumerable<ProcessedItem>?> GetProductionProtocol(string workstationId, int take = 100000, int skip = 0, int daysBack = 7)
        {
            var url = $"/api/productionManager/workstations/{Uri.EscapeDataString(workstationId)}/productionprotocol?daysBack={daysBack}";
            var productionProtocol = await RequestObject<IEnumerable<ProcessedItem>?>(new Uri(url, UriKind.Relative));

            return productionProtocol;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Workstation>?> GetWorkstations()
        {
            const string uri = "api/productionManager/workstations";

            return await RequestEnumerable<Workstation>(new Uri(uri, UriKind.Relative));
        }

        #endregion

        #region Usage statistics

        /// <inheritdoc />
        public async Task<IEnumerable<UsageOverview>> GetUsageOverview(int monthsAgo = 12)
        {
            var url = $"/api/productionManager/statistics/usage/overview?monthsAgo={monthsAgo}";
            var usageOverview = await RequestEnumerable<UsageOverview>(new Uri(url, UriKind.Relative));
            return usageOverview ?? Enumerable.Empty<UsageOverview>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UsageDetails>> GetUsageDetails(int monthsAgo = 12)
        {
            var url = $"/api/productionManager/statistics/usage/details?monthsAgo={monthsAgo}";
            var usageDetails = await RequestEnumerable<UsageDetails>(new Uri(url, UriKind.Relative));
            return usageDetails ?? Enumerable.Empty<UsageDetails>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UsageDetails>> GetUsageDetailsForPeriod(string period)
        {
            var url = $"/api/productionManager/statistics/usage/details/{Uri.EscapeDataString(period)}";
            var usageDetails = await RequestEnumerable<UsageDetails>(new Uri(url, UriKind.Relative));
            return usageDetails ?? Enumerable.Empty<UsageDetails>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UsageDetails>> GetCurrentUsage()
        {
            const string url = "/api/productionManager/statistics/usage/current";
            var usageDetails = await RequestEnumerable<UsageDetails>(new Uri(url, UriKind.Relative));
            return usageDetails ?? Enumerable.Empty<UsageDetails>();
        }

        /// <inhertidoc />
        public async Task<IEnumerable<OrderProgressDetails>?> GetOrderProgress(OrderProgressRequest orderProgressRequest)
        {
            string uri = "api/productionManager/orderprogress";
            return await PostObject<OrderProgressRequest, IEnumerable<OrderProgressDetails>>(new Uri(uri, UriKind.Relative), orderProgressRequest);
        }
        #endregion Usage statistics

        #region Constructors

        /// <inheritdoc />
        public ProductionManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion

        #endregion
    }
}