namespace HomagConnect.OrderManager.Contracts.Price
{
    /// <summary>
    /// Data for the price calculation response
    /// </summary>
    public class PriceResponseData
    {
        /// <summary>
        /// The order data for which the price was calculated
        /// </summary>
        public Orders.OrderDetails OrderData { get; set; } = null!;

        /// <summary>
        /// The library which was used for the price calculation
        /// </summary>
        public string LibraryId { get; set; } = null!;

        /// <summary>
        /// Possible errors during price calculation
        /// </summary>
        public IList<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Possible warnings during price calculation
        /// </summary>
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
