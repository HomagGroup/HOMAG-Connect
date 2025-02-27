
namespace HomagConnect.OrderManager.Contracts.Price
{
    /// <summary>
    /// Data for the price calculation request
    /// </summary>
    public class PriceRequestData
    {
        /// <summary>
        /// The order data for which the price should be calculated
        /// </summary>
        public Orders.OrderDetails OrderData { get; set; } = null!;

        /// <summary>
        /// Optional the library which should be used for the price calculation
        /// </summary>
        public string? LibraryId { get; set; }
    }
}
