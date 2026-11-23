using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.ProductionManager.Contracts.Events.Order;

/// <summary>
/// Represents an event that occurs when an order has been deleted.
/// </summary>
/// <example>
/// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.Order.OrderDeletedEvent", "orderId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" }
/// </example>
[AppEvent(nameof(ProductionManager) + "." + nameof(Order) + "." + nameof(OrderDeletedEvent))]
public class OrderDeletedEvent : AppEvent
{
    /// <summary>
    /// The order id that has been deleted.
    /// </summary>
    [Required]
    public Guid OrderId { get; set; }
}
