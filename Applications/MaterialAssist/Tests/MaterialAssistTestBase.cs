using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Client;

namespace HomagConnect.MaterialAssist.Tests
{
    /// <summary />
    public class MaterialAssistTestBase : TestBase
    {
        /// <summary />
        protected MaterialAssistClient GetMaterialAssistClient()
        {
            $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new MaterialAssistClient(httpClient);
        }

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
}