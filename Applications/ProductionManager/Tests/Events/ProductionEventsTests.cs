using Applications.ProductionManager.Contracts.Events.ProductionItem;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.Events.Order;
using HomagConnect.ProductionManager.Contracts.Events.ProductionItem;
using HomagConnect.ProductionManager.Contracts.Events.Rework;
using HomagConnect.ProductionManager.Contracts.Orders;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
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
        Assert.IsNotEmpty(derivedTypes);

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
            OrderDetails = new OrderDetails{ OrderName = "TestOrder"},
            ReleasedBy = "t@t.com"
           
        };

        TestContext?.AddResultFile(orderReleased.TraceToFile("OrderReleasedEvent").FullName);

        var orderReleasedSerialized = JsonConvert.SerializeObject(orderReleased, SerializerSettings.Default);
        var orderReleasedDeserialized = JsonConvert.DeserializeObject<AppEvent>(orderReleasedSerialized);

        Assert.IsNotNull(orderReleasedDeserialized);
        Assert.AreEqual(orderReleased.Key, orderReleasedDeserialized.Key);
    }

    [TestMethod]
    public void OrderStatusChangedEvent_Serialization()
    {
        var completedAt = DateTimeOffset.Now;
        var orderEvent = new OrderStatusChangedEvent
        {
            Timestamp = completedAt,
            SubscriptionId = Guid.NewGuid(),
            Status = OrderStatus.New

        };

        TestContext?.AddResultFile(orderEvent.TraceToFile("OrderStatusChangedEvent").FullName);

        var eventSerialized = JsonConvert.SerializeObject(orderEvent, SerializerSettings.Default);
        var eventDeserialized = JsonConvert.DeserializeObject<AppEvent>(eventSerialized);

        Assert.IsNotNull(eventDeserialized);
        Assert.AreEqual(orderEvent.Key, eventDeserialized.Key);
    }

    [TestMethod]
    public void ProductionItemStatusChangedEvent_Serialization()
    {
        var completedAt = DateTimeOffset.Now;
        var productionItemEvent = new ProductionItemStatusChangedEvent
        {
            Timestamp = completedAt,
            SubscriptionId = Guid.NewGuid(),
            Status = ProductionItemStatus.InProduction

        };

        TestContext?.AddResultFile(productionItemEvent.TraceToFile("ProductionItemStatusChangedEvent").FullName);

        var eventSerialized = JsonConvert.SerializeObject(productionItemEvent, SerializerSettings.Default);
        var eventDeserialized = JsonConvert.DeserializeObject<AppEvent>(eventSerialized);

        Assert.IsNotNull(eventDeserialized);
        Assert.AreEqual(productionItemEvent.Key, eventDeserialized.Key);
    }

    [TestMethod]
    public void ReworkStatusChangedEvent_Serialization()
    {
        var completedAt = DateTimeOffset.Now;
        var reworkEvent = new ReworkStatusChangedEvent
        {
            Timestamp = completedAt,
            SubscriptionId = Guid.NewGuid(),
            State = Contracts.Rework.ReworkState.Pending,
            StatusChangedBy = "Test User"

        };

        TestContext?.AddResultFile(reworkEvent.TraceToFile("ReworkStatusChangedEvent").FullName);

        var eventSerialized = JsonConvert.SerializeObject(reworkEvent, SerializerSettings.Default);
        var eventDeserialized = JsonConvert.DeserializeObject<AppEvent>(eventSerialized);

        Assert.IsNotNull(eventDeserialized);
        Assert.AreEqual(reworkEvent.Key, eventDeserialized.Key);
    }

    [TestMethod]
    public void ReworkCreatedEvent_Serialization()
    {
        var completedAt = DateTimeOffset.Now;
        var reworkEvent = new ReworkCreatedEvent
        {
            Timestamp = completedAt,
            SubscriptionId = Guid.NewGuid(),
            Rework = new Contracts.Rework.Rework
            {
                Id = "0123456",
                CapturedAt = completedAt,
                Description = "Test Rework",
                State = Contracts.Rework.ReworkState.Pending,
                ReworkId = "RW-001"
            }

        };

        TestContext?.AddResultFile(reworkEvent.TraceToFile("ReworkCreatedEvent").FullName);

        var eventSerialized = JsonConvert.SerializeObject(reworkEvent, SerializerSettings.Default);
        var eventDeserialized = JsonConvert.DeserializeObject<AppEvent>(eventSerialized);

        Assert.IsNotNull(eventDeserialized);
        Assert.AreEqual(reworkEvent.Key, eventDeserialized.Key);
    }

}