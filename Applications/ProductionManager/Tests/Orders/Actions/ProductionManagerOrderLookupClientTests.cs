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
        Assert.AreEqual("?take=1000&skip=0&orderNumberExternal=4711", handler.Requests[1].Query);
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
        Assert.AreEqual("?take=1000&skip=0&orderNumberExternal=4711&orderNumberExternal=%20ERP%20%26%2B%20",
            handler.Requests[0].Query);
        Assert.AreEqual(2, orders.Count());
    }

    [TestMethod]
    public async Task OrderNumbersExternalLookup_SendsOneRequestPerBatchSequentially()
    {
        var firstNumber = new string('a', 1000);
        var secondNumber = new string('b', 1000);
        var firstBatch = Enumerable.Range(0, 1000).Select(_ => new { Id = Guid.NewGuid() }).ToArray();
        var secondNumberMatch = new { Id = Guid.NewGuid() };
        var firstResponse = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var handler = new OrderLookupHandler(Guid.NewGuid(), async uri => uri.Query switch
        {
            var query when query == $"?take=1000&skip=0&orderNumberExternal={firstNumber}" => await CompleteFirstBatch(),
            var query when query == $"?take=1000&skip=0&orderNumberExternal={secondNumber}" => JsonConvert.SerializeObject(new[] { secondNumberMatch }),
            _ => throw new AssertFailedException("Unexpected lookup query.")
        });
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        var lookup = client.GetOrdersByOrderNumberExternal([firstNumber, secondNumber]);
        try
        {
            Assert.AreEqual(1, handler.Requests.Count);
            Assert.IsFalse(lookup.IsCompleted);
        }
        finally
        {
            firstResponse.SetResult();
        }

        var orders = await lookup;
        CollectionAssert.AreEqual(new[] { $"?take=1000&skip=0&orderNumberExternal={firstNumber}", $"?take=1000&skip=0&orderNumberExternal={secondNumber}" },
            handler.Requests.Select(uri => uri.Query).ToArray());
        CollectionAssert.AreEquivalent(firstBatch.Select(order => order.Id).Append(secondNumberMatch.Id).ToArray(),
            orders.Select(order => order.Id).ToArray());

        async Task<string> CompleteFirstBatch()
        {
            await firstResponse.Task;
            return JsonConvert.SerializeObject(firstBatch);
        }
    }

    [TestMethod]
    public async Task OrderNumbersExternalLookup_AppliesManualPagingToTheSameBatches()
    {
        var firstNumber = new string('a', 925);
        var secondNumber = new string('b', 925);
        var numbers = new[] { firstNumber, secondNumber };
        using var handler = new OrderLookupHandler(Guid.NewGuid());
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionManagerClient(httpClient);

        await client.GetOrdersByOrderNumberExternal(numbers, take: 2);
        await client.GetOrdersByOrderNumberExternal(numbers, take: 2, skip: 2);

        CollectionAssert.AreEqual(new[]
        {
            $"?take=2&skip=0&orderNumberExternal={firstNumber}", $"?take=2&skip=0&orderNumberExternal={secondNumber}",
            $"?take=2&skip=2&orderNumberExternal={firstNumber}", $"?take=2&skip=2&orderNumberExternal={secondNumber}"
        }, handler.Requests.Select(uri => uri.Query).ToArray());
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
        await Assert.ThrowsExactlyAsync<ArgumentOutOfRangeException>(() => client.GetOrdersByOrderNumberExternal(["4711"], take: 0));
        await Assert.ThrowsExactlyAsync<ArgumentOutOfRangeException>(() => client.GetOrdersByOrderNumberExternal(["4711"], take: 1001));
        await Assert.ThrowsExactlyAsync<ArgumentOutOfRangeException>(() => client.GetOrdersByOrderNumberExternal(["4711"], skip: -1));
        Assert.AreEqual(0, handler.Requests.Count);
    }

    private sealed class OrderLookupHandler(Guid orderId, Func<Uri, Task<string>>? respond = null) : HttpMessageHandler
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

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestUri = request.RequestUri!;
            lock (Requests)
            {
                Requests.Add(requestUri);
            }

            var isOrderByIdRequest = requestUri.AbsolutePath.EndsWith($"/{orderId}", StringComparison.OrdinalIgnoreCase);
            var content = respond is null ? (isOrderByIdRequest ? _orderResponse : _ordersResponse) : await respond(requestUri);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }
    }
}
