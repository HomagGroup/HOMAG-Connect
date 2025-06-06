using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Samples.Customers.Actions
{
    /// <summary>
    /// Sample class which shows how to delete customers
    /// </summary>
    public static class DeleteCustomerSamples
    {
        /// <summary>
        /// Delete customers by ids
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteCustomersByCustomerIds(IOrderManagerClient orderManager)
        {
            var ids = new List<Guid> { }; // add some ids

            await orderManager.DeleteCustomersByCustomerIds(ids);
        }

        /// <summary>
        /// Delete customers by numbers
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task DeleteCustomersByCustomerNumbers(IOrderManagerClient orderManager)
        {
            var customerNumbers = new List<string> { }; // add some customer numbers

            await orderManager.DeleteCustomersByCustomerNumbers(customerNumbers);
        }
    }
}
