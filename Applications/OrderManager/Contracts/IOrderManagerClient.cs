using HomagConnect.Base.Contracts;
using HomagConnect.OrderManager.Contracts.Customers;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.OrderManager.Contracts
{
    /// <summary />
    public interface IOrderManagerClient
    {
        #region Order overview

        /// <summary>
        /// Get all orders sorted by <see cref="OrderOverview.OrderDate" />.
        /// </summary>
        Task<IEnumerable<OrderOverview>?> GetOrders(int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="OrderOverview.OrderDate" />.
        /// </summary>
        Task<IEnumerable<OrderOverview>> GetOrders(OrderState orderState, int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="OrderOverview.OrderDate" />.
        /// </summary>
        Task<IEnumerable<OrderOverview>> GetOrders(OrderState[] orderState, int take, int skip = 0);

        /// <summary>
        /// Gets all orders for the given order numbers.
        /// </summary>
        Task<IEnumerable<OrderOverview>> GetOrders(string[] orderNumbers);

        #endregion

        #region Order details

        /// <summary>
        /// Get a specific order by its id
        /// </summary>
        Task<OrderDetails?> GetOrder(Guid orderId, bool configurationData = false);

        /// <summary>
        /// Get a specific order by its order number.
        /// </summary>
        Task<OrderDetails?> GetOrder(string orderNumber, bool configurationData = false);

        #endregion

        #region Order import

        /// <summary>
        /// Imports an order. All file references must be provided as URI.
        /// </summary>
        Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order);

        /// <summary>
        /// Imports an order. All file references must be provided either as URIs or as referenced file.
        /// </summary>
        Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order, FileReference[] fileReferences);

        /// <summary>
        /// Imports a Project.zip file as an order.
        /// </summary>
        Task<ImportOrderResponse> ImportOrderRequest(FileInfo projectFile);

        /// <summary>
        /// Get the import state of an order
        /// </summary>
        /// <param name="correlationId">The correlationId for the import job which was triggered when ImportOrderAsync was called</param>
        /// <returns></returns>
        Task<ImportOrderStateResponse?> GetImportOrderState(Guid correlationId);

        /// <summary>
        /// Wait for the import to be completed.
        /// </summary>
        Task<OrderDetails> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration);

        #endregion

        #region Group Import

        /// <summary>
        /// Imports an group. All file references must be provided either as URIs or as referenced file.
        /// </summary>
        Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Group group, FileReference[] fileReferences);

        /// <summary>
        /// Imports a Project.zip file as an group.
        /// </summary>
        Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile);

        #endregion

        #region Order deletion

        /// <summary>
        /// Delete orders by their order ids.
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task DeleteOrdersByOrderIds(IEnumerable<Guid> orderIds);

        /// <summary>
        /// Delete orders by their order numbers.
        /// </summary>
        /// <param name="orderNumbers"></param>
        /// <returns></returns>
        Task DeleteOrdersByOrderNumbers(IEnumerable<string> orderNumbers);

        /// <summary>
        /// Delete order its order id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task DeleteOrdersByOrderId(Guid orderId);

        /// <summary>
        /// Delete order its order number.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        Task DeleteOrdersByOrderNumber(string orderNumber);

        #endregion Order deletion

        #region Customer deletion
        /// <summary>
        /// Delete customers by their customer ids.
        /// </summary>
        /// <param name="customerIds"></param>
        /// <returns></returns>
        Task DeleteCustomersByCustomerIds(IEnumerable<Guid> customerIds);

        /// <summary>
        /// Delete customers by their customer number.
        /// </summary>
        /// <param name="customerNumbers"></param>
        /// <returns></returns>
        Task DeleteCustomersByCustomerNumbers(IEnumerable<string> customerNumbers);
        #endregion

        #region Get customers

        /// <summary>
        /// Get all customers excluding additional data.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer?>> GetCustomers();

        /// <summary>
        /// Get customers by their customer numbers including additional data.
        /// </summary>
        /// <param name="customerNumbers"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer?>> GetCustomers(string[] customerNumbers);

        /// <summary>
        /// Get customers by their customer ids including additional data.
        /// </summary>
        /// <param name="customerIds"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer?>> GetCustomers(Guid[] customerIds);

        /// <summary>
        /// Get a specific customer by its customer number including additional data.
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <returns></returns>
        Task<Customer?> GetCustomer(string customerNumber);

        /// <summary>
        /// Get a specific customer by its customer id including additional data.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Customer?> GetCustomer(Guid customerId);

        #endregion
    }
}