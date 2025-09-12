using System.Net.Http.Headers;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Client;

namespace HomagConnect.MaterialManager.Tests;

/// <summary />
public class MaterialManagerTestBase : TestBase
{
    /// <summary>
    /// Gets a new instance of the <see cref="MaterialManagerClient" />.
    /// </summary>
    protected MaterialManagerClient GetMaterialManagerClient()
    {
        $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MaterialManagerClient(httpClient);
    }
}