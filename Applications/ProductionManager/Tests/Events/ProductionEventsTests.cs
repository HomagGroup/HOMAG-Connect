using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.Events.Order;
using HomagConnect.ProductionManager.Contracts.Events.ProductionItem;
using HomagConnect.ProductionManager.Contracts.Orders;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Events;

/// <summary />
[TestClass]
[TestCategory("Events")]
[TestCategory("ProductionManager.Events")]
public class ProductionEventsTests : ProductionManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_ListProductionManagerEvents()
    {
        var assemblies = new[] { typeof(OrderDetails).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public void ProductionItemProcessedByMachineFileEvent_SerializeDeserialize_PropertiesEqual()
    {
        var productionItemProcessedByMachineFileEvent = new ProductionItemProcessedByMachineFileEvent();

        productionItemProcessedByMachineFileEvent.SubscriptionId = SubscriptionId;
        productionItemProcessedByMachineFileEvent.MachineNumber = "0-250-743-491";
        productionItemProcessedByMachineFileEvent.FileContent = @"20250124144140,PNL,9999,9999,1,1,1,1,XYZ789,,,,,2,1,,,,,,,,,,,,,,,C:\MACHINE1\Control\c1\data\cnc\mp4\LL_TEMPLATES\T2.mprx,XYZ789";
        productionItemProcessedByMachineFileEvent.FileName = "CNC1_20250124144140.hol";
        productionItemProcessedByMachineFileEvent.ColumnMapping = new Dictionary<string, Dictionary<int, string>>
        {
            {
                "PNL",
                new Dictionary<int, string>
                {
                    { 31, "PartId" }
                }
            }
        };

        Assert.IsTrue(productionItemProcessedByMachineFileEvent.IsValid);

        TestContext?.AddResultFile(productionItemProcessedByMachineFileEvent.TraceToFile("productionItemProcessedByMachineFileEvent").FullName);

        // Serialize and deserialize again as base type
        var productionItemProcessedByMachineFileEventSerialized = JsonConvert.SerializeObject(productionItemProcessedByMachineFileEvent, SerializerSettings.Default);
        var appEvent = JsonConvert.DeserializeObject<AppEvent>(productionItemProcessedByMachineFileEventSerialized, SerializerSettings.Default);
        Assert.IsNotNull(appEvent);
        TestContext?.AddResultFile(appEvent.TraceToFile("appEvent").FullName);

        // Serialize base type and deserialize again as derived type
        var appEventSerialized = JsonConvert.SerializeObject(appEvent);
        var productionItemProcessedByMachineFileEventDeserialized = JsonConvert.DeserializeObject<ProductionItemProcessedByMachineFileEvent>(appEventSerialized, SerializerSettings.Default);
        Assert.IsNotNull(productionItemProcessedByMachineFileEventDeserialized);
        TestContext?.AddResultFile(productionItemProcessedByMachineFileEventDeserialized.TraceToFile("productionItemProcessedByMachineFileEventDeserialized").FullName);

        // Compare properties
        Assert.AreEqual(productionItemProcessedByMachineFileEvent.Id, productionItemProcessedByMachineFileEventDeserialized.Id);
        Assert.AreEqual(productionItemProcessedByMachineFileEvent.Timestamp, productionItemProcessedByMachineFileEventDeserialized.Timestamp);
        Assert.AreEqual(productionItemProcessedByMachineFileEvent.SubscriptionId, productionItemProcessedByMachineFileEventDeserialized.SubscriptionId);
        Assert.AreEqual(productionItemProcessedByMachineFileEvent.MachineNumber, productionItemProcessedByMachineFileEventDeserialized.MachineNumber);
        Assert.AreEqual(productionItemProcessedByMachineFileEvent.FileContent, productionItemProcessedByMachineFileEventDeserialized.FileContent);
    }

    [TestMethod]
    public void OrderReleased_Serialization()
    {
        var completedAt = DateTimeOffset.Now;
        var orderReleased = new OrderReleasedEvent
        {
            Timestamp = completedAt,
            SubscriptionId = Guid.NewGuid(),
            OrderDetails = new OrderDetails{ OrderName = "TestOrder"}
           
        };

        TestContext.AddResultFile(orderReleased.TraceToFile("OrderReleasedEvent").FullName);

        var orderReleasedSerialized = JsonConvert.SerializeObject(orderReleased, SerializerSettings.Default);
        var orderReleasedDeserialized = JsonConvert.DeserializeObject<AppEvent>(orderReleasedSerialized);

        Assert.IsNotNull(orderReleasedDeserialized);
        Assert.AreEqual(orderReleased.Key, orderReleasedDeserialized.Key);
    }

}