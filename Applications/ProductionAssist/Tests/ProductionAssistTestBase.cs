using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.ProductionAssist.Client;
using HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

namespace HomagConnect.ProductionAssist.Tests
{
    /// <summary />
    public class ProductionAssistTestBase : TestBase
    {
        protected IProductionAssistFeedbackClient GetProductionAssistFeedbackClient()
        {
            $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new ProductionAssistFeedbackClient(httpClient);
        }
    }
}