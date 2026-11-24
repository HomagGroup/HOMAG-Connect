
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Price
{
    /// <summary>
    /// Request to calculate the price of an order.
    /// </summary>
    /// <example>
    /// { "orderData": { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "orderName": "Kitchen Miller" }, "libraryId": "StandardPrices" }
    /// </example>
    public class PriceRequestData
    {
        /// <summary>
        /// Order for which the price is calculated.
        /// </summary>
        [JsonProperty(Order = 1)]
        public Orders.OrderDetails OrderData { get; set; } = null!;

        /// <summary>
        /// Identifier of the price library to use. Optional.
        /// </summary>
        /// <example>StandardPrices</example>
        [JsonProperty(Order = 2)]
        public string? LibraryId { get; set; }
    }
}
