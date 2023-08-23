using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace HomagConnect.Base.Services

{
    /// <summary>
    /// </summary>
    public class CommonServices : ServiceBase, ICommonServices
    {
        /// <summary>
        /// </summary>
        /// <param name="httpClient"></param>
        public CommonServices(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<GatewayTokenInformation> GetTokenInformationAsync()
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            var uri = $"{Prefix}common/tokenInformation";
            request.RequestUri = new Uri(uri, UriKind.Relative);
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.HandleDeprecatedMessages(request, ApiVersion, ThrowExceptionOnDeprecatedCalls, OnDeprecatedAction);
            response.EnsureSuccessStatusCodeWithDetails(request);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GatewayTokenInformation>(content, SerializerSettings.Default);
        }
    }

    /// <summary>
    /// </summary>
    public interface ICommonServices
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<GatewayTokenInformation> GetTokenInformationAsync();
    }
}