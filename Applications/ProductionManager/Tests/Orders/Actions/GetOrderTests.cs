using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Samples.Orders.Actions;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    [TemporaryDisabledOnServer(2024, 9, 1)]
    public class GetOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrders_NoException()
        {
            // Create new instance of the ProductionManager client:
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

        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrdersHavingStatusNew_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNew(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
        }

        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrdersHavingStatusNewOrInProduction_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNewOrInProduction(productionManager);
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