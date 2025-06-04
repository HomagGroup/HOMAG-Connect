using System.Diagnostics;
using System.IO.Compression;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.DataExchange.Extensions;
using HomagConnect.DataExchange.Samples;
using HomagConnect.OrderManager.Samples.Orders.Actions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Uri = System.Uri;

namespace HomagConnect.OrderManager.Tests.Import
{
    /// <summary />
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders.Import")]
    [TemporaryDisabledOnServer(2025, 05, 10, "DF-Production")]
    public class ImportOrderTests : OrderManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task ImportOrder_Order_NoException()
        {
            var orderManager = GetOrderManagerClient();
            var anyException = false;

            try
            {
                var order = GetOrderSamples.GetSampleOrder();

                var importOrderResponse = await orderManager.ImportOrderRequest(order);

                importOrderResponse.Trace();
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
        public async Task ImportOrder_OrderWithReferencedFiles_NoException()
        {
            var orderManager = GetOrderManagerClient();
            var anyException = false;

            try
            {
                var order = GetOrderSamples.GetSampleOrder();
                var referencedFiles = new List<FileReference>();

                var reference = "images/Cabinet.png";

                order.AdditionalData =
                [
                    new AdditionalDataImage
                    {
                        Category = "Image",
                        DownloadUri = new Uri(reference, UriKind.Relative),
                        Name = "Cabinet"
                    }
                ];

                referencedFiles.Add(new FileReference(reference, new FileInfo("TestData\\Cabinet.png")));

                var importOrderResponse = await orderManager.ImportOrderRequest(order, referencedFiles.ToArray());

                importOrderResponse.Trace();
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
        public async Task ImportOrder_ProjectZip_LargeProject()
        {
            var orderManager = GetOrderManagerClient();
            var anyException = false;

            try
            {
                var projectZip = new FileInfo("TestData\\Kitchen.zip");

                var importOrderResponse = await orderManager.ImportOrderRequest(projectZip);

                importOrderResponse.Trace();
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
        public async Task ImportOrder_ProjectZip_NoException()
        {
            var orderManager = GetOrderManagerClient();
            var anyException = false;

            try
            {
                var projectZip = DataExchangeSamples.GetProjectHavingTypicalProperties();

                var importOrderResponse = await orderManager.ImportOrderRequest(projectZip);

                importOrderResponse.Trace();
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
        public async Task ImportOrder_Wardrobe_OrderDetails()
        {
            var stopWatch = Stopwatch.StartNew();
            var orderManager = GetOrderManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()));

            project.SetSource("SmartWOP");
            project.SetOrderDate(DateTime.Today + TimeSpan.FromDays(-7));
            project.SetDeliveryDatePlanned(DateTime.Today + TimeSpan.FromDays(14));
            project.SetBarcodesToNull();

            foreach (var (orderDetails, fileReferences) in project.ConvertToOrders(projectFiles))
            {
                TestContext?.WriteLine($"Stopwatch: ConvertToOrders {stopWatch.Elapsed}");
                orderDetails.PersonInCharge = Environment.UserName;

                TestContext?.AddResultFile(orderDetails.TraceToFile("Request").FullName);

                var response = await orderManager.ImportOrderRequest(orderDetails, fileReferences);
                TestContext?.WriteLine($"Stopwatch: {nameof(orderManager.ImportOrderRequest)} {stopWatch.Elapsed}");

                var createdOrder = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(6));
                TestContext?.WriteLine($"Stopwatch: {nameof(orderManager.WaitForImportOrderCompletion)} {stopWatch.Elapsed}");

                Assert.IsNotNull(createdOrder);

                TestContext?.AddResultFile(createdOrder.TraceToFile("Result").FullName);

                TestContext?.WriteLine($"Link: {createdOrder.Link}");
            }
        }

        /// <summary />
        [TestMethod]
        public async Task ImportOrder_Wardrobe_ProjectZip()
        {
            var orderManager = GetOrderManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()));

            project.SetSource("SmartWOP");
            project.SetOrderDate(DateTime.Today + TimeSpan.FromDays(-1));
            project.SetDeliveryDatePlanned(DateTime.Today + TimeSpan.FromDays(14));
            project.SetBarcodesToNull();

            var projectZipAdjusted = project.SaveToZipArchive(projectFiles);

            var response = await orderManager.ImportOrderRequest(projectZipAdjusted);
            var order = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(4));

            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Link);

            TestContext?.WriteLine(order.Link?.ToString());
        }
    }
}