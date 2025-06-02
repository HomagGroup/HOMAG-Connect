using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.ProductionAssist.Client;
using HomagConnect.ProductionAssist.Contracts;
using HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;
using HomagConnect.ProductionManager.Client;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionAssist.Tests
{
    /// <summary />
    public class ProductionAssistTestBase : TestBase
    {
        protected IProductionAssistClient GetProductionAssistClient()
        {
            $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new ProductionAssistClient(httpClient);
        }

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