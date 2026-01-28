using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.ProductionManager.Contracts.Orders;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.Order
{
    /// <summary>
    /// Represents an event that is triggered when the status of an order has changed.
    /// </summary>
    [AppEvent(nameof(ProductionManager) + "." + nameof(Order) + "." + nameof(OrderStatusChangedEvent))]
    public class OrderStatusChangedEvent : AppEvent
    {
        /// <summary>
        /// The unique id of this order
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        public Guid OrderId { get; set; }

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        public OrderStatus Status { get; set; }
    }
}
