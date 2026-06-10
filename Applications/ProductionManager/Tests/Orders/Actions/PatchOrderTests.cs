using HomagConnect.Base.Contracts;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Samples.Orders.Actions;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class PatchOrderTests : ProductionManagerTestBase
    {
        [TestMethod]
        [TemporaryDisabledOnServer(2026, 07, 31, "DF-Production")] // Disable till it reaches production, as the API is not yet available on the server.
        public async Task PatchOrder()
        {
            // Arranage
            var productionManagerClient = GetProductionManagerClient();

            // Act
            var orders = await productionManagerClient.GetOrders(OrderStatus.New, 5);

            if (!orders.Any())
            {
                Assert.Inconclusive("No orders with status 'New' found. Please create an order with status 'New' to run this test.");
                return;
            }

            var orderIdentifier = orders.First().OrderNumber;
            var patchData = PatchBuilder<OrderDetails>.For()
            .Set(o => o.CustomerName, "Muster GmbH")
            .Set(o => o.DeliveryDatePlanned, DateTime.Parse("2026-09-15T00:00:00Z", CultureInfo.CurrentCulture))
            .Set(o => o.Email, null)
            .Set(o => o.Address, new Address
            {
                City = "Test City",
                HouseNumber = "123",
            })
            .Build();

            var jPatchData = JObject.FromObject(patchData);
            await productionManagerClient.PatchOrder(orderIdentifier!, jPatchData);

            var updatedOrder = await productionManagerClient.GetOrder(orderIdentifier!);
            Assert.AreEqual("Muster GmbH", updatedOrder.CustomerName);
            Assert.AreEqual("Test City", updatedOrder.Address!.City);
        }
    }
}
