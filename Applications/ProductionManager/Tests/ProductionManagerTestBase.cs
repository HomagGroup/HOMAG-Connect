using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.ProductionManager.Client;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Tests;

public class ProductionManagerTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("2f18cd1a-9f4a-4fa9-92b7-a6ca29955613");

    protected IProductionManagerClient GetProductionManagerClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"AuthorizationKey: {AuthorizationKey[..4]}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new ProductionManagerClient(httpClient);
    }
}