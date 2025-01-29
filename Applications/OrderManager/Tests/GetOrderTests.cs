using HomagConnect.Base.Extensions;
using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Tests
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Orders")]
    public sealed class GetOrderTests
    {
        [TestMethod]
        public void GetOrder()
        {
            var order = new Order();

            // Header

            order.OrderName = "Bedroom & bathroom 01";
            order.Project = "Single family house Müller John";
            order.PersonInCharge = "Hendrik Albers";

            order.Trace(nameof(order));
        }
    }
}