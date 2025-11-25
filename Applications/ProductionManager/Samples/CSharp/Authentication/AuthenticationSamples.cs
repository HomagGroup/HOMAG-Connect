using System.Net.Http.Headers;
using System.Text;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Client;

namespace HomagConnect.ProductionManager.Samples.Authentication
{
    /// <summary>
    /// Authentication samples.
    /// <remarks>
    /// <see href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/ProductionManager/Samples/Authentication" />
    /// for further details.
    /// </remarks>
    /// </summary>
    public static class AuthenticationSamples
    {
        /// <summary>
        /// Create ProductionManager client using HttpClient.
        /// </summary>
        public static async Task CreateProductionManagerClientUsingHttpClient(Guid subscriptionId, string authorizationKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://connect.homag.cloud")
            };

            var authenticationHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

            var productionManager = new ProductionManagerClient(httpClient);

            var orders = await productionManager.GetOrders(1);

            orders.Trace();
        }

        /// <summary>
        /// Create ProductionManager client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateProductionManagerClientUsingSubscriptionIdAndToken(Guid subscriptionId, string authorizationKey)
        {
            var productionManager = new ProductionManagerClient(subscriptionId, authorizationKey);

            var orders = await productionManager.GetOrders(1);

            orders.Trace();
        }
    }
}