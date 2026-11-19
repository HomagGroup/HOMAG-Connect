using System.Net;
using System.Text;

using HomagConnect.ProductionManager.Client;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Orders.Actions;

[TestClass]
public class ProductionManagerOrderLookupClientTests
{
    [TestMethod]
    public async Task OrderNumberLookups_UseSeparateCriteria_AndMapAllExternalMatches()
    {
        var orderId = Guid.NewGuid();
        using var handler = new OrderLookupHandler(orderId);
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        var internalOrders = await client.GetOrders(["10001234"]);
        var externalOrders = await client.GetOrdersByOrderNumberExternal(["4711"]);
        var orderById = await client.GetOrder(orderId);

        Assert.AreEqual("?orderNumber=10001234", handler.Requests[0].Query);
        Assert.AreEqual("?orderNumberExternal=4711", handler.Requests[1].Query);
        Assert.AreEqual($"/api/productionManager/orders/{orderId}", handler.Requests[2].AbsolutePath);
        Assert.IsNotNull(internalOrders);
        Assert.AreEqual(2, externalOrders.Count());
        Assert.IsTrue(externalOrders.All(order => order.OrderNumberExternal == "4711"));
        Assert.IsNotNull(orderById);
        Assert.AreEqual(orderId, orderById.Id);
    }

    [TestMethod]
    public async Task OrderNumbersExternalLookup_RepeatsEncodedCriteriaAndIgnoresDuplicatesAndBlanks()
    {
        using var handler = new OrderLookupHandler(Guid.NewGuid());
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        var orders = await client.GetOrdersByOrderNumberExternal(["4711", " ", "4711", " ERP &+ "]);

        Assert.AreEqual(1, handler.Requests.Count);
        Assert.AreEqual("?orderNumberExternal=4711&orderNumberExternal=%20ERP%20%26%2B%20",
            handler.Requests[0].Query);
        Assert.AreEqual(2, orders.Count());
    }

    [TestMethod]
    public async Task OrderNumbersExternalLookup_SendsOneRequestPerUrlSizedBatch()
    {
        var firstNumber = new string('a', 1000);
        var secondNumber = new string('b', 1000);
        var firstBatch = Enumerable.Range(0, 1000).Select(_ => new { Id = Guid.NewGuid() }).ToArray();
        var secondNumberMatch = new { Id = Guid.NewGuid() };
        using var handler = new OrderLookupHandler(Guid.NewGuid(), uri => uri.Query switch
        {
            var query when query == $"?orderNumberExternal={firstNumber}" => JsonConvert.SerializeObject(firstBatch),
            var query when query == $"?orderNumberExternal={secondNumber}" => JsonConvert.SerializeObject(new[] { secondNumberMatch }),
            _ => throw new AssertFailedException("Unexpected lookup query.")
        });
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        var orders = await client.GetOrdersByOrderNumberExternal([firstNumber, secondNumber]);

        CollectionAssert.AreEquivalent(new[] { $"?orderNumberExternal={firstNumber}", $"?orderNumberExternal={secondNumber}" },
            handler.Requests.Select(uri => uri.Query).ToArray());
        CollectionAssert.AreEquivalent(firstBatch.Select(order => order.Id).Append(secondNumberMatch.Id).ToArray(),
            orders.Select(order => order.Id).ToArray());
    }

    [TestMethod]
    public async Task OrderNumbersExternalLookup_DoesNotSendUnfilteredRequests()
    {
        using var handler = new OrderLookupHandler(Guid.NewGuid());
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        Assert.AreEqual(0, (await client.GetOrdersByOrderNumberExternal([])).Count());
        Assert.AreEqual(0, (await client.GetOrdersByOrderNumberExternal(["", " "])).Count());
        await Assert.ThrowsExactlyAsync<ArgumentException>(() => client.GetOrdersByOrderNumberExternal(["4711", new string('x', 2000)]));
        Assert.AreEqual(0, handler.Requests.Count);
    }

    private sealed class OrderLookupHandler(Guid orderId, Func<Uri, string>? respond = null) : HttpMessageHandler
    {
        private readonly string _ordersResponse = $$"""
            [
              { "Id": "{{Guid.NewGuid()}}", "OrderName": "Order 1", "OrderNumber": "10001234", "OrderNumberExternal": "4711" },
              { "Id": "{{Guid.NewGuid()}}", "OrderName": "Order 2", "OrderNumber": "10005678", "OrderNumberExternal": "4711" }
            ]
            """;

        private readonly string _orderResponse = $$"""
            { "Id": "{{orderId}}", "OrderName": "Order by id", "OrderNumber": "10001234", "OrderNumberExternal": "4711" }
            """;

        public List<Uri> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestUri = request.RequestUri!;
            lock (Requests)
            {
                Requests.Add(requestUri);
            }

            var isOrderByIdRequest = requestUri.AbsolutePath.EndsWith($"/{orderId}", StringComparison.OrdinalIgnoreCase);
            var content = respond?.Invoke(requestUri) ?? (isOrderByIdRequest ? _orderResponse : _ordersResponse);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            });
        }
    }
}
