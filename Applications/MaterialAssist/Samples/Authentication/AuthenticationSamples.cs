using System.Net.Http.Headers;
using System.Text;

using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Authentication
{
    /// <summary>
    /// Authentication samples.
    /// <remarks>
    /// <see href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/MaterialAssist/Samples/Authentication" /> for further details.
    /// </remarks>
    /// </summary>
    public static class AuthenticationSamples
    {
        /// <summary>
        /// Create IntelliDivide client using HttpClient.
        /// </summary>
        public static async Task CreateMaterialAssistClientUsingHttpClient(Guid subscriptionId, string authorizationKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://connect.homag.cloud")
            };

            var authenticationHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{authorizationKey}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

            var materialAssist = new MaterialAssistClient(httpClient);

            var boardEntities = await materialAssist.Boards.GetBoardEntities(100);

            boardEntities.Trace();
        }

        /// <summary>
        /// Create IntelliDivide client using subscription id and authorization key.
        /// </summary>
        public static async Task CreateMaterialAssistClientUsingSubscriptionIdAndToken(Guid subscriptionId, string authorizationKey)
        {
            var materialAssist = new MaterialAssistClient(subscriptionId, authorizationKey);

            var boardEntities = await materialAssist.Boards.GetBoardEntities(100);

            boardEntities.Trace();
        }
    }
}