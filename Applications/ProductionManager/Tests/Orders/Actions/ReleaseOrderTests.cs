using HomagConnect.ProductionManager.Samples.Orders.Actions;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class ReleaseOrderTests : ProductionManagerTestBase
    {
        [TestMethod]
        public async Task ReleaseOrder()
        {
            // Arranage
            var productionManagerClient = GetProductionManagerClient();

            // Act
            try
            {
                await ReleaseOrderSamples.ReleaseOrder(productionManagerClient);
            }
            catch (ArgumentNullException)
            {
                // catch as no hardcoded id is set in sample
            }
        }

        [TestMethod]
        public async Task ResetReleaseOrder()
        {
            // Arranage
            var productionManagerClient = GetProductionManagerClient();

            // Act
            try
            {
                await ReleaseOrderSamples.ResetReleaseOrder(productionManagerClient);
            }
            catch (ArgumentNullException)
            {
                // catch as no hardcoded id is set in sample
            }
        }
    }
}
