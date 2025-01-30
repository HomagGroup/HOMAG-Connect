using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.OrderItems;

namespace HomagConnect.OrderManager.Tests
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders")]
    public sealed class GetOrderTests
    {
        [TestMethod]
        public void GetOrder()
        {
            var order = new Order();

            // Header

            order.OrderNumber = "736362";
            order.State = OrderState.New;
            order.OrderName = "Bedroom & bathroom 01";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Hendrik Albers";
            order.OrderDescription =
                "Lorem ipsum dolor sit amet...";
            order.OrderDate = DateTime.Today;
            order.DeliveryDatePlanned = DateTime.Today.AddDays(14);

            order.Link = new Uri($"https://orderManager.homag.cloud/#/subscriptionId/orders/{order.OrderNumber}");

            // Addresses
            order.Addresses = new Collection<Address>(new List<Address>
            {
                new()
                {
                    Street = "Musterstraße",
                    HouseNumber = "1",
                    PostalCode = "12345",
                    City = "Musterstadt",
                    Country = "Deutschland",
                    Type = AddressType.Delivery | AddressType.Billing
                }
            });

            // Customer

            order.CustomerName = "Müller & Co.";
            order.CustomerNumber = "462642";

            // Details

            order.QuantityOfParts = 100;
            order.QuantityOfArticles = 10;
            order.QuantityOfPartsPlanned = 0;
            //order.TotalPrice = 1000;

            order.CreatedAt = DateTimeOffset.Now;
            order.ChangedAt = DateTimeOffset.Now;
            order.ChangedBy = "Boris Wehrle";

            // Order Items

            order.Items = new()
            {
                new Group()
                {
                    Name = "Bedroom & bathroom 01",
                    Source = "orderConfigurator",

                    Items = new()
                    {
                        new Position
                        {
                            Name = "P 01.01",
                            ArticleNumber = "67839",
                            Quantity = 4,
                            Description = "Cabinet left",
                            Notes = "Lorem ipsum",
                            Length = 250,
                            Width = 100,
                            Height = 150
                        },
                        new Position
                        {
                            Name = "P 01.02",
                            ArticleNumber = "67840",
                            Quantity = 6,
                            Description = "Cabinet right",
                            Notes = "Lorem ipsum",
                            Length = 250,
                            Width = 100,
                            Height = 150
                        }
                    }
                }
            };

            order.Trace(nameof(order));
        }
    }
}