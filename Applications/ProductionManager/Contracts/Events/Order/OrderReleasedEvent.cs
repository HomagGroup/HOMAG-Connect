using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.Orders;

namespace HomagConnect.ProductionManager.Contracts.Events.Order;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

// Note: This is preliminary code and is subject to change

/// <summary>
/// Event that occurs when an order has been released for production.
/// </summary>
/// <example>
/// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.Order.OrderReleasedEvent",
/// "orderDetails": { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "orderName": "Kitchen Miller", "orderDate": "2025-04-01T08:30:00+00:00", "orderStatus": "ReadyForProduction", "additionalData": [], "items": [] },
/// "releasedBy": "planner@example.com" }
/// </example>
[AppEvent(nameof(ProductionManager) + "." + nameof(Order) + "." + nameof(OrderReleasedEvent))]
public class OrderReleasedEvent : AppEvent
{
    /// <summary>
    /// Details of the order that was released, including its production items.
    /// </summary>
    [Required]
    public OrderDetails OrderDetails { get; set; }

    /// <summary>
    /// Email address of the person who released the order.
    /// </summary>
    [Required]
    public string ReleasedBy { get; set; }
}