using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace HomagConnect.Base.Services
{
    /// <summary>
    /// base class for all services
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// Base route for all apis
        /// </summary>
        protected static readonly string Prefix = "api/";

        /// <summary>
        /// CTOR : Store the HttpClient for all requests
        /// </summary>
        /// <param name="client"></param>
        protected ServiceBase(HttpClient client)
        {
            Initialize(client);
        }

        protected ServiceBase(Guid subscriptionId, string accessToken, string homagConnectUrl = "https://connect.homag.cloud")
        {
            Initialize(subscriptionId, accessToken, null, homagConnectUrl);
        }

        protected ServiceBase(Guid subscriptionId, string accessToken, Guid partnerId, string homagConnectUrl = "https://connect.homag.cloud")
        {
            Initialize(subscriptionId, accessToken, null, homagConnectUrl);
        }

        private void Initialize(Guid subscriptionId, string accessToken, Guid? partnerId, string homagConnectUrl)
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(homagConnectUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionId}:{accessToken}")));

            if (partnerId.HasValue)
            {
                httpClient.DefaultRequestHeaders.Add("PartnerId", partnerId.Value.ToString());
            }

            Initialize(httpClient);
        }

        private void Initialize(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));

            // Read the version file
            var assembly = typeof(ServiceBase).Assembly;
            var verName = typeof(ServiceBase).Namespace + ".versions.json";
            var verStream = assembly.GetManifestResourceStream(verName);
            if (verStream == null)
            {
                verStream = new MemoryStream();
                var streamWriter = new StreamWriter(verStream);
                streamWriter.Write(
                    "{'versions': [{'DateTime': '2023-10-27', 'IsDeprecated': false}]}");
                streamWriter.Flush();
                verStream.Position = 0;
            }

            var verStr = new StreamReader(verStream);
            var versions = JsonConvert.DeserializeObject<VersionInformation>(verStr.ReadToEnd());

            if (versions != null)
            {
                // get the latest version / this is the default version for our APIs
                ApiVersion = versions.Default.DateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(ApiVersion) && !Client.DefaultRequestHeaders.Contains(versions.HeaderKey))
                {
                    HeaderKey = versions.HeaderKey;
                    Client.DefaultRequestHeaders.Add(versions.HeaderKey, ApiVersion);
                }
            }
        }

        /// <summary>
        /// </summary>
        public string ApiVersion { get; private set; }

        /// <summary>
        /// Client for all Requests
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// </summary>
        public string HeaderKey { get; private set; }

        /// <summary>
        /// This is called, if the response is marked as deprecated
        /// </summary>
        public Action<HttpRequestMessage, HttpResponseMessage> OnDeprecatedAction { get; set; }

        /// <summary>
        /// If this is <c>true</c>, then the methods will throw an exception, if the response from the server
        /// indicates, that this api version is marked as deprecated
        /// </summary>
        public bool ThrowExceptionOnDeprecatedCalls { get; set; }

        protected async Task<IEnumerable<T>> RequestEnumerable<T>(string url)
        {
            return await RequestAsyncEnumerable<T>(url).ToListAsync();
        }

        protected async IAsyncEnumerable<T> RequestAsyncEnumerable<T>(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var enumerable = JsonConvert.DeserializeObject<IEnumerable<T>>(result, SerializerSettings.Default);

            if (enumerable == null)
            {
                yield break;
            }

            foreach (var item in enumerable)
            {
                yield return item;
            }
        }

        protected async Task<T> RequestObject<T>(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(result, SerializerSettings.Default);

            return data;
        }

        protected async Task<byte[]> RequestRawData(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var data = await response.Content.ReadAsByteArrayAsync();

            return data;
        }

        protected async Task PostObject(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);
        }

        protected async Task DeleteObject(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Delete };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);
        }
    }
}