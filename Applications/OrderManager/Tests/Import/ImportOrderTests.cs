using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.DataExchange.Samples;
using HomagConnect.OrderManager.Samples;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Uri = System.Uri;

namespace HomagConnect.OrderManager.Tests.Import
{
    /// <summary />
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders.Import")]
    [TemporaryDisabledOnServer(2025, 04, 1, "DF-Production")]
    public class ImportOrderTests : OrderManagerTestBase
    {
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
    }
}