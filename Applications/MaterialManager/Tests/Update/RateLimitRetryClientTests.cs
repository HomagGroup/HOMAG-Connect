using System.Net;
using System.Text;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update;

[TestClass]
public class RateLimitRetryClientTests
{
    private const HttpStatusCode _TooManyRequests = (HttpStatusCode)429;

    [TestMethod]
    [UnitTest("MaterialManager.Material")]
    public void RetryOnRateLimit_IsPassedToAllSubClients()
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialManagerClient(httpClient);

        client.RetryOnRateLimit.ShouldBeFalse();
        client.Material.Boards.RetryOnRateLimit.ShouldBeFalse();

        client.RetryOnRateLimit = true;

        client.Material.RetryOnRateLimit.ShouldBeTrue();
        client.Material.Boards.RetryOnRateLimit.ShouldBeTrue();
        client.Material.Edgebands.RetryOnRateLimit.ShouldBeTrue();
        client.Material.Textures.RetryOnRateLimit.ShouldBeTrue();
        client.Processing.RetryOnRateLimit.ShouldBeTrue();
        client.Processing.Optimization.RetryOnRateLimit.ShouldBeTrue();
        client.Processing.Cnc.RetryOnRateLimit.ShouldBeTrue();
        client.Textures.RetryOnRateLimit.ShouldBeTrue();
    }

    [TestMethod]
    [UnitTest("MaterialManager.Material")]
    public async Task UpdateBoardType_WithMultipart_RepeatsRequestOnRateLimit()
    {
        using var handler = new RateLimitedOnceHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new NoDelayBoardsClient(httpClient) { RetryOnRateLimit = true };

        var result = await client.UpdateBoardType("BOARD-001", new MaterialManagerUpdateBoardType(), []);

        result.ShouldNotBeNull();
        handler.Bodies.Count.ShouldBe(2);
        handler.Bodies[1].ShouldBe(handler.Bodies[0]);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(2)]);
    }

    [TestMethod]
    [UnitTest("MaterialManager.Material")]
    public async Task UpdateBoardType_WithMultipart_SendsOnlyOnceByDefault()
    {
        using var handler = new RateLimitedOnceHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new NoDelayBoardsClient(httpClient);

        var exception = await Should.ThrowAsync<HttpRequestException>(() =>
            client.UpdateBoardType("BOARD-001", new MaterialManagerUpdateBoardType(), []));

        exception.StatusCode.ShouldBe(_TooManyRequests);
        handler.Bodies.Count.ShouldBe(1);
        client.Delays.ShouldBeEmpty();
    }

    private sealed class NoDelayBoardsClient : MaterialManagerClientMaterialBoards
    {
        public NoDelayBoardsClient(HttpClient client) : base(client)
        {
        }

        public List<TimeSpan> Delays { get; } = [];

        protected override Task DelayAsync(TimeSpan delay, CancellationToken cancellationToken)
        {
            Delays.Add(delay);

            return Task.CompletedTask;
        }
    }

    private sealed class RateLimitedOnceHandler : HttpMessageHandler
    {
        public List<string> Bodies { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = await request.Content!.ReadAsStringAsync(cancellationToken);
            Bodies.Add(body);

            if (Bodies.Count == 1)
            {
                return new HttpResponseMessage(_TooManyRequests)
                {
                    Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };
        }
    }
}
