using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.OrderManager.Samples.Orders.Import
{
    /// <summary>
    /// Sample class which shows how to import an order.
    /// </summary>
    public static class ImportOrderSamples
    {
        /// <summary>
        /// Sample method to import an order using order details and file references and wait for the completion of the import
        /// operation.
        /// </summary>
        /// <param name="orderManager"></param>
        /// <returns></returns>
        public static async Task ImportOrderUsingOnlyOrderDetails(IOrderManagerClient orderManager)
        {
            var request = GetOrderDetailSample();
            var response = await orderManager.ImportOrderRequest(request);
            var orderDetails = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(1));
            orderDetails.Trace();
        }

        /// <summary>
        /// Sample method to import an order using order details and file references and wait for the completion of the import
        /// operation.
        /// </summary>
        /// <param name="orderManager"></param>
        /// <returns></returns>
        public static async Task ImportOrderUsingOrderDetailsAndFileReferences(IOrderManagerClient orderManager)
        {
            var request = GetOrderDetailSample();

            request.AdditionalData =
            [
                new AdditionalDataEntity
                {
                    Name = "testImage.png",
                    Category = "Surface",
                    Type = AdditionalDataType.Image
                }
            ];

            var fileReferences = new[]
            {
                new FileReference("testImage.png", new FileInfo(@"Orders\Samples\testImage.png"))
            };

            var response = await orderManager.ImportOrderRequest(request, fileReferences);
            var orderDetails = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(1));
            orderDetails.Trace();
        }

        /// <summary>
        /// Sample method to import an order using a project zip file and wait for the completion of the import operation.
        /// </summary>
        /// <param name="orderManager"></param>
        /// <returns></returns>
        public static async Task ImportOrderUsingProjectZipAndWaitForCompletion(IOrderManagerClient orderManager)
        {
            var projectFile = new FileInfo(@"Orders\Samples\project-01.zip");
            var response = await orderManager.ImportOrderRequest(projectFile);
            var orderDetails = await orderManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(1));
            orderDetails.Trace();
        }

        /// <summary />
        private static OrderDetails GetOrderDetailSample()
        {
            var order = new OrderDetails
            {
                // Header

                OrderNumber = "736362",
                State = OrderState.New,
                OrderName = "Bedroom & bathroom 01",
                Project = "Single family house Müller John",
                PersonInCharge = "Hendrik Albers",
                OrderDescription = "Lorem ipsum dolor sit amet...",
                OrderDate = DateTime.Today,
                DeliveryDatePlanned = DateTime.Today.AddDays(14)
            };

            // Addresses
            order.Addresses = new Collection<Address>(
            [
                new()
                {
                    Name = "Max Mustermann",
                    Street = "Musterstraße",
                    HouseNumber = "1",
                    PostalCode = "12345",
                    City = "Musterstadt",
                    Country = "Deutschland",
                    Type = AddressType.Delivery,
                    AdditionalInfo = "additional info"
                }
            ]);

            // Customer

            order.Company = "Müller & Co.";
            order.CustomerNumber = "462642";

            // Details

            order.QuantityOfParts = 100;
            order.QuantityOfArticles = 10;
            order.QuantityOfPartsPlanned = 0;

            order.CreatedAt = DateTimeOffset.Now;
            order.ChangedAt = DateTimeOffset.Now;
            order.ChangedBy = "Boris Wehrle";

            // Order Items

            order.Items =
            [
                new Group
                {
                    Name = "Bedroom & bathroom 01",
                    Source = "orderConfigurator",
                    Items =
                    [
                        new Position
                        {
                            Name = "P 01.01",
                            ArticleNumber = "67839",
                            Quantity = 4,
                            Description = "Cabinet left",
                            Notes = "Lorem ipsum",
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items =
                            [
                                new Price
                                {
                                    UnitPrice = 100,
                                    TotalPrice = 4 * 100,
                                    Currency = "EUR"
                                }
                            ]
                        },
                        new Position
                        {
                            Name = "P 01.02",
                            ArticleNumber = "67840",
                            Quantity = 6,
                            Description = "Cabinet right",
                            Notes = "Lorem ipsum",
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items =
                            [
                                new Price
                                {
                                    UnitPrice = 120,
                                    TotalPrice = 6 * 120,
                                    Currency = "EUR"
                                }
                            ]
                        },
                        new Price
                        {
                            UnitPrice = 6 * 120 + 4 * 100,
                            TotalPrice = 6 * 120 + 4 * 100,
                            Currency = "EUR"
                        }
                    ]
                },
                new Group
                {
                    Name = "Bedroom & bathroom 01",
                    Source = "orderConfigurator",
                    Items =
                    [
                        new Position
                        {
                            Name = "P 01.01",
                            ArticleNumber = "67839",
                            Quantity = 4,
                            Description = "Cabinet left",
                            Notes = "Lorem ipsum",
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items =
                            [
                                new Price
                                {
                                    UnitPrice = 100,
                                    TotalPrice = 4 * 100,
                                    Currency = "EUR"
                                }
                            ]
                        },
                        new Position
                        {
                            Name = "P 01.02",
                            ArticleNumber = "67840",
                            Quantity = 6,
                            Description = "Cabinet right",
                            Notes = "Lorem ipsum",
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items =
                            [
                                new Price
                                {
                                    UnitPrice = 120,
                                    TotalPrice = 6 * 120,
                                    Currency = "EUR"
                                }
                            ]
                        },
                        new Price
                        {
                            UnitPrice = 6 * 120 + 4 * 100,
                            TotalPrice = 6 * 120 + 4 * 100,
                            Currency = "EUR"
                        }
                    ]
                },
                new Price
                {
                    UnitPrice = 2000,
                    TotalPrice = 2000,
                    Currency = "EUR"
                }
            ];

            return order;
        }
    }
}