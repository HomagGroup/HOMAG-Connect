using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MmrMobile.Client;

namespace HomagConnect.MmrMobile.Tests;

/// <summary>
/// </summary>
public class MmrTestBase : TestBase
{
    protected MmrMobileClient GetMmrMobileClient()
    {
        $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MmrMobileClient(httpClient);
    }
}