using HomagConnect.OrderManager.Samples.Orders.Actions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.OrderManager.Tests.Orders.Actions
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders")]
    public class ReleaseOrderTests : OrderManagerTestBase
    {
        [TestMethod]
        public async Task ReleaseOrder()
        {
            // Arrange
            var orderManagerClient = GetOrderManagerClient();

            // Act
            try
            {
                await ReleaseOrderSamples.ReleaseOrder(orderManagerClient);
            }
            catch (ArgumentNullException)
            {
                // catch as no hardcoded id is set in sample
            }
        }

        [TestMethod]
        public async Task ResetReleaseOrder()
        {
            // Arrange
            var orderManagerClient = GetOrderManagerClient();

            // Act
            try
            {
                await ReleaseOrderSamples.ResetReleaseOrder(orderManagerClient);
            }
            catch (ArgumentNullException)
            {
                // catch as no hardcoded id is set in sample
            }
        }
    }
}
