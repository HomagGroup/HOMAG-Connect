using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Tests
{
    /// <summary />
    public class MaterialAssistTestBase : TestBase
    {
        /// <summary />
        protected override Guid UserSecretsFolder { get; set; } = new("ef9be073-0dc1-4e86-ae1a-e07d422f31ee");

        /// <summary />
        protected MaterialAssistClient GetMaterialAssistClient()
        {
            Trace($"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey.Substring(0, 4)}*");

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new MaterialAssistClient(httpClient);
        }
    }
}