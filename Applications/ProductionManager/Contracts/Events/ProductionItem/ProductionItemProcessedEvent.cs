using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when a production item has been processed at a workstation.
/// </summary>
/// <example>
/// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionItemProcessed", "action": "Cut", "identification": "PART-1001", "quantity": 2, "source": "operator@example.com", "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff" }
/// </example>
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
