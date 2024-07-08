using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Client;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace HomagConnect.ProductionAssist.Samples.Authentication
{
    /// <summary>
    /// Authentication samples.
    /// <remarks>
    /// <see href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/ProductionManager/Samples/Authentication" /> for further details.
    /// </remarks>
    /// </summary>
    public static class AuthenticationSamples
    {
        /// <summary>
        /// Create ProductionAssist client using HttpClient.
        /// </summary>
        public static async Task CreateProductionAssistClientUsingHttpClient(Guid subscriptionId, string authorizationKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://connect.homag.cloud")
            };

            var authenticationHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

            var productionAssist = new ProductionAssistFeedbackClient(httpClient);

            var workstations = await productionAssist.GetWorkstationsAsync();

            workstations.Trace();
        }

        /// <summary>
        /// Create ProductionAssist client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateProductionAssistClientUsingSubscriptionIdAndToken(Guid subscriptionId, string authorizationKey)
        {
            var productionManager = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);

            var workstations = await productionManager.GetWorkstationsAsync();

            workstations.Trace();
        }
    }

}
