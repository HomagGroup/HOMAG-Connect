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
        Task<Order> GetOrder(Guid orderId);

        /// <summary>
        /// Get a specific order by its external system id
        /// </summary>
        Task<Order> GetOrderByExternalSystemId(string externalSystemId);

        /// <summary>
        /// Gets all orders for the given external system ids.
        /// </summary>
        Task<IEnumerable<Order>> GetOrderByExternalSystemId(string[] externalSystemIds);

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


        #region production time prediction

        /// <summary>
        /// Get for a Machine
        /// </summary>
        /// <param name="machineNumber">
        /// optional. If omitted, all edgeband machines of the customer are used or even a global fallback
        /// </param>
        /// <param name="productionEntities"></param>
        /// <returns></returns>
        Task<EdgebandPredictionResponse> PredictProductionEntitiesListForMachine(string? machineNumber, IEnumerable<EdgebandPredictPart> productionEntities);
        
        
        /// <summary>
        /// Predict on subscription level or on a global model
        /// </summary>
        /// <returns></returns>
        Task<EdgebandPredictionResponse> PredictProductionEntitiesList(IEnumerable<EdgebandPredictPart> productionEntities);

        #endregion
    }
}