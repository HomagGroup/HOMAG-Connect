using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
                .Select(c => $"/api/productionManager/orders?take={take}&skip={skip}" + c)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<OrderOverview>(uris);
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

       

        /// <inheritdoc />
        public Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers)
        {
            throw new NotImplementedException();
        }
      

        #endregion

        #region Order import

        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Group group, FileReference[] referencedFiles)
        {
            throw new NotImplementedException();
        }

        

        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile)
        {
            throw new NotImplementedException();
        }

        
       
        /// <inheritdoc />
        public async Task<ImportOrderResponse> ImportOrderRequest(FileInfo projectFile)
        {
            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"Project file '{projectFile.FullName}' was not found.");
            }

            const string uri = "api/orderManager/orders/import";

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
            const string uri = "api/orderManager/orders/import";

            var response = await PostObject(new Uri(uri, UriKind.Relative), order);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            return responseObject ?? new ImportOrderResponse();
        }


        public Task<ImportOrderStateResponse> GetImportOrderState(Guid correlationId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderOverview> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration)
        {
            throw new NotImplementedException();
        }

     
        public Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order, FileReference[] referencedFiles)
        {
            //var request = new HttpRequestMessage { Method = HttpMethod.Post };

            //if (!projectFile.Exists)
            //{
            //    throw new FileNotFoundException($"Project file '{projectFile.FullName}' was not found.");
            //}

            //var fileName = projectFile.Name;

            //using var stream = projectFile.OpenRead();

            //const string uri = "api/orderManager/orders/import";
            //request.RequestUri = new Uri(uri, UriKind.Relative);

            //using var httpContent = new MultipartFormDataContent();

            ////var json = JsonConvert.SerializeObject(importOrderRequest, SerializerSettings.Default);

            ////httpContent.Add(new StringContent(json), nameof(importOrderRequest));

            //HttpContent streamContent = new StreamContent(stream);
            //httpContent.Add(streamContent, fileName, fileName);

            //request.Content = httpContent;

            //var response = await Client.SendAsync(request);

            //await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            //var result = await response.Content.ReadAsStringAsync();

            //var responseObject = JsonConvert.DeserializeObject<ImportOrderResponse>(result);

            //return responseObject ?? new ImportOrderResponse();

            throw new NotImplementedException();
        }

       

        #endregion
    }
}