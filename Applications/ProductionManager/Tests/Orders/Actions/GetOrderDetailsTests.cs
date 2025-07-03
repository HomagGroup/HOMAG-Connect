using FluentAssertions;

using HomagConnect.Base;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Lots;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions;

/// <summary />
[TestClass]
[TestCategory("ProductionManager")]
[TestCategory("ProductionManager.Orders")]
public class GetOrderDetailsTests : ProductionManagerTestBase
{
    [TestMethod]
    public void CheckOrderEnumDeserialization_Success()
    {
        var str = JsonConvert.SerializeObject(new Order(), SerializerSettings.Default); 
        var obj = JsonConvert.DeserializeObject<Order>(str, SerializerSettings.Default);

        Assert.IsNotNull(obj);
        Assert.IsNotNull(obj.OrderStatus);
    }

    [TestMethod]
    public void CheckProductionEntityNullableEnumDeserialization_Success()
    {
        var str = JsonConvert.SerializeObject(new Part { LaminateTopGrain = Grain.Crosswise }, SerializerSettings.Default);
        var obj = JsonConvert.DeserializeObject<Part>(str, SerializerSettings.Default);

        Assert.IsNotNull(obj);
        Assert.IsNull(obj.LaminateBottomGrain);
        Assert.IsNotNull(obj.LaminateTopGrain);
    }

    /// <summary />
    [TestMethod]
    public void Order_Trace()
    {
        var lot1 = new Lot { Id = Guid.NewGuid(), Name = "Lot 1" };
        var lot2 = new Lot { Id = Guid.NewGuid(), Name = "Lot 2" };

        var order = new Order
        {
            OrderName = "Mini Schrank",
            CustomerName = "Boris Wehrle",
            CustomerNumber = "4711",
            OrderDate = DateTime.Today,
            DeliveryDatePlanned = DateTime.Today.AddDays(14),
            Lots =
            [
                lot1, lot2
            ],
            Address = new Address
            {
                Street = "Homagstrasse",
                HouseNumber = "3-5",
                PostalCode = "72296",
                City = "Schopfloch",
                Country = "Germany"
            },
            Source = "Test Source",
            QuantityOfParts = 10,
            QuantityOfPartsPlanned = 10
        };

        order.Trace();

        var serialized = JsonConvert.SerializeObject(order,  SerializerSettings.Default);
        var deserialized = JsonConvert.DeserializeObject<Order>(serialized, SerializerSettings.Default);

        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Source);

        deserialized.Should().NotBe(null);
        deserialized.Should().BeEquivalentTo(order);

        deserialized.Address.GetType().Should().Be(order.Address.GetType());
    }

    /// <summary />
    [TestMethod]
    public void OrderDetails_Trace()
    {
        var lot1 = new Lot { Id = Guid.NewGuid(), Name = "Lot 1" };
        var lot2 = new Lot { Id = Guid.NewGuid(), Name = "Lot 2" };

        var order = new OrderDetails
        {
            OrderName = "Mini Schrank",
            CustomerName = "Boris Wehrle",
            CustomerNumber = "4711",
            OrderDate = DateTime.Today,
            DeliveryDatePlanned = DateTime.Today.AddDays(14),
            Lots =
            [
                lot1, lot2
            ],
            Address = new Address
            {
                Street = "Homagstrasse",
                HouseNumber = "3-5",
                PostalCode = "72296",
                City = "Schopfloch",
                Country = "Germany"
            },
            Items =
            [
                new Position
                {
                    OrderItem = "001",
                    ArticleNumber = "U50DT",
                    Description = "Unterschrank mit Doppeltuer",
                    Quantity = 1,
                    Items =
                    [
                        new Part
                        {
                            Quantity = 3,
                            Grain = Grain.None,
                            ArticleNumber = "002",
                            ArticleGroup = "Seite_L",
                            Barcode = "WEBSEITEL",
                            Length = 576,
                            Width = 461,
                            Thickness = 16.6,
                            Notes = "Egger",
                            Material = "P2_White_19",
                            Description = "Seite_L",
                            CncProgramName1 = "spl_01_b5c3fb76-f85e-439c-aa0b-889564249101.mpr",
                            Lots =
                            [
                                new ProductionOrderLotReference(lot1, 2),
                                new ProductionOrderLotReference { LotId = lot2.Id, Quantity = 1 }
                            ]
                        },
                        new Part
                        {
                            Quantity = 1,
                            Grain = Grain.None,
                            ArticleNumber = "003",
                            ArticleGroup = "Seite_R",
                            Barcode = "WEBSEITER",
                            Length = 576,
                            Width = 461,
                            Thickness = 16.6,
                            Notes = "Egger",
                            Material = "P2_White_19",
                            Description = "Seite_R",
                            CncProgramName1 = "spr_01_6e459307-7e7d-43e1-af97-3d6bd04932a4.mpr"
                        }
                    ]
                },
                new Position
                {
                    OrderItem = "002",
                    ArticleNumber = "AP45",
                    Description = "Arbeitsplatte 45",
                    Quantity = 1,
                    Items =
                    [
                        new Part
                        {
                            Quantity = 1,
                            Grain = Grain.None,
                            ArticleNumber = "015",
                            ArticleGroup = "Arbeitsplatte",
                            Length = 500.0,
                            Width = 461.0,
                            Thickness = 19.0,
                            Notes = "Egger",
                            Material = "P2_Grey_19",
                            Description = "Arbeitsplatte",
                            Barcode = "WEBAP"
                        }
                    ]
                }
            ]
        };

        order.Trace();

        var serialized = JsonConvert.SerializeObject(order, SerializerSettings.Default);
        var deserialized = JsonConvert.DeserializeObject<OrderDetails>(serialized, SerializerSettings.Default);

        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.Items);

        deserialized.Should().NotBe(null);
        deserialized.Should().BeEquivalentTo(order);

        deserialized.Items[0].GetType().Should().Be(order.Items[0].GetType());
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 07, 10, "DF-Production")]
    public async Task OrderDetails_Items_NumberOfPartsEqualsItems()
    {
        var productionManagerClient = GetProductionManagerClient();

        var orders = await productionManagerClient.GetOrders(100);

        Assert.IsNotNull(orders);

        var order = orders.FirstOrDefault(o => !string.IsNullOrWhiteSpace(o.OrderNumber) && o.QuantityOfParts > 0);

        if (order == null)
        {
            Assert.Inconclusive("There is no order available in the current subscription having a order number and parts set.");
        }

        var orderNumber = order.OrderNumber;
        Assert.IsNotNull(orderNumber);

        var orderDetails = await productionManagerClient.GetOrder(orderNumber);
        Assert.IsNotNull(orderDetails);

        Assert.AreEqual(orderNumber, orderDetails.OrderNumber);

        // TODO: Add items check

        TestContext?.AddResultFile(orderDetails.TraceToFile(nameof(OrderDetails_Items_NumberOfPartsEqualsItems)).FullName);
    }
}