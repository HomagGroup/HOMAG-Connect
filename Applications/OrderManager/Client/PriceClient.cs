using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.OrderManager.Contracts.Price;
using Newtonsoft.Json;
using System;
using System.Globalization;
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
            const string uri = "api/price/calculate";
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri, UriKind.Relative));
            var txt = JsonConvert.SerializeObject(requestData, SerializerSettings.Default);
            var requestContent = new StringContent(txt, Encoding.UTF8);
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            request.Content = requestContent;

            using var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(content);
            var responseObject = JsonConvert.DeserializeObject<PriceResponseData>(await reader.ReadToEndAsync())!;
            return responseObject;
        }
    }
}
