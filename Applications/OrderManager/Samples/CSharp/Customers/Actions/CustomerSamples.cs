using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts;
using HomagConnect.OrderManager.Contracts.Customers;
using System.Collections.ObjectModel;

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

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="orderManager"></param>
        public static async Task<Customer> CreateNewCustomer(IOrderManagerClient orderManager)
        {
            var request = new CreateCustomerRequest
            {
                CustomerNumber = "956",
                CustomerName = "Test Customer",
                Addresses = new Collection<Address>
                {
                    new Address
                    {
                        Street = "Test Street 1",
                        City = "Test City",
                        PostalCode = "12345",
                        Country = "DE",
                        Type = AddressType.Billing,
                        IsDefaultAddress = true,
                        Name = "Test Customer Address",
                    }
                },
                ApiAccess = true,
                Email = "test@example.com",
                TelephoneNumber= "1234567890",
                Notes = "This is a test customer",
                Locale = Locale.deDE,
                AdditionalData =
                [
                    //AdditionalDataEntity is optional, you can add additional data if needed
                    //new AdditionalDataEntity
                    //{
                    //    DownloadUri = new Uri("https://example.com/test.pdf"),
                    //    DownloadFileName= "test.pdf"                        
                    //}
                ]
            };

            var customer = await orderManager.CreateCustomer(request);

            return customer;
        }
    }
}
