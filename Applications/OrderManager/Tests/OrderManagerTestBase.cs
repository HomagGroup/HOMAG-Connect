using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.OrderManager.Client;
using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Tests;

public class OrderManagerTestBase : TestBase
{
    protected IOrderManagerClient GetOrderManagerClient()
    {
        $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new OrderManagerClient(httpClient);
    }
}