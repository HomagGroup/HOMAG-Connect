using HomagConnect.ProductionManager.Samples.Orders;

namespace HomagConnect.ProductionManager.Tests.Orders
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class GetOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrders_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersAsync(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
        }
    }
}