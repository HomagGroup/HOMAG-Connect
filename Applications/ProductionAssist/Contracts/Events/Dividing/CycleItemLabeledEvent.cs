using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.ProductionAssist.Contracts.Events.Dividing
{
    /// <summary>
    /// Represents an event that occurs when a cycle item has been labeled in the dividing process.
    /// Contains information about the optimization, pattern cycle, and pattern name.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Dividing) + "." + nameof(CycleItemLabeledEvent))]
    public class CycleItemLabeledEvent : CycleItemCompletedEvent
    {
    }
}