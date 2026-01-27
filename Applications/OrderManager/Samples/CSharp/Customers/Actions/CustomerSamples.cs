using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Customers;

namespace HomagConnect.OrderManager.Samples.Customers.Actions
{
    /// <summary>
    /// Sample class which shows how to delete customers
    /// </summary>
    public static class CustomerSamples
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

        /// <summary>
        /// Get customer by number
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task<Customer> GetCustomerByCustomerNumber(IOrderManagerClient orderManager)
        {
            var customerNumber = "11111111111"; // add some customer number

            var customer = await orderManager.GetCustomer(customerNumber);

            return customer;
        }
    }
}
