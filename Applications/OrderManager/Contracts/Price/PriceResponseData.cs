using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Price
{
    /// <summary>
    /// Result of a price calculation, including the priced order and any errors or warnings.
    /// </summary>
    /// <example>
    /// { "orderData": { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "orderName": "Kitchen Miller" }, "libraryId": "StandardPrices", "errors": [], "warnings": ["No price found for position 3."] }
    /// </example>
    public class PriceResponseData
    {
        /// <summary>
        /// Order including the calculated prices.
        /// </summary>
        [JsonProperty(Order = 1)]
        public Orders.OrderDetails OrderData { get; set; } = null!;

        /// <summary>
        /// Identifier of the price library that was used.
        /// </summary>
        /// <example>StandardPrices</example>
        [JsonProperty(Order = 2)]
        public string LibraryId { get; set; } = null!;

        /// <summary>
        /// Errors that occurred during the price calculation. Empty if there were none.
        /// </summary>
        [JsonProperty(Order = 3)]
        public IList<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Warnings that occurred during the price calculation. Empty if there were none.
        /// </summary>
        [JsonProperty(Order = 4)]
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
