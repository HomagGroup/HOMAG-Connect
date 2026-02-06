using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Client;
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

            var productionAssist = new ProductionAssistCuttingClient(httpClient);

            var cuttingJobs = await productionAssist.GetCuttingJobs();

            cuttingJobs.Trace();
        }

        /// <summary>
        /// Create ProductionAssist client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateProductionAssistClientUsingSubscriptionIdAndAuthorizationKey(Guid subscriptionId, string authorizationKey)
        {
            var productionAssist = new ProductionAssistCuttingClient(subscriptionId, authorizationKey);

            var cuttingJobs = await productionAssist.GetCuttingJobs();

            cuttingJobs.Trace();
        }
    }

}
