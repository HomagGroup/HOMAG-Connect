using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.ProductionManager.Samples.Orders
{
    /// <summary>
    /// Sample class which shows how to get orders.
    /// </summary>
    public static class GetOrderSamples
    {
        /// <summary>
        /// Gets all the available orders for a customer.
        /// </summary>
        public static async Task GetAllOrdersAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrdersAsync();

            response.Trace();

            Assert.IsTrue(response.Any());
            foreach (var order in response)
            {
                Assert.IsFalse(string.IsNullOrEmpty(order.Name));
                Assert.AreNotEqual(Guid.Empty, order.Id);
            }
        }
    }
}
