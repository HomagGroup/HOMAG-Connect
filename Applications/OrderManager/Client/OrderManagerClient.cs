using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.OrderManager.Client
{
    /// <inheritdoc cref="IOrderManagerClient" />
    public class OrderManagerClient : ServiceBase, IOrderManagerClient
    {
        /// <inheritdoc />
        protected OrderManagerClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        protected OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        protected OrderManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #region Order overview

        /// <inheritdoc />
        public Task<IEnumerable<OrderOverview>> GetOrders(int take, int skip = 0)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<OrderOverview>> GetOrders(OrderState orderState, int take, int skip = 0)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<OrderOverview>> GetOrders(OrderState[] orderState, int take, int skip = 0)
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

        /// <inheritdoc />
        public Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Order import

        /// <inheritdoc />
        public Task<ImportOrderResponse> ImportOrderRequest(ImportOrderRequest importOrderRequest, FileInfo projectFile)
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