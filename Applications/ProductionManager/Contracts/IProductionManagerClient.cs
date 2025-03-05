using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;
using System.Text;

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
        /// Get a specific order by its order number.
        /// </summary>
        Task<OrderDetails> GetOrder(string orderNumber);

        /// <summary>
        /// Gets all orders for the given order numbers.
        /// </summary>
        Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers);

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

        #region Order deletion

        /// <summary>
        /// Deletes an order by its id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task DeleteOrderByOrderId(string orderId);

        /// <summary>
        /// Deletes multiple orders by their ids
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task DeleteOrderByOrderIds(string[] orderIds);

        /// <summary>
        /// Deletes an order by its order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        Task DeleteOrderByOrderNumber(string orderNumber);

        /// <summary>
        /// Deletes multiple orders by their order numbers
        /// </summary>
        /// <param name="orderNumbers"></param>
        /// <returns></returns>
        Task DeleteOrderByOrderNumbers(string[] orderNumbers);

        #endregion Order deletion

        #region Production prediction

        /// <summary>
        /// Predicts the edgebanding duration based on a list of parts and the length of edgebands
        /// </summary>
        /// <returns></returns>
        Task<EdgebandingPrediction> Predict(EdgebandingPredictionRequest edgebandingPredictionRequest);

        /// <summary>
        /// Predicts the cutting duration based on the number of parts in the request
        /// </summary>
        /// <returns></returns>
        Task<CuttingPrediction> Predict(CuttingPredictionRequest cuttingPredictionRequest);

        /// <summary>
        /// Predicts the cutting duration based on the number of parts with CncProgramName in the request
        /// </summary>
        /// <returns></returns>
        Task<CncPrediction> Predict(CncPredictionRequest cncPredictionRequest);

        #endregion
    }
}