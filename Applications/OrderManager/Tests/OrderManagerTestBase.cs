using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.OrderManager.Client;
using HomagConnect.OrderManager.Contracts;

namespace HomagConnect.OrderManager.Tests;

public class OrderManagerTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("c3e9e88b-1b92-431e-b66e-7783c9dbac3c");

    protected IOrderManagerClient GetOrderManagerClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"AuthorizationKey: {AuthorizationKey[..4]}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new OrderManagerClient(httpClient);
    }
}