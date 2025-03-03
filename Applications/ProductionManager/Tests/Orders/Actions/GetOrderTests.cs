using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Samples.Orders.Actions;
using System.IO.Compression;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class GetOrderTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Orders_GetAllOrders_NoException()
        {
            // Create new instance of the ProductionManager client:
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersAsync(productionManager);
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
        public async Task Orders_GetAllOrdersHavingStatusNew_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNew(productionManager);
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
        public async Task Orders_GetAllOrdersHavingStatusNewOrInProduction_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNewOrInProduction(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
        }

        private static async Task<string> ImportOrderWithOrderNumber(IProductionManagerClient productionManagerClient)
        {
            var projectDirectory = new DirectoryInfo(@"Orders\Samples\OrderWithExternalSystemId");

            var projectXml = new FileInfo(Path.Combine(projectDirectory.FullName, "Project.xml"));
            Assert.IsTrue(projectXml.Exists, $"File '{projectXml.FullName}' not found.");

            var projectFile = new FileInfo(Path.GetTempFileName());

            if (projectFile.Exists)
            {
                projectFile.Delete();
            }

            ZipFile.CreateFromDirectory(projectDirectory.FullName, projectFile.FullName);

            projectFile.Refresh();
            Assert.IsTrue(projectFile.Exists, $"File '{projectFile.FullName}' not found.");

            var request = new ImportOrderRequest
            {
                Action = ImportOrderRequestAction.ImportOnly
            };

            var response = await productionManagerClient.ImportOrderRequest(request, projectFile);

            var order = await productionManagerClient.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromSeconds(30));

            Assert.IsNotNull(order);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(order.OrderNumber));

            return order.OrderNumber;
        }

        /// <summary />
        [TestMethod]
        public async Task Orders_GetCompletionDatesPlanned_NoException()
        {
            var orderNumbers = new List<string>();
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            foreach (var order in await productionManager.GetOrders(1000))
            {
                if (order.OrderNumber != null)
                {
                    orderNumbers.Add(order.OrderNumber);
                }
            }

            if (orderNumbers.Count == 0)
            {
                orderNumbers.Add(await ImportOrderWithOrderNumber(productionManager));
            }

            try
            {
                await GetOrderSamples.GetCompletionDatesPlanned(productionManager, [.. orderNumbers]);
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
        public async Task Orders_GetOrder_NoException()
        {
            // Create new instance of the ProductionManager client:
            var productionManager = GetProductionManagerClient();

            var anyException = false;

            try
            {
                var order = await productionManager.GetOrders(1).FirstOrDefaultAsync();

                if (order == null)
                {
                    Assert.Inconclusive("There is no order.");
                }
                
                var order2 =  await GetOrderSamples.GetOrder(productionManager, order.Id);

                Assert.AreEqual(order.Id, order2.Id);

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