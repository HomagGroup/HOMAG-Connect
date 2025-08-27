using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.Orders;

namespace HomagConnect.ProductionManager.Contracts.Events.Order;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

// Note: This is preliminary code and is subject to change

/// <summary>
/// Gets or sets an event that occurs when an order has been released.
/// </summary>
[AppEvent(nameof(ProductionManager), nameof(OrderReleasedEvent))]
public class OrderReleasedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the order that has been released.
    /// </summary>
    [Required]
    public OrderDetails OrderDetails { get; set; }
}