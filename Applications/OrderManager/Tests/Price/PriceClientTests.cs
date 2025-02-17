using FluentAssertions;
using HomagConnect.Base.Tests;
using HomagConnect.OrderManager.Client;
using HomagConnect.OrderManager.Contracts.Orders;
using HomagConnect.OrderManager.Contracts.Price;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.OrderManager.Tests.Orders
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Price")]
   
    public sealed class PriceClientTests
    {
        /// <summary />
        [TestMethod]
        public async Task Price_CalculatePrice_NoException()
        {
            // Arrange

            // Simulate server
            using var client = new TestApplication(async ctx =>
            {
                var data = await TestApplication.GetBodyAsAsync<PriceRequestData>(ctx)!;

                data.LibraryId.Should().BeNull();
                var responseData = new PriceResponseData
                {
                    LibraryId = "libId",
                };
                responseData.OrderData = data.OrderData;
                var result = new OkObjectResult(responseData);
                return result;
            }, postPath: "/api/price/calculate").CreateClient();

            // build request
            var orderId = Guid.NewGuid();
            var requestData = new PriceRequestData
            {
                OrderData = new OrderDetails
                {
                    Id = orderId
                }
            };

            // Create client
            var priceClient = new PriceClient(client);

            // Act
            var res = await priceClient.CalculatePrice(requestData);

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.OrderData);
            res.LibraryId.Should().Be("libId");
            res.OrderData.Should().BeEquivalentTo(requestData.OrderData);
        }
    }
}