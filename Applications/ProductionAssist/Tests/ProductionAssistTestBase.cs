using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.ProductionAssist.Client;
using HomagConnect.ProductionAssist.Contracts;

namespace HomagConnect.ProductionAssist.Tests
{
    /// <summary />
    public class ProductionAssistTestBase : TestBase
    {
        protected override Guid UserSecretsFolder { get; set; } = new("83853b7c-5e98-4dfa-9a8a-9f123310e6b0");

        protected IProductionAssistFeedbackClient GetProductionAssistFeedbackClient()
        {
            Trace($"BaseUrl: {BaseUrl}");
            Trace($"Subscription: {SubscriptionId}");
            Trace($"AuthorizationKey: {AuthorizationKey[..4]}*");

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new ProductionAssistFeedbackClient(httpClient);
        }
    }
}