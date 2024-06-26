using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Client;
using HomagConnect.ProductionManager.Samples.Orders.Actions;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    [TemporaryDisabledOnServer(2024, 7, 22)]
    public class GetOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrders_NoException()
        {
            // Create new instance of the ProductionManager client:
            var productionManager = new ProductionManagerClient(SubscriptionId, AuthorizationKey);
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