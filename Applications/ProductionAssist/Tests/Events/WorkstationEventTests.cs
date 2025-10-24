using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts.Events;
using HomagConnect.ProductionAssist.Contracts.Events.Dividing;

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
}