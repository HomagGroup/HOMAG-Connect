#nullable enable

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.ProductionAssist.Contracts.Events.Feedback;

namespace HomagConnect.ProductionAssist.Contracts.Events.Sorting
{
    /// <summary>
    /// Event that occurs when a production item has been picked from a shelf.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(Sorting) + "." + nameof(ProductionItemPickedFromShelfEvent))]
    public class ProductionItemPickedFromShelfEvent : ProductionItemCompletedEvent;
}