using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("05d68c42-49ad-4338-91d5-e80d2c675907");

    protected IIntelliDivideClient GetIntelliDivideClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"Token: {Token.Substring(0, 4)}*");

        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), Token));

        return new IntelliDivideClient(httpClient);
    }

    protected void Trace(string o)
    {
        if (TestContext == null)
        {
            Console.WriteLine(o);
        }
        else
        {
            TestContext.WriteLine(o);
        }
    }
}