using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary />
    public interface IProductionManagerClient
    {
        #region Order overview

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

        #endregion

        #region Order details

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

        #endregion

        #region Order import

        /// <summary>
        /// Import an order using a structured zip file.
        /// </summary>
        /// <param name="importOrderRequest">
        /// Import request based on a structured <see cref="Import.ImportOrderRequest" />.
        /// </param>
        /// <param name="projectFile">
        /// Structured zip file, whose format corresponds to the ImportSpecification (
        /// <seealso
        ///     href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md" />
        /// format.
        /// </param>
        Task<ImportOrderResponse> ImportOrderRequest(ImportOrderRequest importOrderRequest, FileInfo projectFile);

        /// <summary>
        /// Get the import state of an order
        /// </summary>
        /// <param name="correlationId">The correlationId for the import job which was triggered when ImportOrderAsync was called</param>
        /// <returns></returns>
        Task<ImportOrderStateResponse> GetImportOrderState(Guid correlationId);

        /// <summary>
        /// Wait for the import to be completed.
        /// </summary>
        Task<Order> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration);

        #endregion

        #region Production prediction

        /// <summary>
        /// Predicts the edgebanding duration.
        /// </summary>
        /// <returns></returns>
        Task<EdgebandingPrediction> Predict(EdgebandingPredictionRequest edgebandingPredictionRequest);

        #endregion
    }
}