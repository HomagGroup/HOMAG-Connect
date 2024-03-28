using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

using HomagConnect.Base.Exceptions;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace HomagConnect.Base
{
    /// <summary>
    /// Extensions for response messages
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        public const string DeprecatedVersionHeaderKey = "api-deprecated-versions";

        public static void EnsureSuccessStatusCodeWithDetails(this HttpResponseMessage response,
            HttpRequestMessage request)
        {
            response.EnsureSuccessStatusCodeWithDetails(request, null);
        }

        public static void EnsureSuccessStatusCodeWithDetails(this HttpResponseMessage response,
            HttpRequestMessage request, ILogger logger)
        {
            try
            {
                response.EnsureSuccessStatusCodeWithDetailsAsync(request, logger, null).Wait();
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1)
                {
                    throw e.InnerExceptions[0];
                }

                throw;
            }
        }

        public static async Task EnsureSuccessStatusCodeWithDetailsAsync(this HttpResponseMessage response, HttpRequestMessage request,
            ILogger logger, Func<HttpStatusCode, string, Exception, Task<bool>> handleResponse)
        {
            if (request == null)
            {
                request = response.RequestMessage;
            }

            if (!response.IsSuccessStatusCode)
            {
                var resTxt = string.Empty;

                try
                {
                    resTxt = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                catch
                {
                    // If we can´t get the content, we throw the exception without content
                }

                Exception exception = null;

                if (!string.IsNullOrWhiteSpace(resTxt))
                {
                    var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(resTxt);

                    if (problemDetails != null)
                    {
                        exception = new ProblemDetailsException(problemDetails);

                        if (problemDetails.Type == nameof(ArgumentOutOfRangeException))
                        {
                            exception = new ArgumentOutOfRangeException(problemDetails.Detail, exception);
                        }
                        else if (problemDetails.Type == nameof(NotSupportedException))
                        {
                            exception = new NotSupportedException(problemDetails.Detail, exception);
                        }
                        else if (problemDetails.Type == nameof(NotImplementedException))
                        {
                            exception = new NotImplementedException(problemDetails.Detail, exception);
                        }
                        else if (problemDetails.Type == nameof(AuthenticationException))
                        {
                            exception = new AuthenticationException(problemDetails.Detail, exception);
                        }
                        else if (problemDetails.Type == nameof(ValidationException))
                        {
                            exception= new ValidationException(problemDetails.Detail, exception);
                        }
                    }
                }

                if (exception == null)
                {
                    exception = new HttpRequestException(
                        $"HTTP request '{request?.RequestUri}' failed with status code {response.StatusCode}{Environment.NewLine}Response:{Environment.NewLine}{response}{Environment.NewLine}Response-Body:{Environment.NewLine}{resTxt}{Environment.NewLine}Request:{Environment.NewLine}{request}");
                }

                if (handleResponse != null)
                {
                    if (await handleResponse.Invoke(response.StatusCode, resTxt, exception).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                // ReSharper disable once StructuredMessageTemplateProblem
                logger?.LogError(exception, "Request failed: {0}, {1}, {2}", request?.RequestUri, response.StatusCode, resTxt);

                throw exception;
            }
        }

        /// <summary>
        /// This checks the result and calls the Action on deprecated messages; optionally it throws an exception
        /// </summary>
        public static void HandleDeprecatedMessages(this HttpResponseMessage response, HttpRequestMessage request, string currentApiVersion,
            bool throwExceptionOnDeprecatedCalls = false, Action<HttpRequestMessage, HttpResponseMessage> onDeprecatedAction = null)
        {
            if (!string.IsNullOrEmpty(currentApiVersion) && response.Headers.Contains(DeprecatedVersionHeaderKey))
            {
                var values = response.Headers.GetValues(DeprecatedVersionHeaderKey);
                if (values.Any(p => p.IndexOf(currentApiVersion, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    onDeprecatedAction?.Invoke(request, response);
                    if (throwExceptionOnDeprecatedCalls)
                    {
                        throw new InvalidOperationException($"API version '{currentApiVersion}' is marked as deprecated!");
                    }
                }
            }
        }
    }
}