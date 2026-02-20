using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.OrderManager.Samples.Orders.Actions
{
    /// <summary>
    /// Sample class which shows how to get orders.
    /// </summary>
    public static class GetOrderSamples
    {
        /// <summary>
        /// Gets all the available orders for a customer.
        /// </summary>
        public static async Task GetAllOrdersAsync(IOrderManagerClient orderManager)
        {
            var response = await orderManager.GetOrders(5).ToListAsync();

            response.Trace();

            var orderNames = response.Select(x => x.OrderName).ToList();

            orderNames.Trace(nameof(orderNames));
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNew(IOrderManagerClient orderManager)
        {
            var response = await orderManager.GetOrders(OrderState.New, 5);

            response.Trace();
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNewOrInProgress(IOrderManagerClient orderManager)
        {
            var response = await orderManager.GetOrders([OrderState.New, OrderState.InProgress], 5);

            response.Trace();
        }

        /// <summary />
        public static async Task GetOrdersHavingThePassedOrderNumbers(IOrderManagerClient orderManager)
        {
            var response = await orderManager.GetOrders(["10000001"]);

            response.Trace();
        }

        /// <summary />
        public static OrderDetails GetSampleOrder()
        {
            var order = new OrderDetails();

            // Header

            order.OrderNumber = "736362";
            order.State = OrderState.New;
            order.OrderName = "Bedroom & bathroom 01b";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Hendrik Albers";
            order.OrderDescription =
                "Lorem ipsum dolor sit amet...";
            order.OrderDate = DateTime.Today;
            order.DeliveryDatePlanned = DateTime.Today.AddDays(14);

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
                new Group
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
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items = new Collection<Contracts.OrderItems.Base>
                            {
                                new Price
                                {
                                    UnitPrice = 100,
                                    TotalPrice = 4 * 100,
                                    Currency = "EUR"
                                }
                            }
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
                            Items = new Collection<Contracts.OrderItems.Base>
                            {
                                new Price
                                {
                                    UnitPrice = 120,
                                    TotalPrice = 6 * 120,
                                    Currency = "EUR"
                                }
                            }
                        },
                        new Price
                        {
                            UnitPrice = 6 * 120 + 4 * 100,
                            TotalPrice = 6 * 120 + 4 * 100,
                            Currency = "EUR"
                        }
                    }
                },
                new Group
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
                            Depth = 250,
                            Width = 100,
                            Height = 150,
                            Items = new Collection<Contracts.OrderItems.Base>
                            {
                                new Price
                                {
                                    UnitPrice = 100,
                                    TotalPrice = 4 * 100,
                                    Currency = "EUR"
                                }
                            }
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
                            Items = new Collection<Contracts.OrderItems.Base>
                            {
                                new Price
                                {
                                    UnitPrice = 120,
                                    TotalPrice = 6 * 120,
                                    Currency = "EUR"
                                }
                            }
                        },
                        new Price
                        {
                            UnitPrice = 6 * 120 + 4 * 100,
                            TotalPrice = 6 * 120 + 4 * 100,
                            Currency = "EUR"
                        }
                    }
                },
                new Price
                {
                    UnitPrice = 2000,
                    TotalPrice = 2000,
                    Currency = "EUR"
                }
            };

            return order;
        }
    }
}