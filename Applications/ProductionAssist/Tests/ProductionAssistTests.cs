using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionAssist.Samples;
using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionAssist.Tests;

/// <summary />
[TestClass]
[TestCategory("ProductionAssist")]
public class ProductionAssistTests : ProductionAssistTestBase
{
    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 9, 29, "DF-Production")]
    public async Task ProductionAssist_GetOrderItem_NoException()
    {
        var exceptionThrown = false;
        var productionAssist = GetProductionAssistClient();
        var productionManager = GetProductionManagerClient();

        try
        {
            var projectFile = new FileInfo(@"Orders\Project.zip");

            var request = new ImportOrderRequest
            {
                Action = ImportOrderRequestAction.ImportOnly
            };

            var response = await productionManager.ImportOrderRequest(request, projectFile);
            var orderDetails = await productionManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(1));

            var orderItemId = "WEBSEITEL";

            var orderItem = await productionAssist.GetOrderItem(orderItemId);

            Assert.IsNotNull(orderItem);
            Assert.AreEqual(orderItemId, orderItem.Barcode);

            await productionManager.DeleteOrderByOrderId(orderDetails.Id);
        }
        catch (Exception e)
        {
            e.Trace();
            exceptionThrown = true;
        }

        Assert.IsFalse(exceptionThrown);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 12, 30, "DF-Production")]
    public async Task ProductionAssist_GetWorkstations_NoException()
    {
        var exceptionThrown = false;
        var client = GetProductionAssistClient();

        try
        {
            await ProductionAssistSamples.GetWorkstations(client);
        }
        catch (Exception e)
        {
            e.Trace();
            exceptionThrown = true;
        }

        Assert.IsFalse(exceptionThrown);
    }
}