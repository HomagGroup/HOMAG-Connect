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
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionManager.Order.OrderStatusChangedEvent", "orderId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "status": "InProduction" }
    /// </example>
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
        /// The status of the order.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        public OrderStatus Status { get; set; }
    }
}
