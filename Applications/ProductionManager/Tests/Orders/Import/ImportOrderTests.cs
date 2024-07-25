using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Samples.Orders.Import;

namespace HomagConnect.ProductionManager.Tests.Orders.Import
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
                await ImportOrderSamples.ImportOrderUsingProjectZipAndWaitForCompletion(productionManager);
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