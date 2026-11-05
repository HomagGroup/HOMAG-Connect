using System.Net;
using System.Text;

using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

using Newtonsoft.Json.Linq;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures.DecorMatches;

/// <summary>
/// Unit tests for <see cref="MaterialManagerClientTextures.SearchDecorMatchesByImageAsync" />.
/// </summary>
[TestClass]
[TemporaryDisabledOnServer(2026, 11, 15, "SmartDecor")]
public class DecorImageSearchClientTests
{
    private const string _ImageUrl = "https://example.com/images/oak.jpg";

    /// <summary>
    /// Tests that the request is sent as POST with the expected URL and JSON body.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task SearchDecorMatchesByImageAsync_SendsExpectedRequest()
    {
        var handler = new StubHandler(HttpStatusCode.OK, """{"totalCount":1,"candidates":[]}""");
        var client = CreateClient(handler);

        var response = await client.SearchDecorMatchesByImageAsync(new DecorImageSearchRequest { ImageUri = new Uri(_ImageUrl) }, 25, 5);

        handler.Request.ShouldNotBeNull();
        handler.Request.Method.ShouldBe(HttpMethod.Post);
        handler.Request.RequestUri!.AbsoluteUri.ShouldBe("https://connect.example.com/api/materialManager/catalogs/decors/search/image?take=25&skip=5");
        handler.Request.Content!.Headers.ContentType!.MediaType.ShouldBe("application/json");

        var body = JObject.Parse(handler.Body!);
        body.Properties().Select(p => p.Name).ShouldBe(["imageUri"]);
        body.Value<string>("imageUri").ShouldBe(_ImageUrl);

        response.TotalCount.ShouldBe(1);
    }

    /// <summary>
    /// Tests that the defaults take=10 and skip=0 are used in the URL.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task SearchDecorMatchesByImageAsync_UsesDefaultPaging()
    {
        var handler = new StubHandler(HttpStatusCode.OK, """{"totalCount":0,"candidates":[]}""");
        var client = CreateClient(handler);

        await client.SearchDecorMatchesByImageAsync(new DecorImageSearchRequest { ImageUri = new Uri(_ImageUrl) });

        handler.Request!.RequestUri!.Query.ShouldBe("?take=10&skip=0");
    }

    /// <summary>
    /// Tests that a null request is rejected.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task SearchDecorMatchesByImageAsync_NullRequest_Throws()
    {
        var client = CreateClient(new StubHandler(HttpStatusCode.OK, "{}"));

        await Should.ThrowAsync<ArgumentNullException>(() => client.SearchDecorMatchesByImageAsync(null!));
    }

    /// <summary>
    /// Tests that a missing, relative or non-https image URI is rejected before any request is sent.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    [DataRow(null)]
    [DataRow("images/oak.jpg")]
    [DataRow("http://example.com/oak.jpg")]
    [DataRow("ftp://example.com/oak.jpg")]
    public async Task SearchDecorMatchesByImageAsync_InvalidImageUri_Throws(string? imageUri)
    {
        var handler = new StubHandler(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);
        var request = new DecorImageSearchRequest { ImageUri = imageUri == null ? null! : new Uri(imageUri, UriKind.RelativeOrAbsolute) };

        await Should.ThrowAsync<ArgumentException>(() => client.SearchDecorMatchesByImageAsync(request));

        handler.Request.ShouldBeNull();
    }

    /// <summary>
    /// Tests that take outside 1-100 and a negative skip are rejected.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    [DataRow(0, 0)]
    [DataRow(101, 0)]
    [DataRow(-1, 0)]
    [DataRow(10, -1)]
    public async Task SearchDecorMatchesByImageAsync_InvalidPaging_Throws(int take, int skip)
    {
        var handler = new StubHandler(HttpStatusCode.OK, "{}");
        var client = CreateClient(handler);

        await Should.ThrowAsync<ArgumentOutOfRangeException>(() =>
            client.SearchDecorMatchesByImageAsync(new DecorImageSearchRequest { ImageUri = new Uri(_ImageUrl) }, take, skip));

        handler.Request.ShouldBeNull();
    }

    /// <summary>
    /// Tests that the boundary values take=1 and take=100 are accepted.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    [DataRow(1)]
    [DataRow(100)]
    public async Task SearchDecorMatchesByImageAsync_BoundaryTake_IsAccepted(int take)
    {
        var client = CreateClient(new StubHandler(HttpStatusCode.OK, """{"totalCount":0,"candidates":[]}"""));

        var response = await client.SearchDecorMatchesByImageAsync(new DecorImageSearchRequest { ImageUri = new Uri(_ImageUrl) }, take);

        response.ShouldNotBeNull();
    }

    /// <summary>
    /// Tests that an RFC 7807 problem details response is surfaced as a <see cref="ProblemDetailsException" />.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task SearchDecorMatchesByImageAsync_ProblemDetails_Throws()
    {
        var handler = new StubHandler(HttpStatusCode.BadRequest, """{"type":"ProblemDetailsException","title":"Invalid image","status":400}""", "application/problem+json");
        var client = CreateClient(handler);

        var exception = await Should.ThrowAsync<ProblemDetailsException>(() =>
            client.SearchDecorMatchesByImageAsync(new DecorImageSearchRequest { ImageUri = new Uri(_ImageUrl) }));

        exception.Title.ShouldBe("Invalid image");
    }

    private static MaterialManagerClientTextures CreateClient(HttpMessageHandler handler)
    {
        return new MaterialManagerClientTextures(new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com") });
    }

    private sealed class StubHandler(HttpStatusCode statusCode, string content, string mediaType = "application/json") : HttpMessageHandler
    {
        public string? Body { get; private set; }

        public HttpRequestMessage? Request { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            Body = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken);

            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content, Encoding.UTF8, mediaType)
            };
        }
    }
}
