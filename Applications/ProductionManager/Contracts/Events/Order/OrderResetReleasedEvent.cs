using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.ProductionManager.Contracts.Events.Order;

/// <summary>
/// Gets or sets an event that occurs when an order has been released.
/// </summary>
[AppEvent(nameof(ProductionManager) + "." + nameof(Order) + "." + nameof(OrderResetReleasedEvent))]
public class OrderResetReleasedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the order id that has been released.
    /// </summary>
    [Required]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Gets or sets the email of the person that reset the release of the order
    /// </summary>
    [Required]
    public string ReleasedBy { get; set; }
}
