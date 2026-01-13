using HomagConnect.Base;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts;
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
    public void Events_CycleItemCompleted_SerializeDeserialize()
    {
        var cycleItemCompletedEvent = new CycleItemCompletedEvent();

        cycleItemCompletedEvent.SubscriptionId = Guid.NewGuid();
        cycleItemCompletedEvent.WorkstationId = Guid.NewGuid();

        cycleItemCompletedEvent.OptimizationId = Guid.NewGuid();
        cycleItemCompletedEvent.PatternName = "0001";
        cycleItemCompletedEvent.PatternCycle = 1;
        cycleItemCompletedEvent.Identifier = "I0012";
        cycleItemCompletedEvent.Trace();

        Assert.IsTrue(cycleItemCompletedEvent.IsValid);

        TestContext?.AddResultFile(cycleItemCompletedEvent.TraceToFile("cycleItemCompletedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_WorkstationUpsertedEventCreated_SerializeDeserialize()
    {
        var workstationUpsertedEvent = new WorkstationUpsertedEvent();

        workstationUpsertedEvent.SubscriptionId = Guid.NewGuid();
        workstationUpsertedEvent.Workstation = new Workstation 
        { 
            Id = Guid.NewGuid(),
            DisplayName = "TestMachine",
            Type = WorkstationType.Cutting,
        };
        workstationUpsertedEvent.Trace();

        Assert.IsTrue(workstationUpsertedEvent.IsValid);
        Assert.AreEqual(UpsertAction.Created, workstationUpsertedEvent.Action);

        TestContext?.AddResultFile(workstationUpsertedEvent.TraceToFile("workstationUpsertedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_WorkstationUpsertedEventUpdated_SerializeDeserialize()
    {
        var workstationUpsertedEvent = new WorkstationUpsertedEvent();

        workstationUpsertedEvent.SubscriptionId = Guid.NewGuid();
        workstationUpsertedEvent.Workstation = new Workstation
        {
            Id = Guid.NewGuid(),
            DisplayName = "TestMachine",
            Type = WorkstationType.Cutting,
        };
        workstationUpsertedEvent.Action = UpsertAction.Updated;
        workstationUpsertedEvent.Trace();

        Assert.IsTrue(workstationUpsertedEvent.IsValid);
        Assert.AreEqual(UpsertAction.Updated, workstationUpsertedEvent.Action);

        TestContext?.AddResultFile(workstationUpsertedEvent.TraceToFile("workstationUpsertedEvent").FullName);
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
        Assert.IsNotEmpty(derivedTypes);

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public void Events_ProductionItemCompletedEvent_SerializeDeserialize()
    {
        var pice = new ProductionItemCompletedEvent();

        pice.SubscriptionId = Guid.NewGuid();
        pice.WorkstationId = Guid.NewGuid();

        pice.Identifier = "ProdItem-01";
        pice.Quantity = 10;

        pice.Trace();

        Assert.IsTrue(pice.IsValid);

        TestContext?.AddResultFile(pice.TraceToFile("ProductionItemCompletedEventSample").FullName);
    }


    /// <summary />
    [TestMethod]
    public void Events_ProductionItemCompletedByParentEvent_SerializeDeserialize()
    {
        var productionItemCompletedEvent2 = new ProductionItemCompletedByParentEvent();

        productionItemCompletedEvent2.SubscriptionId = Guid.NewGuid();
        productionItemCompletedEvent2.WorkstationId = Guid.NewGuid();

        productionItemCompletedEvent2.Identifier = "ProdItem-11";
        productionItemCompletedEvent2.ParentIdentifier = "ProdItem-01";
        productionItemCompletedEvent2.Quantity = 10;

        productionItemCompletedEvent2.Trace();

        Assert.IsTrue(productionItemCompletedEvent2.IsValid);

        TestContext?.AddResultFile(productionItemCompletedEvent2.TraceToFile(nameof(productionItemCompletedEvent2)).FullName);
    }

    [TestMethod]
    public void Events_ProductionItemCompletedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var evt = new ProductionItemCompletedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            WorkstationId = Guid.NewGuid(),
            Identifier = "ProdItem-42",
            Quantity = 5
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        // Deserialize as ProductionItemCompletedEvent
        var deserializedTyped = JsonConvert.DeserializeObject<ProductionItemCompletedEvent>(json, SerializerSettings.Default);

        // Deserialize as AppEvent
        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsTrue(evt.IsValid);
        Assert.IsNotNull(deserializedTyped);
        Assert.AreEqual(evt.Identifier, deserializedTyped.Identifier);
        Assert.AreEqual(evt.Quantity, deserializedTyped.Quantity);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("identifier"));
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("quantity"));
        Assert.AreEqual("ProdItem-42", deserializedBase.CustomProperties["identifier"].ToString());
        Assert.AreEqual(5, Convert.ToInt32(deserializedBase.CustomProperties["quantity"]));
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

        Assert.IsTrue(evt.IsValid);
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

        Assert.IsTrue(evt.IsValid);
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