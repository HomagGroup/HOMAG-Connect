using System.Net;
using System.Text;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures.DecorMatches;

/// <summary>Tests the decor search routes used by the client.</summary>
[TestClass]
[UnitTest("MaterialManager.Textures")]
public class DecorSearchClientTests
{
    /// <summary>Details searches use their dedicated routes and request types.</summary>
    [TestMethod]
    public async Task DetailsSearches_SendTypedRequestsToTheirEndpoints()
    {
        var handler = new StubHandler();
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://connect.example.com");
        var client = new MaterialManagerClientTextures(httpClient);

        await client.SearchBoardDetailsDecors(new BoardTypeDetailsDecorMatchSearchRequest
        {
            Material = new BoardTypeDetails { BoardCode = "BOARD-001" }
        });
        handler.Path.ShouldBe("/api/materialManager/catalogs/decors/search/boards");
        JObject.Parse(handler.Body!)["materialType"]!.Value<string>().ShouldBe("BoardDetails");
        JObject.Parse(handler.Body!)["material"]!["boardCode"]!.Value<string>().ShouldBe("BOARD-001");

        await client.SearchEdgebandDetailsDecors(new EdgebandTypeDetailsDecorMatchSearchRequest
        {
            Material = new EdgebandTypeDetails { EdgebandCode = "EDGE-001" }
        });
        handler.Path.ShouldBe("/api/materialManager/catalogs/decors/search/edgebands");
        JObject.Parse(handler.Body!)["materialType"]!.Value<string>().ShouldBe("EdgebandDetails");
        JObject.Parse(handler.Body!)["material"]!["edgebandCode"]!.Value<string>().ShouldBe("EDGE-001");
    }

    /// <summary>Legacy searches continue to use the unified route.</summary>
    [TestMethod]
    public async Task LegacySearches_KeepUsingUnifiedEndpoint()
    {
        var handler = new StubHandler();
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://connect.example.com");
        var client = new MaterialManagerClientTextures(httpClient);

#pragma warning disable CS0618
        await client.SearchBoardDecors(new BoardTypeDecorMatchSearchRequest { Material = new BoardType { BoardCode = "BOARD-001" } });
        handler.Path.ShouldBe("/api/materialManager/catalogs/decors/search");
        await client.SearchEdgebandDecors(new EdgebandTypeDecorMatchSearchRequest { Material = new EdgebandType { EdgebandCode = "EDGE-001" } });
#pragma warning restore CS0618
        handler.Path.ShouldBe("/api/materialManager/catalogs/decors/search");
    }

    private sealed class StubHandler : HttpMessageHandler
    {
        public string? Path { get; private set; }
        public string? Body { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Path = request.RequestUri!.AbsolutePath;
            Body = await request.Content!.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"totalCount":0,"candidates":[]}""", Encoding.UTF8, "application/json")
            };
        }
    }
}
