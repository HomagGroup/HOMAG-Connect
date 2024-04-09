using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.MaterialManager.Client;

namespace HomagConnect.MaterialManager.Tests;

public class MaterialManagerTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("7a028258-94b9-4d79-822a-1005e4558b74");

    protected MaterialManagerClient GetMaterialManagerClient()
    {
        Trace($"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey.Substring(0, 4)}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MaterialManagerClient(httpClient);
    }
}