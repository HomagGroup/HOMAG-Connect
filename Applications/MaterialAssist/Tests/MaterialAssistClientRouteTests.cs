using System.Net;
using System.Text;

using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Request;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests;

[TestClass]
public class MaterialAssistClientRouteTests
{
    [TestMethod]
    public async Task CreateMaterialTypes_UsesMaterialManagerResourceRoutes()
    {
        using var handler = new RequestCaptureHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var boards = new MaterialAssistClientBoards(httpClient);
        var edgebands = new MaterialAssistClientEdgebands(httpClient);

        await boards.CreateBoardType(new MaterialManagerRequestBoardType
        {
            BoardCode = "BOARD-001",
            MaterialCode = "MAT-001",
            Length = 2800,
            Width = 2070,
            Thickness = 19
        });
        await edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "EDGE-001",
            Height = 23,
            Thickness = 0.8
        });

        handler.Requests.Count.ShouldBe(2);
        handler.Requests[0].Method.ShouldBe(HttpMethod.Post);
        handler.Requests[0].RequestUri!.AbsolutePath.ShouldBe("/api/materialManager/materials/boards");
        handler.Requests[1].Method.ShouldBe(HttpMethod.Post);
        handler.Requests[1].RequestUri!.AbsolutePath.ShouldBe("/api/materialManager/materials/edgebands");
    }

    private sealed class RequestCaptureHandler : HttpMessageHandler
    {
        public List<(HttpMethod Method, Uri? RequestUri)> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add((request.Method, request.RequestUri));
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            });
        }
    }
}
