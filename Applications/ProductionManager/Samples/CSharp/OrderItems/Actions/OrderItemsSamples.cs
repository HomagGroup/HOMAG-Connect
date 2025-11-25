using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.OrderItems.Actions
{
    /// <summary>
    /// Sample class which shows how to get order items.
    /// </summary>
    public static class OrderItemsSamples
    {
        /// <summary />
        public static async Task GetOrderItem(IProductionManagerClient productionManager, string[] identifiers)
        {
            var response = await productionManager.GetOrderItems(identifiers);
            response.Trace();
        }
    }
}
