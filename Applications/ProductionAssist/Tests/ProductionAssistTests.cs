using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.Orders;

namespace HomagConnect.ProductionAssist.Tests;

/// <summary />
[TestClass]
[TestCategory("ProductionAssist")]
public class ProductionAssistTests : ProductionAssistTestBase
{
    /// <summary />
    [TestMethod]
    public async Task ProductionAssist_GetOrderItem_NoException()
    {
        // Assert.Inconclusive("Implementation not ready");

        var exceptionThrown = false;
        var productionAssist = GetProductionAssistClient();
        var productionManager = GetProductionManagerClient();

        try
        {
            var orders = await productionManager.GetOrders(OrderStatus.New, 1).ToListAsync();

            Assert.IsNotNull(orders);

            if (!orders.Any())
            {
                Assert.Inconclusive("No orders found. Please create a new order before running this test.");
            }

            //var orderItemId = "11119218"; // Get a valid order item id or barcode from orderManager
            const string orderItemId = "1e101d0c-b8f4-4f74-9ce7-4400ec0c9623"; // Get a valid order item id or barcode from orderManager

            var orderItem = await productionAssist.GetOrderItem(orderItemId);

            Assert.IsNotNull(orderItem);
            Assert.AreEqual(orderItemId, orderItem.Id);
        }
        catch (Exception e)
        {
            e.Trace();
            exceptionThrown = true;
        }

        Assert.IsFalse(exceptionThrown);
    }
}