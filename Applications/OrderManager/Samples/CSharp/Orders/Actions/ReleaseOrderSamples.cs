using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Samples.Orders.Actions
{
    /// <summary>
    /// Release order samples
    /// </summary>
    public static class ReleaseOrderSamples
    {
        /// <summary>
        /// Release order
        /// </summary>
        /// <param name="orderManagerClient"></param>
        /// <returns></returns>
        public static async Task ReleaseOrder(IOrderManagerClient orderManagerClient)
        {
            Guid? orderId = null; // set existing order id
            if (orderId == null)
            {
                throw new ArgumentNullException("No order id set");
            }

            await orderManagerClient.ReleaseOrder(orderId.Value);
        }

        /// <summary>
        /// Reset release order
        /// </summary>
        /// <param name="orderManagerClient"></param>
        /// <returns></returns>
        public static async Task ResetReleaseOrder(IOrderManagerClient orderManagerClient)
        {
            Guid? orderId = null; // set existing order id
            if (orderId == null)
            {
                throw new ArgumentNullException("No order id set");
            }

            await orderManagerClient.ResetReleaseOrder(orderId.Value);
        }
    }
}
