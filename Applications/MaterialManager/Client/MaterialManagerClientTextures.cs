using HomagConnect.Base.Client;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for textures
/// </summary>
public class MaterialManagerClientTextures : ClientBase, IMaterialManagerClientTextures, IMaterialManagerClientDecorCatalog
{
    private const string _BaseRoute = "api/materialManager/surfaces/textures";
    private const string _DecorSearchRoute = "api/materialManager/catalogs/decors/search";

        private const string _DecorImageSearchRoute = "api/materialManager/catalogs/decors/search/image";
    /// <inheritdoc />
    public MaterialManagerClientTextures(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public MaterialManagerClientTextures(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

    /// <inheritdoc />
    public MaterialManagerClientTextures(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

    /// <inheritdoc />
    public async Task<Texture> GetTexture(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var url = $"{_BaseRoute}/byId?id={Uri.EscapeDataString(id)}";
        var response = await RequestObject<Texture>(new Uri(url, UriKind.Relative));
        return response ?? throw new InvalidOperationException($"Texture with id '{id}' not found.");
    }

    /// <inheritdoc />
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    public Task<PagedTextureResult> GetTextures(int pageSize = 100, string? continuationToken = null)
        => GetTextures(new TextureFilter(), pageSize, continuationToken);

    /// <inheritdoc />
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    public Task<PagedTextureResult> GetTextures(string? catalog, int pageSize = 100, string? continuationToken = null)
        => GetTextures(new TextureFilter { Catalog = catalog }, pageSize, continuationToken);

    /// <inheritdoc />
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    public Task<PagedTextureResult> GetTextures(string? catalog, string? decorCode, int pageSize = 100, string? continuationToken = null)
        => GetTextures(new TextureFilter { Catalog = catalog, DecorCode = decorCode }, pageSize, continuationToken);

    /// <inheritdoc />
    public async Task<PagedTextureResult> GetTextures(TextureFilter? filter = null, int pageSize = 100, string? continuationToken = null)
    {
        var url = $"{_BaseRoute}?pageSize={pageSize}";

        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Catalog))
            {
                url += $"&catalog={Uri.EscapeDataString(filter.Catalog)}";
            }

            if (!string.IsNullOrWhiteSpace(filter.DecorCode))
            {
                url += $"&decorCode={Uri.EscapeDataString(filter.DecorCode)}";
            }

            if (!string.IsNullOrWhiteSpace(filter.Embossing))
            {
                url += $"&embossing={Uri.EscapeDataString(filter.Embossing)}";
            }
        }

        if (!string.IsNullOrWhiteSpace(continuationToken))
        {
            url += $"&continuationToken={Uri.EscapeDataString(continuationToken)}";
        }

        var response = await RequestObject<PagedTextureResult>(new Uri(url, UriKind.Relative));
        return response ?? new PagedTextureResult();
    }

    /// <inheritdoc />
    public async Task<Texture> ImportOrUpdate(MaterialDefinitionRoomle material)
    {
        if (material == null)
        {
            throw new ArgumentNullException(nameof(material));
        }

        var texture = await PostObject<MaterialDefinitionRoomle, Texture>(
            new Uri($"{_BaseRoute}/import", UriKind.Relative), material);

        return texture;
    }

    /// <inheritdoc />
    public async Task<Texture[]> ImportOrUpdate(MaterialDefinitionsRoomle materials)
    {
        if (materials == null)
        {
            throw new ArgumentNullException(nameof(materials));
        }

        var texture = await PostObject<MaterialDefinitionsRoomle, Texture[]>(
            new Uri($"{_BaseRoute}/import/batch", UriKind.Relative), materials);

        return texture;
    }

    /// <inheritdoc />
    public Task<DecorMatchSearchResponse> SearchBoardDecors(BoardTypeDecorMatchSearchRequest request)
        => SearchDecors(request);

    /// <inheritdoc />
    public Task<DecorMatchSearchResponse> SearchEdgebandDecors(EdgebandTypeDecorMatchSearchRequest request)
        => SearchDecors(request);

    private async Task<DecorMatchSearchResponse> SearchDecors(DecorMatchSearchRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        ValidateRequiredProperties(request);

        var result = await PostObject<DecorMatchSearchRequest, DecorMatchSearchResponse>(
            new Uri(_DecorSearchRoute, UriKind.Relative), request);

        return result ?? new DecorMatchSearchResponse();
    }

    /// <inheritdoc />
    public async Task<DecorMatchSearchResponse> SearchDecorMatchesByImageAsync(DecorImageSearchRequest request, int take = 10, int skip = 0, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var imageUri = request.ImageUri;

        if (imageUri == null || !imageUri.IsAbsoluteUri || !string.Equals(imageUri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("ImageUri must be an absolute https URI.", nameof(request));
        }

        if (take < 1 || take > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "take must be between 1 and 100.");
        }

        if (skip < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(skip), skip, "skip must not be negative.");
        }

        var url = string.Format(CultureInfo.InvariantCulture, "{0}?take={1}&skip={2}", _DecorImageSearchRoute, take, skip);

        var payload = JsonConvert.SerializeObject(request, SerializerSettings.Default);

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.Relative))
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        };

        using var response = await Client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessStatusCodeWithDetailsAsync(httpRequest).ConfigureAwait(false);

        var rawResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = JsonConvert.DeserializeObject<DecorMatchSearchResponse>(rawResult, SerializerSettings.Default);

        return result ?? throw new ProblemDetailsException { Title = "Invalid or no result. Process returned null!" };
    }

    /// <inheritdoc />
    public async Task DeleteTexture(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        var url = $"{_BaseRoute}?id={Uri.EscapeDataString(id)}";
        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }
}