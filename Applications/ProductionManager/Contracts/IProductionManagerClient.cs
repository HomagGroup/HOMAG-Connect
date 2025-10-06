﻿using HomagConnect.ProductionManager.Contracts.Import;
using HomagConnect.ProductionManager.Contracts.Lots;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.Predict;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary />
    public interface IProductionManagerClient
    {
        #region Order overview

        /// <summary>
        /// Get all orders sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>?> GetOrders(int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>> GetOrders(OrderStatus orderStatus, int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="Order.OrderDate" />.
        /// </summary>
        Task<IEnumerable<Order>> GetOrders(OrderStatus[] orderStatus, int take, int skip = 0);

        #endregion

        #region Order items

        /// <summary>
        /// Get order item by its identifier (public id or id)
        /// </summary>
        public Task<ProductionItemBase[]?> GetOrderItems(string[] identifiers);

        #endregion

        #region Order details

        /// <summary>
        /// Get a specific order by its id
        /// </summary>
        Task<OrderDetails?> GetOrder(Guid orderId);

        /// <summary>
        /// Get a specific order by its order number.
        /// </summary>
        Task<OrderDetails> GetOrder(string orderNumber);

        /// <summary>
        /// Gets all orders for the given order numbers.
        /// </summary>
        Task<IEnumerable<OrderDetails>> GetOrders(string[] orderNumbers);

        /// <summary>
        /// Get available import templates for parts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ImportTemplate>?> GetImportTemplates();

        #endregion

        #region Order release

        /// <summary>
        /// Release order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task ReleaseOrder(Guid orderId);

        /// <summary>
        /// Reset release order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task ResetReleaseOrder(Guid orderId);

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
        Task<ImportOrderStateResponse?> GetImportOrderState(Guid correlationId);

        /// <summary>
        /// Wait for the import to be completed.
        /// </summary>
        Task<Order> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration);

        /// <summary>
        /// Importing part data via template to an existing order or create a new order
        /// </summary>
        /// <returns></returns>
        Task<ImportOrderStateResponse> ImportViaTemplateAsync(string templateId, FileInfo importFile, string? orderName = null);

        #endregion

        #region Order deletion

        /// <summary>
        /// Deletes an order by its id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task DeleteOrderByOrderId(Guid orderId);

        /// <summary>
        /// Deletes multiple orders by their ids
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task DeleteOrdersByOrderIds(Guid[] orderIds);

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
        Task DeleteOrdersByOrderNumbers(string[] orderNumbers);

        #endregion Order deletion

        #region Lot deletion

        /// <summary>
        /// Deletes or decomposes a lot by its id
        /// </summary>
        /// <param name="lotId"></param>
        /// <returns></returns>
        Task DeleteOrDecomposeLotByLotId(Guid lotId);

        /// <summary>
        /// Deletes or decomposes lots by their ids
        /// </summary>
        /// <param name="lotIds"></param>
        /// <returns></returns>
        Task DeleteOrDecomposeLotsByLotIds(Guid[] lotIds);

        /// <summary>
        /// Deletes or decomposes a lot by its name
        /// </summary>
        /// <param name="lotName"></param>
        /// <returns></returns>
        Task DeleteOrDecomposeLotByLotName(string lotName);

        /// <summary>
        /// Deletes or decomposes lots by their names
        /// </summary>
        /// <param name="lotNames"></param>
        /// <returns></returns>
        Task DeleteOrDecomposeLotsByLotNames(string[] lotNames);

        #endregion

        #region Lot creation

        /// <summary>
        /// Creates a new lot based on the given request
        /// </summary>
        /// <param name="createLotRequest"></param>
        /// <returns></returns>
        Task<CreateLotResponse> CreateLotRequest(CreateLotRequest createLotRequest);

        #endregion

        #region Lot overview

        /// <summary>
        /// Get all lots
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public Task<IEnumerable<Lot>?> GetLots(int take, int skip = 0);

        /// <summary>
        /// Get a single lot details by its identifier (public id or id)
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public Task<LotDetails?> GetLotDetails(string identifier);

        #endregion

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