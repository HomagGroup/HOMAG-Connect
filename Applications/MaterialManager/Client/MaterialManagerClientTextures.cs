using HomagConnect.Base.Client;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

        var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}";
        var response = await RequestObject<Texture>(new Uri(url, UriKind.Relative));
        if (response != null)
        {
            return response;
        }

        throw new InvalidOperationException($"Texture with id '{id}' not found.");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Texture>?> GetTextures(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";
        return await RequestEnumerable<Texture>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Texture>?> GetTextures(string catalog, int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?catalog={Uri.EscapeDataString(catalog)}&take={take}&skip={skip}";
        return await RequestEnumerable<Texture>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Texture>?> GetTextures(string catalog, string decorCode, int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?catalog={Uri.EscapeDataString(catalog)}&decorCode={Uri.EscapeDataString(decorCode)}&take={take}&skip={skip}";
        return await RequestEnumerable<Texture>(new Uri(url, UriKind.Relative));
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
        var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}";
        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }
}
