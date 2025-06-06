using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.OrderManager.Contracts.Price;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.OrderManager.Client
{
    /// <summary>
    /// Client for the price calculation
    /// </summary>
    public class PriceClient : ServiceBase, Contracts.IPriceClient
    {
        /// <inheritdoc />
        public PriceClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public PriceClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public PriceClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        /// <inheritdoc />
        public async Task<PriceResponseData> CalculatePrice(PriceRequestData requestData)
        {
            var txt = JsonConvert.SerializeObject(requestData, SerializerSettings.Default);
            var requestContent = new StringContent(txt, Encoding.UTF8);
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            using var response = await PostObject(new Uri("api/price/calculate", UriKind.Relative), requestContent);
            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PriceResponseData>(content, SerializerSettings.Default)!;
            return responseObject; 
        }
    }
}
