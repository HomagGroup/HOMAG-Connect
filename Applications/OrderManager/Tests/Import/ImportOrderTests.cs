using System.Collections.ObjectModel;
using System.IO.Compression;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.DataExchange.Extensions;
using HomagConnect.DataExchange.Samples;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.OrderManager.Samples.Orders.Actions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Uri = System.Uri;

namespace HomagConnect.OrderManager.Tests.Import
{
    /// <summary />
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders.Import")]
    [TemporaryDisabledOnServer(2025, 05, 1, "DF-Production")]
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
        public async Task ImportOrder_OrderDetails_Wardrobe()
        {
            var orderManager = GetOrderManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var (project, projectFiles) = ProjectPersistenceManager.Load(new ZipArchive(projectZip.OpenRead()));

            foreach (var (order, fileReferences) in project.ConvertToOrders(projectFiles))
            {
                var importOrderResponse = await orderManager.ImportOrderRequest(order, fileReferences);

                await EnsureImportOrderSucceeded(orderManager, importOrderResponse);
            }
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

                order.AdditionalData = new Collection<AdditionalDataEntity>
                {
                    new AdditionalDataImage
                    {
                        Category = "Image",
                        DownloadUri = new Uri(reference, UriKind.Relative),
                        Name = "Cabinet"
                    }
                };

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
        public async Task ImportOrder_ProjectZip_Wardrobe()
        {
            var orderManager = GetOrderManagerClient();

            var projectZip = new FileInfo("TestData\\Wardrobe.zip");

            var importOrderRequest = await orderManager.ImportOrderRequest(projectZip);

            await EnsureImportOrderSucceeded(orderManager, importOrderRequest);
        }

        private static async Task EnsureImportOrderSucceeded(IOrderManagerClient orderManager, ImportOrderResponse importOrderRequest)
        {
            var timeout = DateTime.UtcNow.AddMinutes(3);

            while (true)
            {
                if (DateTime.UtcNow > timeout)
                {
                    Assert.Fail("Timeout");
                }

                var importOrderState = await orderManager.GetImportOrderState(importOrderRequest.CorrelationId);

                if (importOrderState.State is ImportState.InProgress or ImportState.Queued)
                {
                    await Task.Delay(1000);
                }
                else if (importOrderState.State is ImportState.Succeeded)
                {
                    importOrderState.Trace();
                    break;
                }
                else if (importOrderState.State is ImportState.Error)
                {
                    importOrderState.Trace();
                    Assert.Fail("Import failed");
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(importOrderState.State), importOrderState.State, @"Unknown state");
                }
            }
        }
    }
}