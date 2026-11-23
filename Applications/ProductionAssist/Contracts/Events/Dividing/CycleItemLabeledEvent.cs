using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Represents an event that occurs when a cycle item has been labeled in the dividing process.
    /// Contains information about the optimization, pattern cycle, and pattern name.
    /// </summary>
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionAssist.Dividing.CycleItemLabeledEvent", "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "identifier": "PART-1001", "quantity": 1, "optimizationId": "3f2504e0-4f89-11d3-9a0c-0305e82c3301", "patternName": "Pattern 1", "patternCycle": 3 }
    /// </example>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleItemLabeledEvent))]
    public class CycleItemLabeledEvent : CycleItemCompletedEvent
    {
    }
}
