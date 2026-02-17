using System.ComponentModel.DataAnnotations;
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
    [TemporaryDisabledOnServer(2026, 2, 01, "DF-OrderManager")]
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders.Import")]
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

                var createdOrder = await orderManager.WaitForImportOrderCompletion(importOrderResponse.CorrelationId, TimeSpan.FromMinutes(6));

                importOrderResponse.Trace();

                await orderManager.DeleteOrdersByOrderId(createdOrder.Id);
            }
            catch (Exception e)
            {
                TestContext?.WriteLine(e.Message);
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

                var createdOrder = await orderManager.WaitForImportOrderCompletion(importOrderResponse.CorrelationId, TimeSpan.FromMinutes(6));

                importOrderResponse.Trace();

                await orderManager.DeleteOrdersByOrderId(createdOrder.Id);
            }
            catch (Exception e)
            {
                TestContext?.WriteLine(e.Message);
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

                var createdOrder = await orderManager.WaitForImportOrderCompletion(importOrderResponse.CorrelationId, TimeSpan.FromMinutes(6));

                importOrderResponse.Trace();

                await orderManager.DeleteOrdersByOrderId(createdOrder.Id);
            }
            catch (Exception e)
            {
                TestContext?.WriteLine(e.Message);
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
                var projectZip = DataExchangeSamples.GetProjectHavingTypicalProperties(false, true);

                var importOrderResponse = await orderManager.ImportOrderRequest(projectZip);

                var createdOrder = await orderManager.WaitForImportOrderCompletion(importOrderResponse.CorrelationId, TimeSpan.FromMinutes(6));

                importOrderResponse.Trace();

                await orderManager.DeleteOrdersByOrderId(createdOrder.Id);
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
        public async Task ImportOrder_ProjectZip_Exception()
        {
            var orderManager = GetOrderManagerClient();
            var anyException = false;

            try
            {
                var projectZip = DataExchangeSamples.GetProjectHavingTypicalProperties();

                var importOrderResponse = await orderManager.ImportOrderRequest(projectZip);
            }
            catch (ValidationException e)
            {
                TestContext?.WriteLine("Expected excpetion: " + e.Message);
                anyException = true;
            }

            if (BaseUrl.OriginalString == "https://connect.homag.cloud" || BaseUrl.OriginalString == "https://connect-preview.homag.cloud")
            {
                Assert.IsFalse(anyException);
            }
            else if (BaseUrl.OriginalString == "https://connect-dev.homag.cloud" || BaseUrl.OriginalString == "https://connect-int.homag.cloud")
            {
                Assert.IsTrue(anyException);
            }
        }

        /// <summary />
        [TestMethod]
        public async Task ImportOrder_Wardrobe_OrderDetails()
        {
            var stopWatch = Stopwatch.StartNew();
            var orderManager = GetOrderManagerClient();

            using var projectDirectory = DisposableTempDirectory.Create();
            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()), projectDirectory.DirectoryInfo);

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

                await orderManager.DeleteOrdersByOrderId(createdOrder.Id);
            }
        }

        /// <summary />
        [TestMethod]
        public async Task ImportOrder_Wardrobe_ProjectZip()
        {
            var orderManager = GetOrderManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            using var projectDirectory = DisposableTempDirectory.Create();
            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()), projectDirectory.DirectoryInfo);

            //project.SetSource("SmartWOP");
            project.SetOrderDate(DateTime.Today + TimeSpan.FromDays(-1));
            project.SetDeliveryDatePlanned(DateTime.Today + TimeSpan.FromDays(14));
            project.SetBarcodesToNull();

            var projectZipAdjusted = project.SaveToZipArchive(projectFiles);

            var response = await orderManager.ImportOrderRequest(projectZipAdjusted);
            var order = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(4));

            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Link);

            TestContext?.WriteLine(order.Link?.ToString());

            await orderManager.DeleteOrdersByOrderId(order.Id);
        }
    }
}