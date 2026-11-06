using System.Net;
using System.Text;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

internal sealed class MaterialValidationTestSupport : HttpMessageHandler
{
    public string? Payload { get; private set; }

    public int RequestCount { get; private set; }

    public HttpClient CreateClient() => new(this) { BaseAddress = new Uri("https://example.invalid/") };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestCount++;
        Payload = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{}", Encoding.UTF8, "application/json")
        };
    }
}