using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.MmrMobile.Client;

namespace HomagConnect.MmrMobile.Tests;

/// <summary>
/// </summary>
public class MmrTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("10225a60-2f4f-4e77-b6b5-b57926da5ad6");

    protected MmrMobileClient GetMmrMobileClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"AuthorizationKey: {AuthorizationKey[..4]}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MmrMobileClient(httpClient);
    }
}