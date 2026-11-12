using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Exceptions;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomagConnect.Base.Client
{
    /// <summary>
    /// Extension methods for <see cref="HttpResponseMessage"/> to provide enriched error handling,
    /// logging, and deprecation header processing.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// HTTP response header key that indicates deprecated API versions.
        /// </summary>
        public const string DeprecatedVersionHeaderKey = "api-deprecated-versions";

        /// <param name="response">The HTTP response to validate.</param>
        extension(HttpResponseMessage response)
        {
            /// <summary>
            /// Ensures the response has a successful HTTP status code; otherwise throws a detailed exception.
            /// </summary>
            /// <param name="request">The original HTTP request. If null, <see cref="HttpResponseMessage.RequestMessage"/> is used.</param>
            public async Task EnsureSuccessStatusCodeWithDetailsAsync(HttpRequestMessage request)
            {
                await response.EnsureSuccessStatusCodeWithDetailsAsync(request, null);
            }

            /// <summary>
            /// Ensures the response has a successful HTTP status code; otherwise throws a detailed exception and logs the error.
            /// </summary>
            /// <param name="request">The original HTTP request. If null, <see cref="HttpResponseMessage.RequestMessage"/> is used.</param>
            /// <param name="logger">Optional logger used to record the error when the request fails.</param>
            /// <remarks>
            /// Unwraps single-inner <see cref="AggregateException"/> to throw the contained exception directly.
            /// </remarks>
            public async Task EnsureSuccessStatusCodeWithDetailsAsync(HttpRequestMessage request, ILogger logger)
            {
                try
                {
                    await response.EnsureSuccessStatusCodeWithDetailsAsync(request, logger, null);
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

            /// <summary>
            /// Ensures the response has a successful HTTP status code. On failure, attempts to parse the body
            /// as <see cref="ProblemDetails"/> and throws a specialized exception where possible.
            /// </summary>
            /// <remarks>
            /// Unrecognized problem types are retained as the inner exception of an <see cref="HttpRequestException"/>.
            /// Legacy responses without the problem JSON media type must contain a recognized type, or a combination
            /// of type, title, detail, and status fields identifying a problem. The HTTP status is included in
            /// <see cref="HttpRequestException"/> on supported target frameworks.
            /// </remarks>
            /// <param name="request">The original HTTP request. If null, <see cref="HttpResponseMessage.RequestMessage"/> is used.</param>
            /// <param name="logger">Optional logger used to record the error when the request fails.</param>
            /// <param name="handleResponse">
            /// Optional handler invoked on failure. If it returns true, the exception is suppressed.
            /// Parameters are: <see cref="HttpStatusCode"/> status, raw response body string, constructed <see cref="Exception"/>.
            /// </param>
            /// <exception cref="HttpRequestException">Thrown when the response is unsuccessful and no handler suppresses it.</exception>
            public async Task EnsureSuccessStatusCodeWithDetailsAsync(HttpRequestMessage request,
                ILogger logger, Func<HttpStatusCode, string, Exception, Task<bool>> handleResponse)
            {
                request ??= response.RequestMessage;

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
                    ProblemDetailsException problemException = null;
                    ProblemDetails problemDetails = null;

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(resTxt))
                        {
                            var json = JObject.Parse(resTxt);
                            if (IsProblemDetails(json, response.Content.Headers.ContentType?.MediaType))
                            {
                                problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(resTxt, SerializerSettings.Default);
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        // If we can´t deserialize the content, we throw the exception without content
                    }

                    if (problemDetails != null)
                    {
                        problemException = new ProblemDetailsException(problemDetails);
                        if (problemDetails.Type == nameof(ProblemDetailsException))
                        {
                            exception = problemException;
                        }
                        else if (problemDetails.Type == nameof(ArgumentOutOfRangeException))
                        {
                            exception = new ArgumentOutOfRangeException(problemDetails.Detail, problemException);
                        }
                        else if (problemDetails.Type == nameof(NotSupportedException))
                        {
                            exception = new NotSupportedException(problemDetails.Detail, problemException);
                        }
                        else if (problemDetails.Type == nameof(NotImplementedException))
                        {
                            exception = new NotImplementedException(problemDetails.Detail, problemException);
                        }
                        else if (problemDetails.Type == nameof(AuthenticationException))
                        {
                            exception = new AuthenticationException(problemDetails.Detail, problemException);
                        }
                        else if (problemDetails.Type == nameof(ValidationException))
                        {
                            exception = new ValidationException(problemDetails.Detail, problemException);
                        }
                    }

                    if (exception == null)
                    {
                        var message = $"HTTP request '{request?.RequestUri}' failed with status code {response.StatusCode}{Environment.NewLine}Response:{Environment.NewLine}{response}{Environment.NewLine}Response-Body:{Environment.NewLine}{resTxt}{Environment.NewLine}Request:{Environment.NewLine}{request}";
#if NETSTANDARD2_0
                        exception = new HttpRequestException(message, problemException);
#else
                        exception = new HttpRequestException(message, problemException, response.StatusCode);
#endif
                    }

                    if (handleResponse != null && await handleResponse.Invoke(response.StatusCode, resTxt, exception).ConfigureAwait(false))
                    {
                        return;
                    }

                    // ReSharper disable once StructuredMessageTemplateProblem
                    logger?.LogError(exception, "Request failed: {requestUri}, {statusCode}, {responseText}", request?.RequestUri, response.StatusCode, resTxt);

                    throw exception;
                }
            }

            /// <summary>
            /// Checks the response for the deprecated API versions header and optionally throws or invokes a callback.
            /// </summary>
            /// <param name="request">The originating HTTP request.</param>
            /// <param name="currentApiVersion">The current client API version string.</param>
            /// <param name="throwExceptionOnDeprecatedCalls">If true, throws when the current version is marked deprecated.</param>
            /// <param name="onDeprecatedAction">Optional action invoked when the current version is deprecated.</param>
            /// <remarks>
            /// Uses the <c>api-deprecated-versions</c> response header to detect deprecation. Comparison is case-insensitive.
            /// </remarks>
            public void HandleDeprecatedMessages(HttpRequestMessage request, string currentApiVersion,
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

        private static bool IsProblemDetails(JObject json, string mediaType)
        {
            bool HasString(string name) =>
                json.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out var value) &&
                value.Type == JTokenType.String && !string.IsNullOrWhiteSpace((string)value);

            var hasType = HasString("type");
            var hasTitle = HasString("title");
            var hasDetail = HasString("detail");
            var hasStatus = json.TryGetValue("status", StringComparison.OrdinalIgnoreCase, out var status) &&
                            status.Type == JTokenType.Integer;

            if (string.Equals(mediaType, "application/problem+json", StringComparison.OrdinalIgnoreCase))
            {
                return hasType || hasTitle || hasDetail || hasStatus ||
                       json.TryGetValue("errors", StringComparison.OrdinalIgnoreCase, out var errors) && errors is JObject;
            }

            var type = hasType ? (string)json.GetValue("type", StringComparison.OrdinalIgnoreCase) : null;
            return type == nameof(ProblemDetailsException) || type == nameof(ArgumentOutOfRangeException) ||
                   type == nameof(NotSupportedException) || type == nameof(NotImplementedException) ||
                   type == nameof(AuthenticationException) || type == nameof(ValidationException) ||
                   (hasStatus && (hasType || hasTitle || hasDetail)) ||
                   (hasType && (hasTitle || hasDetail));
        }
    }
}