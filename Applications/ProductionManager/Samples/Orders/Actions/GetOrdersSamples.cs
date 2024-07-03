using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.ProductionManager.Samples.Orders.Actions
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
            var response = await productionManager.GetOrders(5);

            response.Trace();

            Assert.IsTrue(response.Any());
            foreach (var order in response)
            {
                Assert.IsFalse(string.IsNullOrEmpty(order.OrderName));
                Assert.AreNotEqual(Guid.Empty, order.Id);
            }

            var orderNames = response.Select(x => x.OrderName).ToList();
            orderNames.Trace(nameof(orderNames));
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNew(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrders(OrderStatus.New,5);
            
            response.Trace();
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNewOrInProduction(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrders(new []{OrderStatus.New, OrderStatus.InProduction},5);

            response.Trace();
        }
     
    }
}
