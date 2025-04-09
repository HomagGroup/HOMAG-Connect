using System.IO.Compression;

using HomagConnect.DataExchange.Extensions;
using HomagConnect.ProductionManager.Contracts.Import;
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

        /// <summary />
        [TestMethod]
        public async Task ImportOrder_ProjectZip_Wardrobe()
        {
            var productionManager = GetProductionManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()));

            project.SetSource("SmartWOP");
            project.SetOrderDate(DateTime.Today + TimeSpan.FromDays(-1));
            project.SetDeliveryDatePlanned(DateTime.Today + TimeSpan.FromDays(14));
            project.SetBarcodesToNull();

            var projectZipAdjusted = project.SaveToZipArchive(projectFiles);

            var response = await productionManager.ImportOrderRequest(new ImportOrderRequest(), projectZipAdjusted);
            var order = await productionManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(3));

            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Link);

            TestContext?.WriteLine(order.Link?.ToString());
        }
    }
}