using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts.Exceptions;

using Newtonsoft.Json;

namespace HomagConnect.Base.Services
{
    /// <summary>
    /// base class for all services
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// Maximum length of query parameters
        /// </summary>
        protected const int QueryParametersMaxLength = 1900;

        /// <summary>
        /// Base route for all apis
        /// </summary>
        protected static readonly string Prefix = "api/";

        /// <summary>
        /// Default HOMAG Connect base uri.
        /// </summary>
        private readonly Uri _DefaultBaseUri = new("https://connect.homag.cloud");

        /// <summary>
        /// Creates a new instance of the service base
        /// </summary>
        protected ServiceBase(HttpClient client)
        {
            Initialize(client);
        }

        /// <summary>
        /// Creates a new instance of the service base
        /// </summary>
        protected ServiceBase(Guid subscriptionOrPartnerId, string authorizationKey)
        {
            Initialize(subscriptionOrPartnerId, authorizationKey, _DefaultBaseUri);
        }

        /// <summary>
        /// Creates a new instance of the service base
        /// </summary>
        protected ServiceBase(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri)
        {
            Initialize(subscriptionOrPartnerId, authorizationKey, baseUri);
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

        protected async Task DeleteObject(Uri uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = uri
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);
        }

        protected async Task<HttpResponseMessage> PostObject(Uri uri, StringContent content = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            return response;
        }

        protected async Task<T2> PostObject<T1, T2>(Uri uri, T1 payload)
        {
            var response = await PostObject(uri, payload);
            var rawResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T2 result = JsonConvert.DeserializeObject<T2>(rawResult, SerializerSettings.Default);

            if (object.Equals(result, default(T2)))
            {
                throw new ProblemDetailsException()
                {
                    Title = "Invalid or no result. Process returned null!"
                };
            }

            return result;
        }

        protected async Task<HttpResponseMessage> PostObject<T>(Uri uri, T payload)
        {
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            return response;
        }

        protected async Task<IEnumerable<T>> RequestEnumerableAsync<T>(IEnumerable<Uri> uris)
        {
            return (await Task.WhenAll(uris.AsParallel().Select(RequestEnumerable<T>))).SelectMany(s => s);
        }

        protected async Task<IEnumerable<T>?> RequestEnumerable<T>(Uri uri)

        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var enumerable = JsonConvert.DeserializeObject<IEnumerable<T>>(result, SerializerSettings.Default);

            return enumerable;
        }

        protected async Task<HttpResponseMessage> PatchObject(Uri uri, StringContent content = null)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            {
                Content = content
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            return response;
        }

        protected async Task<T?> RequestObject<T>(Uri uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(result, SerializerSettings.Default);

            return data;
        }

        protected async Task<byte[]> RequestRawData(Uri uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var data = await response.Content.ReadAsByteArrayAsync();

            return data;
        }

        protected static void ValidateRequiredProperties(object boardTypeRequest)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(boardTypeRequest, null, null);
            var isValid = Validator.TryValidateObject(boardTypeRequest, validationContext, validationResults, true);

            if (isValid)
            {
                return;
            }

            var errorMessages = validationResults.Select(vr => vr.ErrorMessage);
            throw new ValidationException("Required properties are missing: " + string.Join(", ", errorMessages));
        }

        private void Initialize(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = baseUri
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionOrPartnerId}:{authorizationKey}")));

            Initialize(httpClient);
        }

        private void Initialize(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));

            // Add the current culture to the accept language header
            client.DefaultRequestHeaders.AcceptLanguage.Clear();
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentCulture.Name));

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
    }
}