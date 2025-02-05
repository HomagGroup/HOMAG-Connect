using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

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

        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Project project, FileReference[] referencedFiles)
        {
            throw new NotImplementedException();
        }

        public Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile)
        {
            throw new NotImplementedException();
        }

        public Task<ImportOrderResponse> ImportOrderRequest(Order order, FileReference[] referencedFiles)
        {
            throw new NotImplementedException();
        }

        public Task<ImportOrderResponse> ImportOrderRequest(Project project, FileReference[] projectFiles)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ImportOrderResponse> ImportOrderRequest(FileInfo projectFile)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}