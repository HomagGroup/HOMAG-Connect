using System.Net;
using System.Text;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Processing.Optimization;

[TestClass]
public class ProcessingOptimizationClientTests
{
    [TestMethod]
    [UnitTest("MaterialManager.Processing.Optimization")]
    public async Task GetOffcutParameterSetsAsync_EncodesMaterialCodesAndPreservesNormalization()
    {
        using var handler = new RequestCaptureHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialManagerClientProcessingOptimization(httpClient);

        await client.GetOffcutParameterSetsAsync([" M &DF#19% ", "A+B", "M &DF#19%", " "]);

        handler.RequestUri.ShouldNotBeNull();
        handler.RequestUri!.Query.ShouldBe("?materialCode=A%2BB&materialCode=M%20%26DF%2319%25");
    }

    private sealed class RequestCaptureHandler : HttpMessageHandler
    {
        public Uri? RequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]", Encoding.UTF8, "application/json")
            });
        }
    }
}
