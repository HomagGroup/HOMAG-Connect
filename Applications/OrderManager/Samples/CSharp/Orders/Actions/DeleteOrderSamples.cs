using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Samples.Orders.Actions
{
    /// <summary>
    /// Delete order samples
    /// </summary>
    public static class DeleteOrderSamples
    {
        /// <summary>
        /// Delete order by order id
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteOrdersByOrderId(IOrderManagerClient orderManager)
        {
            var orderId = Guid.Parse("PUT_ORDER_ID_HERE");

            await orderManager.DeleteOrdersByOrderId(orderId);
        }

        /// <summary>
        /// Delete orders by order ids
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteOrdersByOrderIds(IOrderManagerClient orderManager)
        {
            var orderIds = new[]
            {
                Guid.Parse("PUT_ORDER_ID1_HERE"),
                Guid.Parse("PUT_ORDER_ID2_HERE")
            };

            await orderManager.DeleteOrdersByOrderIds(orderIds);
        }

        /// <summary>
        /// Delete order by order number
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteOrdersByOrderNumber(IOrderManagerClient orderManager)
        {
            var orderNumber = "10000001"; // set existing order number

            await orderManager.DeleteOrdersByOrderNumber(orderNumber);
        }

        /// <summary>
        /// Delete orders by order numbers
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteOrdersByOrderNumbers(IOrderManagerClient orderManager)
        {
            var orderNumbers = new[] { "10000001", "10000002" };

            await orderManager.DeleteOrdersByOrderNumbers(orderNumbers);
        }
    }
}
