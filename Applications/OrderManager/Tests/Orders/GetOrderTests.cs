using System.Collections.ObjectModel;

using FluentAssertions;

using HomagConnect.Base;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;
using HomagConnect.OrderManager.Samples.Orders.Actions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Tests.Orders
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders")]
    public sealed class GetOrderTests : OrderManagerTestBase
    {
        [TestMethod]
        public void GetOrderWithConfigAndParts()
        {
            var order = new OrderDetails();

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
                    Name = "Test",
                    Source = "OrderConfigurator",
                    Items = new()
                    {
                        new ConfigurationPosition
                        {
                            Id = "190d9d40-9095-40b0-a7ce-2b85e26b9485",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Attributes = new()
                            {
                                new ConfigurationAttribute("mod_Depth", 548),
                                new ConfigurationAttribute("mod_Height", 900),
                                new ConfigurationAttribute("mod_Width", 600),
                                new ConfigurationAttribute("color", "white")
                            },
                            ArticleName = "Cabinet",
                            ArticleNumber = "CAB-01",
                            Color = "white",
                            Catalog = "CabinetCatalog",
                            CarcaseColor = "white",
                            DoorDirection = "left",
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
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_FrontHeight", 705)
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
                                },
                                new ErrorInfo
                                {
                                    Category = "Error",
                                    Text = "Error message",
                                },
                                new ErrorInfo
                                {
                                    Category = "Warning",
                                    Text = "Warning message",
                                },
                                new ErrorInfo
                                {
                                    Text = "Error message",
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));

            // Try to serialize and deserialize it
            var json = JsonConvert.SerializeObject(order, SerializerSettings.Default);
            var o = JsonConvert.DeserializeObject<OrderDetails>(json, SerializerSettings.Default);
            o.Should().BeEquivalentTo(order);
        }

        /// <summary />
        [TestMethod]
        [TemporaryDisabledOnServer(2025, 08, 1, "DF-Production")]
        public async Task Orders_GetAllOrders_NoException()
        {
            var orderManager = GetOrderManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersAsync(orderManager);
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
        [TemporaryDisabledOnServer(2025, 08, 1, "DF-Production")]
        public async Task Orders_GetAllOrdersHavingStatusNew_NoException()
        {
            var orderManager = GetOrderManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNew(orderManager);
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
        [TemporaryDisabledOnServer(2025, 08, 1, "DF-Production")]
        public async Task Orders_GetAllOrdersHavingStatusNewOrInProduction_NoException()
        {
            var orderManager = GetOrderManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetAllOrdersHavingStatusNewOrInProduction(orderManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2025, 08, 1, "DF-Production")]
        public async Task Orders_GetOrdersHavingThePassedOrderNumbers_NoException()
        {
            var orderManager = GetOrderManagerClient();

            var anyException = false;

            try
            {
                await GetOrderSamples.GetOrdersHavingThePassedOrderNumbers(orderManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
        }

        [TestMethod]
        public void Orders_GetSampleOrder()
        {
            var order = GetOrderSamples.GetSampleOrder();

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void Orders_GetSampleOrderWithConfig()
        {
            var order = new OrderDetails();

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
                    Name = "Test",
                    Source = "OrderConfigurator",
                    Items = new()
                    {
                        new ConfigurationPosition
                        {
                            Id = "190d9d40-9095-40b0-a7ce-2b85e26b9485",
                            LibraryId = "CabinetLibrary",
                            ModuleId = "mr_StorageunitSingle",
                            Attributes = new()
                            {
                                new ConfigurationAttribute("mod_Depth", 548),
                                new ConfigurationAttribute("mod_Height", 900),
                                new ConfigurationAttribute("mod_Width", 600),
                                new ConfigurationAttribute("color", "white")
                            },
                            Items = new()
                            {
                                new Configuration
                                {
                                    Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                                    ModuleId = "mf_Door",
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_FrontHeight", 705)
                                    }
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));

            var json = JsonConvert.SerializeObject(order, SerializerSettings.Default);
            var o = JsonConvert.DeserializeObject<OrderDetails>(json, SerializerSettings.Default);
            o.Should().BeEquivalentTo(order);
        }

        [TestMethod]
        public void Orders_GetSampleOrderWithConfig2()
        {
            var order = new OrderDetails();

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
                    Notes = "This is a room (ONE roomle planner id)",
                    Name = "Test",
                    Source = "OrderConfigurator",
                    AdditionalProperties = new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { "roomlePlannerId", "ps_4ejnf8ese0jwgmtan2ltzki0io473a8" },
                    },
                    AdditionalData = new()
                    {
                        new AdditionalDataImage
                        {
                            Category = "Room plan",
                            DownloadUri = new Uri("https://www.roomle.com/planner/room/ps_4ejnf8ese0jwgmtan2ltzki0io473a8.jpg")
                        }
                    },
                    Items = new()
                    {
                        new ConfigurationGroup
                        {
                            Id = "0F18CBC6-52C9-4885-BCA6-BCF95F700525",
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
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_Depth", 548),
                                        new ConfigurationAttribute("mod_Height", 2000),
                                        new ConfigurationAttribute("mod_Width", 1000),
                                        new ConfigurationAttribute("mod_TypeElement", "TallUnit")
                                    },
                                    Items = new()
                                    {
                                        new Configuration
                                        {
                                            Id = "9746919d-9611-4d1d-98d3-0fc6f083c1fb",
                                            Notes = "This is a SUB module",
                                            ModuleId = "mf_Door",
                                            Attributes = new()
                                            {
                                                new ConfigurationAttribute("mod_FrontHeight", 800)
                                            }
                                        },
                                        new Configuration
                                        {
                                            Id = "61029b65-730a-47de-9c5a-2cbc4cce3fc4",
                                            Notes = "This is a 2nd SUB module",
                                            ModuleId = "mf_Door",
                                            Attributes = new()
                                            {
                                                new ConfigurationAttribute("mod_FrontHeight", 9999)
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
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_Depth", 548),
                                        new ConfigurationAttribute("mod_Height", 2000),
                                        new ConfigurationAttribute("mod_Width", 1000),
                                        new ConfigurationAttribute("mod_TypeElement", "TallUnit")
                                    },
                                    Items = new()
                                    {
                                        new Configuration
                                        {
                                            Id = "810BE6A4-17A2-47C1-A4C7-392ECE5FF584",
                                            Notes = "This is a SUB module",
                                            ModuleId = "mf_Door",
                                            Attributes = new()
                                            {
                                                new ConfigurationAttribute("mod_FrontHeight", 800)
                                            }
                                        },
                                        new Configuration
                                        {
                                            Id = "1CCF8E5C-556A-4356-B254-7C597CC98538",
                                            Notes = "This is a 2nd SUB module",
                                            ModuleId = "mf_Door",
                                            Attributes = new()
                                            {
                                                new ConfigurationAttribute("mod_FrontHeight", 9999)
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Group()
                {
                    Id = "4468B97A-F8C2-4456-BC56-A0C568F4470C",
                    Notes = "This is a 2nd room (ONE roomle planner id)",
                    Name = "Test2",
                    Source = "OrderConfigurator",
                    AdditionalProperties = new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { "roomlePlannerId", "ps_ai687h32o22vdn7twtiqij3e810sjde" },
                    },
                    AdditionalData = new()
                    {
                        new AdditionalDataImage
                        {
                            Category = "Room plan",
                            DownloadUri = new Uri("https://www.roomle.com/planner/room/ps_ai687h32o22vdn7twtiqij3e810sjde.jpg")
                        }
                    },
                    Items = new()
                    {
                        new ConfigurationGroup
                        {
                            Id = "61C36AB3-9406-44A6-8559-CBC132D5D8D4",
                            Notes = "This is an article group (PosGroup)",
                            ContourInformation = "{ \"contour\": \"M 0 0 L 100 0 L 100 100 L 0 100 Z\" }",
                            Items = new()
                            {
                                new ConfigurationPosition
                                {
                                    Id = "07B4A26D-AC4A-4524-99BA-6DE8FACEE41F",
                                    ArticleNumber = "LW_H1000DT",
                                    Notes = "This is a ROOT module",
                                    LibraryId = "CabinetLibrary",
                                    ModuleId = "mr_StorageunitSingle",
                                    Position = new double[] { 0, 0, 0 },
                                    Rotation = new double[] { 0, 0, 0 },
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_Depth", 548),
                                        new ConfigurationAttribute("mod_Height", 1800),
                                        new ConfigurationAttribute("mod_Width", 600),
                                        new ConfigurationAttribute("mod_TypeElement", "TallUnit")
                                    },
                                },
                                new ConfigurationPosition
                                {
                                    Id = "E02967A6-22AF-466F-B721-B4A9DC270488",
                                    ArticleNumber = "LW_H1000DT",
                                    Notes = "This is a 2nd ROOT module",
                                    LibraryId = "CabinetLibrary",
                                    ModuleId = "mr_StorageunitSingle",
                                    Position = new double[] { 600, 0, 0 },
                                    Attributes = new()
                                    {
                                        new ConfigurationAttribute("mod_Depth", 548),
                                        new ConfigurationAttribute("mod_Height", 2000),
                                        new ConfigurationAttribute("mod_Width", 1000),
                                        new ConfigurationAttribute("mod_TypeElement", "TallUnit")
                                    },
                                }
                            }
                        }
                    }
                }
            };

            order.Trace(nameof(order));

            // Try to serialize and deserialize it
            var json = JsonConvert.SerializeObject(order, SerializerSettings.Default);
            var o = JsonConvert.DeserializeObject<OrderDetails>(json, SerializerSettings.Default);
            o.Should().BeEquivalentTo(order);
        }
    }
}