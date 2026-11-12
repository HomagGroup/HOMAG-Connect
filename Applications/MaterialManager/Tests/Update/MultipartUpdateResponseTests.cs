using System.Net;
using System.Text;

using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update;

[TestClass]
public class MultipartUpdateResponseTests
{
    [TestMethod]
    [UnitTest("MaterialManager.Material")]
    public async Task UpdateBoardType_WithMultipartAndNullResponse_ThrowsProblemDetailsException()
    {
        using var handler = new NullResponseHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialManagerClientMaterialBoards(httpClient);

        var exception = await Should.ThrowAsync<ProblemDetailsException>(() =>
            client.UpdateBoardType("BOARD-001", new MaterialManagerUpdateBoardType(), []));

        exception.Title.ShouldBe("Invalid or no result. Process returned null for BoardType!");
    }

    private sealed class NullResponseHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("null", Encoding.UTF8, "application/json")
            });
        }
    }
}
