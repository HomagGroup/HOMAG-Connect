using System;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Client;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for textures
/// </summary>
public class MaterialManagerClientTextures : ClientBase, IMaterialManagerClientTextures
{
    private const string _BaseRoute = "api/materialManager/surfaces/textures";

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
    public async Task<PagedTextureResult> GetTextures(int pageSize = 100, string? continuationToken = null)
    {
        var url = $"{_BaseRoute}?pageSize={pageSize}";
        if (!string.IsNullOrWhiteSpace(continuationToken))
        {
            url += $"&continuationToken={Uri.EscapeDataString(continuationToken)}";
        }

        var response = await RequestObject<PagedTextureResult>(new Uri(url, UriKind.Relative));
        return response ?? new PagedTextureResult();
    }

    /// <inheritdoc />
    public async Task<PagedTextureResult> GetTextures(string? catalog, int pageSize = 100, string? continuationToken = null)
    {
        var url = $"{_BaseRoute}?pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(catalog))
        {
            url += $"&catalog={Uri.EscapeDataString(catalog)}";
        }

        if (!string.IsNullOrWhiteSpace(continuationToken))
        {
            url += $"&continuationToken={Uri.EscapeDataString(continuationToken)}";
        }

        var response = await RequestObject<PagedTextureResult>(new Uri(url, UriKind.Relative));
        return response ?? new PagedTextureResult();
    }

    /// <inheritdoc />
    public async Task<PagedTextureResult> GetTextures(string? catalog, string? decorCode, int pageSize = 100, string? continuationToken = null)
    {
        var url = $"{_BaseRoute}?pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(catalog))
        {
            url += $"&catalog={Uri.EscapeDataString(catalog)}";
        }

        if (!string.IsNullOrWhiteSpace(decorCode))
        {
            url += $"&decorCode={Uri.EscapeDataString(decorCode)}";
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
    public async Task DeleteTexture(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        var url = $"{_BaseRoute}?id={Uri.EscapeDataString(id)}";
        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }
}