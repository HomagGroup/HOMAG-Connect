using HomagConnect.OrderManager.Samples.Customers.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.OrderManager.Tests.Customers
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Customers")]
    public sealed class CustomersTests : OrderManagerTestBase
    {
        [TestMethod]
        public async Task DeleteCustomersByCustomerIds()
        {
            var orderManager = GetOrderManagerClient();

            try
            {
                await CustomerSamples.DeleteCustomersByCustomerIds(orderManager);
            }
            catch (ArgumentNullException) 
            {
                // catch as no customer ids are hardcoded in sample
            }
        }

        [TestMethod]
        public async Task DeleteCustomersByCustomerNumbers()
        {
            var orderManager = GetOrderManagerClient();

            try
            {
                await CustomerSamples.DeleteCustomersByCustomerNumbers(orderManager);
            }
            catch (ArgumentNullException) 
            {
                // catch as no customer numbers are hardcoded in sample
            }
        }

        [TestMethod]
        public async Task GetCustomerByCustomerNumbers()
        {
            var orderManager = GetOrderManagerClient();

            try
            {
                await CustomerSamples.GetCustomerByCustomerNumber(orderManager);
            }
            catch (ArgumentNullException)
            {
                // catch as no customer numbers are hardcoded in sample
            }
        }
    }
}
