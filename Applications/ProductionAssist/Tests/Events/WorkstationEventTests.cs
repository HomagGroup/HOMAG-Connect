using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts.Events;
using HomagConnect.ProductionAssist.Contracts.Events.Dividing;
using HomagConnect.ProductionAssist.Contracts.Events.Sorting;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Tests.Events;

/// <summary />
[TestClass]
[TestCategory("ProductionAssist")]
[TestCategory("ProductionAssist.Events")]
public class WorkstationEventTests : ProductionAssistTestBase

{
    /// <summary />
    [TestMethod]
    public void Events_CuttingCycleCompleted_SerializeDeserialize()
    {
        var cycleCompletedEvent = new CycleCompletedEvent();

        cycleCompletedEvent.SubscriptionId = Guid.NewGuid();
        cycleCompletedEvent.WorkstationId = Guid.NewGuid();

        cycleCompletedEvent.OptimizationId = Guid.NewGuid();
        cycleCompletedEvent.PatternName = "0001";
        cycleCompletedEvent.PatternCycle = 1;

        cycleCompletedEvent.Trace();

        Assert.IsTrue(cycleCompletedEvent.IsValid);

        TestContext?.AddResultFile(cycleCompletedEvent.TraceToFile("cycleCompletedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_CuttingCycleStarted_SerializeDeserialize()
    {
        var cycleStartedEvent = new CycleStartedEvent();

        cycleStartedEvent.SubscriptionId = Guid.NewGuid();
        cycleStartedEvent.WorkstationId = Guid.NewGuid();

        cycleStartedEvent.OptimizationId = Guid.NewGuid();
        cycleStartedEvent.PatternName = "0001";
        cycleStartedEvent.PatternCycle = 1;
        cycleStartedEvent.BoardEntityId = "Board-01";

        cycleStartedEvent.Trace();

        Assert.IsTrue(cycleStartedEvent.IsValid);

        TestContext?.AddResultFile(cycleStartedEvent.TraceToFile("cycleStartedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_ListProductionAssistEvents()
    {
        var assemblies = new[] { typeof(WorkstationEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public void Events_ProductionItemCompletedEvent_SerializeDeserialize()
    {
        var productionItemCompletedEvent = new ProductionItemCompletedEvent();

        productionItemCompletedEvent.SubscriptionId = Guid.NewGuid();
        productionItemCompletedEvent.WorkstationId = Guid.NewGuid();

        productionItemCompletedEvent.Identifier = "ProdItem-01";
        productionItemCompletedEvent.Quantity = 10;

        productionItemCompletedEvent.Trace();

        Assert.IsTrue(productionItemCompletedEvent.IsValid);

        TestContext?.AddResultFile(productionItemCompletedEvent.TraceToFile(nameof(productionItemCompletedEvent)).FullName);
    }

    [TestMethod]
    public void Events_ProductionItemPickedFromShelfEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        var evt = new ProductionItemPickedFromShelfEvent
        {
            SubscriptionId = Guid.NewGuid(),
            WorkstationId = Guid.NewGuid(),
            Identifier = "ProdItem-200",
            Quantity = 2
        };

        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<ProductionItemPickedFromShelfEvent>(json, SerializerSettings.Default);
        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        Assert.IsNotNull(deserializedTyped);
        Assert.AreEqual(evt.Identifier, deserializedTyped.Identifier);
        Assert.AreEqual(evt.Quantity, deserializedTyped.Quantity);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("identifier"));
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("quantity"));
        Assert.AreEqual("ProdItem-200", deserializedBase.CustomProperties["identifier"].ToString());
        Assert.AreEqual(2, Convert.ToInt32(deserializedBase.CustomProperties["quantity"]));
    }

    [TestMethod]
    public void Events_ProductionItemPlacedInShelfEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        var evt = new ProductionItemPlacedInShelfEvent
        {
            SubscriptionId = Guid.NewGuid(),
            WorkstationId = Guid.NewGuid(),
            Identifier = "ProdItem-100",
            Quantity = 3
        };

        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<ProductionItemPlacedInShelfEvent>(json, SerializerSettings.Default);
        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        Assert.IsNotNull(deserializedTyped);
        Assert.AreEqual(evt.Identifier, deserializedTyped.Identifier);
        Assert.AreEqual(evt.Quantity, deserializedTyped.Quantity);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("identifier"));
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("quantity"));
        Assert.AreEqual("ProdItem-100", deserializedBase.CustomProperties["identifier"].ToString());
        Assert.AreEqual(3, Convert.ToInt32(deserializedBase.CustomProperties["quantity"]));
    }
}