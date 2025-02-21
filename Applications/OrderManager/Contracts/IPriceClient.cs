namespace HomagConnect.OrderManager.Contracts
{
    /// <summary>
    /// Interface for the price calculation client
    /// </summary>
    public interface IPriceClient
    {
        /// <summary>
        /// Calculate the price for the given order
        /// </summary>
        /// <param name="requestData">The data for the price calculation request</param>
        /// <returns>The data for the price calculation response</returns>
        Task<Price.PriceResponseData> CalculatePrice(Price.PriceRequestData requestData);
    }
}
