using HomagConnect.ProductionManager.Samples.Orders;

namespace HomagConnect.ProductionManager.Tests.Orders
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders.Import")]
    public class ImportOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task ImportOrder_ProjectZip_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            try
            {
                await ImportOrderSamples.ImportOrderUsingProjectZipAsync(productionManager);
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