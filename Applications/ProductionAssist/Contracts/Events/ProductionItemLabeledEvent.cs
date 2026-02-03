using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.ProductionAssist.Contracts.Events
{
    /// <summary>
    /// Represents an event that is triggered when a production item has been labeled.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(ProductionItemLabeledEvent))]
    public class ProductionItemLabeledEvent : ProductionItemCompletedEvent
    {
    }
}
