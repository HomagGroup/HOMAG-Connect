using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Gets or sets an event that occurs when an order has been released.
/// </summary>
[AppEvent("ProductionItemProcessed")]
public class ProductionItemProcessedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the action to be taken when a production item is processed.
    /// </summary>
    public ProductionItemProcessedAction Action { get; set; }

    /// <summary>
    /// Identification
    /// </summary>
    public string Identification { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or Sets the source
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Workstation id
    /// </summary>
    public Guid WorkstationId { get; set; }
}