using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.ProductionManager.Client;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Tests;

public class ProductionManagerTestBase : TestBase
{
    protected IProductionManagerClient GetProductionManagerClient()
    {
        $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();
        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new ProductionManagerClient(httpClient);
    }
}