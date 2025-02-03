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
                            Length = 250,
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
                            Length = 250,
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
                            Length = 250,
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
                            Length = 250,
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

            order.Trace(nameof(order));
        }

        [TestMethod]
        public void GetOrderWithConfig()
        {
            var order = new Order();

            // Header

            order.OrderNumber = "736362";
            order.State = OrderState.New;
            order.OrderName = "Bedroom & bathroom 01";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Joe";
            order.OrderDescription = "Lorem ipsum dolor sit amet...";
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

            order.CreatedAt = DateTimeOffset.Now;
            order.ChangedAt = DateTimeOffset.Now;
            order.ChangedBy = "Selfish";

            // Order Items

            order.Items = new()
            {
                new Group()
                {
                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                    Items = new()
                    {
                        new ConfigurationPosition
                        {
                            Id = "190d9d40-9095-40b0-a7ce-2b85e26b9485",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                { "mod_Depth", 548 },
                                { "mod_Height", 900 },
                                { "mod_Width", 600 },
                                { "color", "white" }
                            },
                            Items = new()
                            {
                                new Configuration
                                {
                                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 705 }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));
        }

        [TestMethod]
        public void GetOrderWithConfig2()
        {
            var order = new Order();

            // Header

            order.OrderNumber = "736362";
            order.State = OrderState.New;
            order.OrderName = "Bedroom & bathroom 01";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Joe";
            order.OrderDescription = "Lorem ipsum dolor sit amet...";
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

            order.CreatedAt = DateTimeOffset.Now;
            order.ChangedAt = DateTimeOffset.Now;
            order.ChangedBy = "Selfish";

            // Order Items

            order.Items = new()
            {
                new Group()
                {
                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                    Notes = "This is an article group (PosGroup)",
                    Items = new()
                    {
                        new ConfigurationPosition
                        {
                            Id = "190d9d40-9095-40b0-a7ce-2b85e26b9485",
                            ArticleNumber = "LW_H1000DT",
                            Notes = "This is a ROOT module",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Position = new double[] { 0, 0, 0 },
                            Rotation = new double[] { 0, 0, 0 },
                            Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                { "mod_Depth", 548 },
                                { "mod_Height", 2000 },
                                { "mod_Width", 1000 },
                                { "mod_TypeElement", "TallUnit" }
                            },
                            Items = new()
                            {
                                new Configuration
                                {
                                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                                    Notes = "This is a SUB module",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 800 }
                                    }
                                },
                                new Configuration
                                {
                                    Id = "61029b65-730a-47de-9c5a-2cbc4cce3fc4",
                                    Notes = "This is a 2nd SUB module",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 9999 }
                                    }
                                }
                            }
                        },
                        new ConfigurationPosition
                        {
                            Id = "47F74A7F-999C-44C2-AF5B-709F0D25B5EA",
                            ArticleNumber = "LW_H1000DT",
                            Notes = "This is a 2nd ROOT module",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Position = new double[] { 1000, 0, 0 },
                            Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                { "mod_Depth", 548 },
                                { "mod_Height", 2000 },
                                { "mod_Width", 1000 },
                                { "mod_TypeElement", "TallUnit" }
                            },
                            Items = new()
                            {
                                new Configuration
                                {
                                    Id = "810BE6A4-17A2-47C1-A4C7-392ECE5FF584",
                                    Notes = "This is a SUB module",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 800 }
                                    }
                                },
                                new Configuration
                                {
                                    Id = "1CCF8E5C-556A-4356-B254-7C597CC98538",
                                    Notes = "This is a 2nd SUB module",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 9999 }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));
        }

        [TestMethod]
        public void GetOrderWithConfigAndParts()
        {
            var order = new Order();

            // Header

            order.OrderNumber = "736362";
            order.State = OrderState.New;
            order.OrderName = "Bedroom & bathroom 01";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Joe";
            order.OrderDescription = "Lorem ipsum dolor sit amet...";
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

            order.CreatedAt = DateTimeOffset.Now;
            order.ChangedAt = DateTimeOffset.Now;
            order.ChangedBy = "Selfish";

            // Order Items

            order.Items = new()
            {
                new Group()
                {
                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                    Items = new()
                    {
                        new ConfigurationPosition
                        {
                            Id = "190d9d40-9095-40b0-a7ce-2b85e26b9485",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                { "mod_Depth", 548 },
                                { "mod_Height", 900 },
                                { "mod_Width", 600 },
                                { "color", "white" }
                            },
                            Items = new()
                            {
                                new Part
                                {
                                    Id = "P 01.01",
                                    Quantity = 4,
                                    Description = "Cabinet left",
                                    Notes = "Lorem ipsum",
                                    Length = 250,
                                    Width = 100,
                                    Thickness = 150
                                },
                                new Configuration
                                {
                                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                                    ModuleId = "mf_Door",
                                    Attributes = new Dictionary<string, object>(StringComparer.Ordinal)
                                    {
                                        { "mod_FrontHeight", 705 }
                                    },
                                    Items = new()
                                    {
                                        new Part
                                        {
                                            Id = "P 01.02",
                                            Quantity = 6,
                                            Description = "Front panel",
                                            Notes = "Lorem ipsum",
                                            Length = 250,
                                            Width = 100,
                                            Thickness = 16,
                                            Items = new()
                                            {
                                                new Resource
                                                {
                                                    Id = "P 01.02.01",
                                                    Quantity = 1,
                                                    Description = "Hinge",
                                                    Notes = "Lorem ipsum",
                                                    ArticleNumber = "67840.01"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));
        }
    }
}