using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary />
    public interface IProductionManagerClient
    {
        /// <summary>
        /// Get the import state of an order
        /// </summary>
        /// <param name="correlationId">The correlationId for the import job which was triggered when ImportOrderAsync was called</param>
        /// <returns></returns>
        Task<ImportOrderStateResponse> GetImportOrderStateAsync(Guid correlationId);

        /// <summary>
        /// Get a specific order by its id
        /// </summary>
        Task<OrderDetails> GetOrder(Guid orderId);

        /// <summary>
        /// Get a specific order by its external system id
        /// </summary>
        Task<OrderDetails> GetOrderByExternalSystemId(string externalSystemId);

        /// <summary>
        /// Gets all orders for the given external system ids.
        /// </summary>
        Task<IEnumerable<OrderDetails>> GetOrderByExternalSystemId(string[] externalSystemIds);

        /// <summary>
        /// Get all orders sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>> GetOrders(int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>> GetOrders(OrderStatus orderStatus, int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>> GetOrders(OrderStatus[] orderStatus, int take, int skip = 0);

        #region Obsolete

        /// <summary />
        [Obsolete("Use GetOrders instead.", true)]
        Task<IEnumerable<Order>> GetOrdersAsync();

        #endregion

        /// <summary>
        /// Import an order using a structured zip file.
        /// </summary>
        /// <param name="importOrderRequest">
        /// Import request based on a structured <see cref="ImportOrderRequest" />.
        /// </param>
        /// <param name="projectFile">
        /// Structured zip file, whose format corresponds to the ImportSpecification (
        /// <seealso
        ///     href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md" />
        /// format.
        /// </param>
        Task<ImportOrderResponse> ImportOrderAsync(ImportOrderRequest importOrderRequest, FileInfo projectFile);
    }
}