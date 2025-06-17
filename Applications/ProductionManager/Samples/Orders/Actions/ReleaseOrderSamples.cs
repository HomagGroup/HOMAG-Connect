using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.Orders.Actions
{
    /// <summary>
    /// Release order samples
    /// </summary>
    public static class ReleaseOrderSamples
    {
        /// <summary>
        /// Release order
        /// </summary>
        /// <param name="productionManagerClient"></param>
        /// <returns></returns>
        public static async Task ReleaseOrder(IProductionManagerClient productionManagerClient)
        {
            Guid? orderId = null; // set existing order id
            if (orderId == null)
            {
                throw new ArgumentNullException("No order id set");
            }

            await productionManagerClient.ReleaseOrder(orderId.Value);
        }

        /// <summary>
        /// Reset release order
        /// </summary>
        /// <param name="productionManagerClient"></param>
        /// <returns></returns>
        public static async Task ResetReleaseOrder(IProductionManagerClient productionManagerClient)
        {
            Guid? orderId = null; // set existing order id
            if (orderId == null)
            {
                throw new ArgumentNullException("No order id set");
            }

            await productionManagerClient.ResetReleaseOrder(orderId.Value);
        }
    }
}
