using System.Net.Http.Headers;

namespace HomagConnect.Base.Client;

/// <summary>
/// Copy of a request which allows to send the same request multiple times. An <see cref="HttpRequestMessage" /> can only be sent once.
/// </summary>
internal sealed class RequestSnapshot
{
    private readonly HttpMethod _method;
    private readonly Uri? _requestUri;
    private readonly Version _version;
#if NET5_0_OR_GREATER
    private readonly HttpVersionPolicy _versionPolicy;
#endif
    private readonly List<KeyValuePair<string, string[]>> _headers;
    private readonly byte[]? _content;
    private readonly List<KeyValuePair<string, string[]>>? _contentHeaders;

    private RequestSnapshot(HttpRequestMessage request, byte[]? content)
    {
        _method = request.Method;
        _requestUri = request.RequestUri;
        _version = request.Version;
#if NET5_0_OR_GREATER
        _versionPolicy = request.VersionPolicy;
#endif
        _headers = CopyHeaders(request.Headers);
        _content = content;

        if (request.Content != null)
        {
            // The content length is calculated for every content instance.
            _contentHeaders = CopyHeaders(request.Content.Headers)
                .Where(header => !string.Equals(header.Key, "Content-Length", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    /// <summary>
    /// Captures the request. The snapshot must be created before the request is sent.
    /// </summary>
    internal static async Task<RequestSnapshot> CreateAsync(HttpRequestMessage request)
    {
        byte[]? content = null;

        if (request.Content != null)
        {
            content = await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        return new RequestSnapshot(request, content);
    }

    /// <summary>
    /// Creates a new request, which can be sent.
    /// </summary>
    internal HttpRequestMessage CreateRequest()
    {
        var request = new HttpRequestMessage(_method, _requestUri)
        {
            Version = _version
        };

#if NET5_0_OR_GREATER
        request.VersionPolicy = _versionPolicy;
#endif

        foreach (var header in _headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (_content != null)
        {
            var content = new ByteArrayContent(_content);

            foreach (var header in _contentHeaders!)
            {
                content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            request.Content = content;
        }

        return request;
    }

    private static List<KeyValuePair<string, string[]>> CopyHeaders(HttpHeaders headers)
    {
        return headers.Select(header => new KeyValuePair<string, string[]>(header.Key, header.Value.ToArray())).ToList();
    }
}
