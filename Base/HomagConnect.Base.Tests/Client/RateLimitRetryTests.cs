using System.Net;
using System.Net.Http.Headers;
using System.Text;

using HomagConnect.Base.Client;

using Shouldly;

namespace HomagConnect.Base.Tests.Client;

[TestClass]
public class RateLimitRetryTests
{
    private const HttpStatusCode _TooManyRequests = (HttpStatusCode)429;

    [TestMethod]
    public async Task Default_SendsOnlyOneCall_AndReturnsRateLimitedResponse()
    {
        var handler = new ScriptedHandler(_ => Response(_TooManyRequests));
        var client = new TestClient(handler);

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        client.RetryOnRateLimit.ShouldBeFalse();
        response.StatusCode.ShouldBe(_TooManyRequests);
        handler.Calls.Count.ShouldBe(1);
        client.Delays.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task Enabled_RetriesUntilCallSucceeds()
    {
        var handler = new ScriptedHandler(call => Response(call < 3 ? _TooManyRequests : HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        handler.Calls.Count.ShouldBe(3);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4)]);
    }

    [TestMethod]
    public async Task Enabled_StopsAfterThreeRetriesWithIncreasingDelays()
    {
        var handler = new ScriptedHandler(_ => Response(_TooManyRequests));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(_TooManyRequests);
        handler.Calls.Count.ShouldBe(4);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(8)]);
    }

    [TestMethod]
    public async Task Enabled_ThrowsHttpRequestExceptionWithStatusCode_WhenStillRateLimited()
    {
        var handler = new ScriptedHandler(_ => Response(_TooManyRequests));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        var exception = await Should.ThrowAsync<HttpRequestException>(() => client.GetItem(new Uri("/api/items/1", UriKind.Relative)));

        exception.StatusCode.ShouldBe(_TooManyRequests);
        handler.Calls.Count.ShouldBe(4);
    }

    [TestMethod]
    public async Task Enabled_DoesNotRetryOtherErrors()
    {
        var handler = new ScriptedHandler(_ => Response(HttpStatusCode.InternalServerError));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        handler.Calls.Count.ShouldBe(1);
        client.Delays.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task Enabled_AwaitsRetryAfter_WhenLongerThanDefaultDelay()
    {
        var handler = new ScriptedHandler(call => call == 1
            ? Response(_TooManyRequests, retryAfter: new RetryConditionHeaderValue(TimeSpan.FromSeconds(10)))
            : Response(HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(10)]);
    }

    [TestMethod]
    public async Task Enabled_UsesDefaultDelay_WhenRetryAfterIsShorter()
    {
        var handler = new ScriptedHandler(call => call == 1
            ? Response(_TooManyRequests, retryAfter: new RetryConditionHeaderValue(TimeSpan.FromSeconds(1)))
            : Response(HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(2)]);
    }

    [TestMethod]
    public async Task Enabled_AwaitsRetryAfter_WhenExactlyThirtySeconds()
    {
        var handler = new ScriptedHandler(call => call == 1
            ? Response(_TooManyRequests, retryAfter: new RetryConditionHeaderValue(TimeSpan.FromSeconds(30)))
            : Response(HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        client.Delays.ShouldBe([TimeSpan.FromSeconds(30)]);
    }

    [TestMethod]
    public async Task Enabled_DoesNotRetry_WhenRetryAfterExceedsThirtySeconds()
    {
        var handler = new ScriptedHandler(_ => Response(_TooManyRequests, retryAfter: new RetryConditionHeaderValue(TimeSpan.FromSeconds(31))));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/items"));

        response.StatusCode.ShouldBe(_TooManyRequests);
        handler.Calls.Count.ShouldBe(1);
        client.Delays.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task Enabled_SendsContentAndHeadersAgain()
    {
        var handler = new ScriptedHandler(call => Response(call < 2 ? _TooManyRequests : HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/items")
        {
            Content = new StringContent("{\"name\":\"test\"}", Encoding.UTF8, "application/json")
        };
        request.Headers.Add("X-Custom", "custom-value");

        using var response = await client.SendAsync(request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        handler.Calls.Count.ShouldBe(2);

        foreach (var call in handler.Calls)
        {
            call.Method.ShouldBe(HttpMethod.Post);
            call.Uri.ShouldBe(new Uri("https://connect.example.test/api/items"));
            call.Body.ShouldBe("{\"name\":\"test\"}");
            call.ContentType.ShouldBe("application/json; charset=utf-8");
            call.Headers["X-Custom"].ShouldBe("custom-value");
        }
    }

    [TestMethod]
    public async Task Enabled_SendsMultipartContentAgain()
    {
        var handler = new ScriptedHandler(call => Response(call < 2 ? _TooManyRequests : HttpStatusCode.OK));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent("json-part"), "request");
        content.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("file-part"))), "file", "file.txt");

        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/items") { Content = content };

        using var response = await client.SendAsync(request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        handler.Calls.Count.ShouldBe(2);
        handler.Calls[1].Body.ShouldContain("json-part");
        handler.Calls[1].Body.ShouldContain("file-part");
        handler.Calls[1].ContentType.ShouldStartWith("multipart/form-data");
    }

    [TestMethod]
    public async Task Enabled_RetriesCallsOfClientMethods()
    {
        var handler = new ScriptedHandler(call => call == 1
            ? Response(_TooManyRequests)
            : Response(HttpStatusCode.OK, "{\"name\":\"item\"}"));
        var client = new TestClient(handler) { RetryOnRateLimit = true };

        var item = await client.GetItem(new Uri("api/items/1", UriKind.Relative));

        item.ShouldNotBeNull().Name.ShouldBe("item");
        handler.Calls.Count.ShouldBe(2);
        handler.Calls.ShouldAllBe(c => c.Uri == new Uri("https://connect.example.test/api/items/1"));
    }

    [TestMethod]
    public async Task Disabled_ClientMethodsFailAfterOneCall()
    {
        var handler = new ScriptedHandler(_ => Response(_TooManyRequests));
        var client = new TestClient(handler);

        var exception = await Should.ThrowAsync<HttpRequestException>(() => client.GetItem(new Uri("api/items/1", UriKind.Relative)));

        exception.StatusCode.ShouldBe(_TooManyRequests);
        handler.Calls.Count.ShouldBe(1);
    }

    [TestMethod]
    public void RetryOnRateLimit_IsPassedToChildClients()
    {
        var child = new TestClient(new ScriptedHandler(_ => Response(HttpStatusCode.OK)));
        var grandChild = new TestClient(new ScriptedHandler(_ => Response(HttpStatusCode.OK)));
        child.Children = [grandChild];
        var parent = new TestClient(new ScriptedHandler(_ => Response(HttpStatusCode.OK))) { Children = [child, null] };

        parent.RetryOnRateLimit = true;

        child.RetryOnRateLimit.ShouldBeTrue();
        grandChild.RetryOnRateLimit.ShouldBeTrue();

        parent.RetryOnRateLimit = false;

        child.RetryOnRateLimit.ShouldBeFalse();
        grandChild.RetryOnRateLimit.ShouldBeFalse();
    }

    private static HttpResponseMessage Response(HttpStatusCode statusCode, string body = "", RetryConditionHeaderValue? retryAfter = null)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        };

        response.Headers.RetryAfter = retryAfter;

        return response;
    }

    private sealed class Item
    {
        public string? Name { get; set; }
    }

    private sealed class RecordedCall
    {
        public required HttpMethod Method { get; init; }

        public required Uri? Uri { get; init; }

        public required string Body { get; init; }

        public required string? ContentType { get; init; }

        public required Dictionary<string, string> Headers { get; init; }
    }

    private sealed class ScriptedHandler : HttpMessageHandler
    {
        private readonly Func<int, HttpResponseMessage> _respond;

        public ScriptedHandler(Func<int, HttpResponseMessage> respond)
        {
            _respond = respond;
        }

        public List<RecordedCall> Calls { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Calls.Add(new RecordedCall
            {
                Method = request.Method,
                Uri = request.RequestUri,
                Body = request.Content == null ? string.Empty : await request.Content.ReadAsStringAsync(cancellationToken),
                ContentType = request.Content?.Headers.ContentType?.ToString(),
                Headers = request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value))
            });

            return _respond(Calls.Count);
        }
    }

    private sealed class TestClient : ClientBase
    {
        public TestClient(ScriptedHandler handler)
            : base(new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.test") })
        {
        }

        public IEnumerable<ClientBase?> Children { get; set; } = [];

        public List<TimeSpan> Delays { get; } = [];

        protected override IEnumerable<ClientBase?> ChildClients => Children;

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return SendRequestAsync(request);
        }

        public Task<Item?> GetItem(Uri uri)
        {
            return RequestObject<Item>(uri);
        }

        protected override Task DelayAsync(TimeSpan delay, CancellationToken cancellationToken)
        {
            Delays.Add(delay);

            return Task.CompletedTask;
        }
    }
}
