using System.Net.Http.Headers;
using System.Text;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Client;

namespace HomagConnect.IntelliDivide.Samples.Authentication
{
    /// <summary>
    /// Authentication samples.
    /// <remarks>
    /// <see href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Authentication" /> for further details.
    /// </remarks>
    /// </summary>
    public static class AuthenticationSamples
    {
        /// <summary>
        /// Create IntelliDivide client using HttpClient.
        /// </summary>
        public static async Task CreateIntelliDivideClientUsingHttpClient(Guid subscriptionId, string authorizationKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://connect.homag.cloud")
            };

            var authenticationHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

            var intelliDivide = new IntelliDivideClient(httpClient);

            var optimizations = await intelliDivide.GetOptimizationsAsync(100);

            optimizations.Trace();
        }

        /// <summary>
        /// Create IntelliDivide client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateIntelliDivideClientUsingSubscriptionIdAndToken(Guid subscriptionId, string authorizationKey)
        {
            var intelliDivide = new IntelliDivideClient(subscriptionId, authorizationKey);

            var optimizations = await intelliDivide.GetOptimizationsAsync(100);

            optimizations.Trace();
        }
    }
}