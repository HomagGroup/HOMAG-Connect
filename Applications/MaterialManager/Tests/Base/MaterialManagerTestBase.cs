using System.Net.Http.Headers;

using HomagConnect.Base.Tests;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;

namespace HomagConnect.MaterialManager.Tests.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class MaterialManagerTestBase : TestBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override Guid UserSecretsFolder { get; set; } = new("7a028258-94b9-4d79-822a-1005e4558b74");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IMaterialManagerClientMaterialBoards GetMaterialManagerClient()
        {
            Trace($"BaseUrl: {BaseUrl}");
            Trace($"Subscription: {SubscriptionId}");
            Trace($"AuthorizationKey: {AuthorizationKey.Substring(0, 4)}*");

            var httpClient = new HttpClient
            {
                BaseAddress = BaseUrl
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

            return new MaterialManagerClientMaterialBoards(httpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        protected void Trace(string o)
        {
            if (TestContext == null)
            {
                Console.WriteLine(o);
            }
            else
            {
                TestContext.WriteLine(o);
            }
        }
    }
}
