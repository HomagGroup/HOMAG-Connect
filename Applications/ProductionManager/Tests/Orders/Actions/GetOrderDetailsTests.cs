using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Contracts.Lots;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.ProductionEntity;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions;

/// <summary />
[TestClass]
[TestCategory("ProductionManager")]
[TestCategory("ProductionManager.Orders")]
[TemporaryDisabledOnServer(2024, 9, 1)]
public class GetOrderDetailsTests : ProductionManagerTestBase
{
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
            BillOfMaterials =
            [
                new ProductionEntityOrderItem
                {
                    OrderItem = "001",
                    ArticleNumber = "U50DT",
                    Description = "Unterschrank mit Doppeltuer",
                    Quantity = 1,
                    ProductionEntities =
                    [
                        new ProductionEntityProductionOrder
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
                                new ProductionOrderLotReference( lot1, 2),
                                new ProductionOrderLotReference { LotId = lot2.Id, Quantity = 1 }
                            ]
                        },
                        new ProductionEntityProductionOrder
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
                new ProductionEntityOrderItem
                {
                    OrderItem = "002",
                    ArticleNumber = "AP45",
                    Description = "Arbeitsplatte 45",
                    Quantity = 1,
                    ProductionEntities =
                    [
                        new ProductionEntityProductionOrder
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

        Trace(order);

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var serialized = JsonConvert.SerializeObject(order, jsonSerializerSettings);
        var deserialized = JsonConvert.DeserializeObject<OrderDetails>(serialized);

        Assert.IsNotNull(deserialized);
        Assert.IsNotNull(deserialized.BillOfMaterials);

        deserialized.Should().NotBe(null);
        deserialized.Should().BeEquivalentTo(order);

        deserialized.BillOfMaterials[0].GetType().Should().Be(order.BillOfMaterials[0].GetType());
    }
}