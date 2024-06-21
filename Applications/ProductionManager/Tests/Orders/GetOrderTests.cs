using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Client;
using HomagConnect.ProductionManager.Samples.Orders;

namespace HomagConnect.ProductionManager.Tests.Orders
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    [TemporaryDisabledOnServer(2024, 7, 15)]
    public class GetOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrders_NoException()
        {
            // Create new instance of the ProductionManager client:
            var productionManager = new ProductionManagerClient(SubscriptionId, AuthorizationKey, BaseUrl);
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