using System.IO.Compression;

using HomagConnect.Base.TestBase.Attributes;
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
        [TemporaryDisabledOnServer(2025, 5, 1, "DF-Production")]
        public async Task ImportOrder_ProjectZip_Cabinet()
        {
            var projectZip = new FileInfo("TestData\\Cabinet.zip");

            await ImportOrder(projectZip, "Borm");
        }

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
        [TemporaryDisabledOnServer(2025, 5, 30, "DF-Production")]
        public async Task ImportOrder_ProjectZip_Wardrobe()
        {
            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            await ImportOrder(projectZip, "SmartWOP");
        }

        private async Task ImportOrder(FileInfo projectZip, string source)
        {
            var productionManager = GetProductionManagerClient();

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()));

            project.SetSource(source);
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