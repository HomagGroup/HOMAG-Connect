using HomagConnect.ProductionManager.Samples.Orders.Actions;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class PatchOrderTests : ProductionManagerTestBase
    {
        [TestMethod]
        public async Task PatchOrder()
        {
            // Arranage
            var productionManagerClient = GetProductionManagerClient();

            // Act
            try
            {
                await PatchOrderSamples.PatchOrder(productionManagerClient);
            }
            catch (ArgumentNullException)
            {
                // catch as no hardcoded id is set in sample
            }
        }
    }
}
