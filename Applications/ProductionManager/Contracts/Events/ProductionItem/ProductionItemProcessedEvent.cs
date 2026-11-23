using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when a production item has been processed at a workstation.
/// </summary>
[AppEvent("ProductionItemProcessed")]
public class ProductionItemProcessedEvent : AppEvent
{
    /// <summary>
    /// The action to be taken when a production item is processed.
    /// </summary>
    public ProductionItemProcessedAction Action { get; set; }

    /// <summary>
    /// Identifier of the processed production item.
    /// </summary>
    public string Identification { get; set; }

    /// <summary>
    /// Number of items that were processed.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Person or program that triggered the processing. Optional.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Unique identifier of the workstation at which the item was processed.
    /// </summary>
    public Guid WorkstationId { get; set; }
}