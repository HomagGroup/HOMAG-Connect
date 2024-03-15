using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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

            // get the latest version / this is the default version for our APIs
            ApiVersion = versions.Default.DateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(ApiVersion) && !Client.DefaultRequestHeaders.Contains(versions.HeaderKey))
            {
                HeaderKey = versions.HeaderKey;
                Client.DefaultRequestHeaders.Add(versions.HeaderKey, ApiVersion);
            }
        }

        /// <summary>
        /// </summary>
        public string ApiVersion { get; }

        /// <summary>
        /// Client for all Requests
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// </summary>
        public string HeaderKey { get; }

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
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<T>>(result, SerializerSettings.Default);

            return data ?? Array.Empty<T>();
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

        protected async Task PostObject<T>(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);
        }

        protected async Task DeleteObject<T>(string url)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Delete };
            request.RequestUri = new Uri(url, UriKind.Relative);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);
        }
    }
}