using System.Net;
using System.Text;

using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests.Constraints;

internal sealed class MaterialValidationTestSupport : HttpMessageHandler
{
    public string? Payload { get; private set; }

    public int RequestCount { get; private set; }

    public string Response { get; set; } = "{}";

    public static MaterialManagerRequestBoardType Board() => new()
    {
        BoardCode = "board",
        MaterialCode = "material",
        Length = 2800,
        Width = 2070,
        Thickness = 19,
        Density = 650
    };

    public HttpClient CreateClient() => new(this) { BaseAddress = new Uri("https://example.invalid/") };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestCount++;
        var content = request.Content is MultipartFormDataContent multipart ? multipart.First() : request.Content;
        Payload = content == null ? null : await content.ReadAsStringAsync(cancellationToken);
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(Response, Encoding.UTF8, "application/json")
        };
    }
}