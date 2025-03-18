using System.Net.Http.Headers;
using System.Text;

using HomagConnect.Base.Extensions;

namespace HomagConnect.OrderManager.Samples.Authentication
{
    /// <summary>
    /// Authentication samples.
    /// <remarks>
    /// <see href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/OrderManager/Samples/Authentication" />
    /// for further details.
    /// </remarks>
    /// </summary>
    public static class AuthenticationSamples
    {
        /// <summary>
        /// Create OrderManager client using HttpClient.
        /// </summary>
        public static async Task CreateOrderManagerClientUsingHttpClient(Guid subscriptionId, string authorizationKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://connect.homag.cloud")
            };

            var authenticationHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

            var orderManager = new OrderManagerClient(httpClient);

            var orders = await orderManager.GetOrders(1);

            orders.Trace();
        }

        /// <summary>
        /// Create OrderManager client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateOrderManagerClientUsingSubscriptionIdAndToken(Guid subscriptionId, string authorizationKey)
        {
            var orderManager = new OrderManagerClient(subscriptionId, authorizationKey);

            var orders = await productionManager.GetOrders(1);

            orders.Trace();
        }
    }
}