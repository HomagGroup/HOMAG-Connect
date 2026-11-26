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
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The orders, or <c>null</c> if none are found.</returns>
        Task<IEnumerable<OrderOverview>?> GetOrders(int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="OrderOverview.OrderDate" />.
        /// </summary>
        /// <param name="orderState">The state the returned orders must have.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The orders having the specified state.</returns>
        Task<IEnumerable<OrderOverview>> GetOrders(OrderState orderState, int take, int skip = 0);

        /// <summary>
        /// Get all orders having the specified status sorted by <see cref="OrderOverview.OrderDate" />.
        /// </summary>
        /// <param name="orderState">The states the returned orders can have. An order matches if it has any of them.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The orders having any of the specified states.</returns>
        Task<IEnumerable<OrderOverview>> GetOrders(OrderState[] orderState, int take, int skip = 0);

        /// <summary>
        /// Gets all orders for the given order numbers.
        /// </summary>
        /// <param name="orderNumbers">The order numbers of the orders to return.</param>
        /// <returns>The orders with the given order numbers.</returns>
        Task<IEnumerable<OrderOverview>> GetOrders(string[] orderNumbers);

        /// <summary>
        /// Gets all orders for the given order state and the latest changedSince Date.
        /// </summary>
        /// <param name="orderState">The state the returned orders must have.</param>
        /// <param name="changedSince">Only orders changed on or after this date and time are returned.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The matching orders, or <c>null</c> if none are found.</returns>
        Task<IEnumerable<OrderOverview>?> GetOrdersChangedSince(OrderState orderState, DateTime changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets all orders for the given order states and the latest changedSince Date.
        /// </summary>
        /// <param name="orderStates">The states the returned orders can have. An order matches if it has any of them.</param>
        /// <param name="changedSince">Only orders changed on or after this date and time are returned.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The matching orders, or <c>null</c> if none are found.</returns>
        Task<IEnumerable<OrderOverview>?> GetOrdersChangedSince(OrderState[] orderStates, DateTime changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets all orders for the given order state and the latest releasedSince Date.
        /// </summary>
        /// <param name="orderState">The state the returned orders must have.</param>
        /// <param name="releasedSince">Only orders released on or after this date and time are returned.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The matching orders, or <c>null</c> if none are found.</returns>
        Task<IEnumerable<OrderOverview>?> GetOrdersReleasedSince(OrderState orderState, DateTime releasedSince, int take, int skip = 0);

        /// <summary>
        /// Gets all orders for the given order states and the latest releasedSince Date.
        /// </summary>
        /// <param name="orderStates">The states the returned orders can have. An order matches if it has any of them.</param>
        /// <param name="releasedSince">Only orders released on or after this date and time are returned.</param>
        /// <param name="take">The maximum number of orders to return.</param>
        /// <param name="skip">The number of orders to skip before returning results. Must be nonnegative.</param>
        /// <returns>The matching orders, or <c>null</c> if none are found.</returns>
        Task<IEnumerable<OrderOverview>?> GetOrdersReleasedSince(OrderState[] orderStates, DateTime releasedSince, int take, int skip = 0);

        #endregion

        #region Order details

        /// <summary>
        /// Get a specific order by its id
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="configurationData"><c>true</c> to include configuration data; otherwise <c>false</c>.</param>
        /// <returns>The order, or <c>null</c> if it is not found.</returns>
        Task<OrderDetails?> GetOrder(Guid orderId, bool configurationData = false);

        /// <summary>
        /// Get a specific order by its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the order.</param>
        /// <param name="configurationData"><c>true</c> to include configuration data; otherwise <c>false</c>.</param>
        /// <returns>The order, or <c>null</c> if it is not found.</returns>
        Task<OrderDetails?> GetOrder(string orderNumber, bool configurationData = false);

        #endregion

        #region Order release

        /// <summary>
        /// Release order
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to release.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ReleaseOrder(Guid orderId);

        /// <summary>
        /// Reset release order
        /// </summary>
        /// <param name="orderId">The unique identifier of the order whose release is reset.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ResetReleaseOrder(Guid orderId);

        #endregion

        #region Order import

        /// <summary>
        /// Imports an order. All file references must be provided as URI.
        /// </summary>
        /// <param name="order">The order to import.</param>
        /// <returns>The response of the import request.</returns>
        Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order);

        /// <summary>
        /// Imports an order. All file references must be provided either as URIs or as referenced file.
        /// </summary>
        /// <param name="order">The order to import.</param>
        /// <param name="fileReferences">The files referenced by the order.</param>
        /// <returns>The response of the import request.</returns>
        Task<ImportOrderResponse> ImportOrderRequest(OrderDetails order, FileReference[] fileReferences);

        /// <summary>
        /// Imports a Project.zip file as an order.
        /// </summary>
        /// <param name="projectFile">The Project.zip file to import.</param>
        /// <returns>The response of the import request.</returns>
        Task<ImportOrderResponse> ImportOrderRequest(FileInfo projectFile);

        /// <summary>
        /// Get the import state of an order
        /// </summary>
        /// <param name="correlationId">The correlationId for the import job which was triggered when ImportOrderAsync was called</param>
        /// <returns>The import state, or <c>null</c> if the import job is not found.</returns>
        Task<ImportOrderStateResponse?> GetImportOrderState(Guid correlationId);

        /// <summary>
        /// Wait for the import to be completed.
        /// </summary>
        /// <param name="correlationId">The correlationId of the import job which was triggered by the import request.</param>
        /// <param name="maxDuration">The maximum time to wait for the import to be completed.</param>
        /// <returns>The imported order.</returns>
        Task<OrderDetails> WaitForImportOrderCompletion(Guid correlationId, TimeSpan maxDuration);

        #endregion

        #region Group Import

        /// <summary>
        /// Imports an group. All file references must be provided either as URIs or as referenced file.
        /// </summary>
        /// <param name="orderNumber">The order number of the order the group belongs to.</param>
        /// <param name="group">The group to add or update.</param>
        /// <param name="fileReferences">The files referenced by the group.</param>
        /// <returns>The response of the import request.</returns>
        Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, Group group, FileReference[] fileReferences);

        /// <summary>
        /// Imports a Project.zip file as an group.
        /// </summary>
        /// <param name="orderNumber">The order number of the order the group belongs to.</param>
        /// <param name="projectFile">The Project.zip file to import.</param>
        /// <returns>The response of the import request.</returns>
        Task<ImportOrderResponse> AddOrUpdateGroup(string orderNumber, FileInfo projectFile);

        #endregion

        #region Order deletion

        /// <summary>
        /// Delete orders by their order ids.
        /// </summary>
        /// <param name="orderIds">The unique identifiers of the orders to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteOrdersByOrderIds(IEnumerable<Guid> orderIds);

        /// <summary>
        /// Delete orders by their order numbers.
        /// </summary>
        /// <param name="orderNumbers">The order numbers of the orders to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteOrdersByOrderNumbers(IEnumerable<string> orderNumbers);

        /// <summary>
        /// Delete order its order id.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteOrdersByOrderId(Guid orderId);

        /// <summary>
        /// Delete order its order number.
        /// </summary>
        /// <param name="orderNumber">The order number of the order to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteOrdersByOrderNumber(string orderNumber);

        #endregion Order deletion

        #region Customer deletion
        /// <summary>
        /// Delete customers by their customer ids.
        /// </summary>
        /// <param name="customerIds">The unique identifiers of the customers to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCustomersByCustomerIds(IEnumerable<Guid> customerIds);

        /// <summary>
        /// Delete customers by their customer number.
        /// </summary>
        /// <param name="customerNumbers">The customer numbers of the customers to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCustomersByCustomerNumbers(IEnumerable<string> customerNumbers);
        #endregion

        #region Get customers

        /// <summary>
        /// Get all customers excluding additional data.
        /// </summary>
        /// <returns>All customers.</returns>
        Task<IEnumerable<Customer?>> GetCustomers();

        /// <summary>
        /// Get customers by their customer numbers including additional data.
        /// </summary>
        /// <param name="customerNumbers">The customer numbers of the customers to return.</param>
        /// <returns>The customers with the given customer numbers.</returns>
        Task<IEnumerable<Customer?>> GetCustomers(string[] customerNumbers);

        /// <summary>
        /// Get customers by their customer ids including additional data.
        /// </summary>
        /// <param name="customerIds">The unique identifiers of the customers to return.</param>
        /// <returns>The customers with the given ids.</returns>
        Task<IEnumerable<Customer?>> GetCustomers(Guid[] customerIds);

        /// <summary>
        /// Get a specific customer by its customer number including additional data.
        /// </summary>
        /// <param name="customerNumber">The customer number of the customer.</param>
        /// <returns>The customer, or <c>null</c> if it is not found.</returns>
        Task<Customer?> GetCustomer(string customerNumber);

        /// <summary>
        /// Get a specific customer by its customer id including additional data.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>The customer, or <c>null</c> if it is not found.</returns>
        Task<Customer?> GetCustomer(Guid customerId);

        #endregion

        #region Create customer

        /// <summary>
        /// Create a new customer.
        /// </summary>
        /// <param name="request">The data of the customer to create.</param>
        /// <returns>The created customer.</returns>
        Task<Customer> CreateCustomer(CreateCustomerRequest request);

        #endregion
    }
}