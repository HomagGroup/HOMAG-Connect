using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for textures
/// </summary>
public class MaterialManagerClientTextures : ServiceBase, IMaterialManagerClientTextures
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
    public async Task<Texture> ImportOrUpdate(MaterialDefinitionRoomle material, params FileReference[] fileReferences)
    {
        if (material == null)
        {
            throw new ArgumentNullException(nameof(material));
        }

        if (fileReferences == null)
        {
            throw new ArgumentNullException(nameof(fileReferences));
        }

        var missingFile = fileReferences.FirstOrDefault(f => !f.FileInfo.Exists);
        if (missingFile != null)
        {
            throw new FileNotFoundException($"File '{missingFile.FileInfo.FullName}' was not found.");
        }

        var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));
        if (missingReference != null)
        {
            throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_BaseRoute}/import", UriKind.Relative));
        using var httpContent = new MultipartFormDataContent();

        var json = JsonConvert.SerializeObject(material, SerializerSettings.Default);
        httpContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), "material");

        foreach (var fileReference in fileReferences)
        {
            var fileStream = fileReference.FileInfo.OpenRead();
            HttpContent streamContent = new StreamContent(fileStream);
            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }

        request.Content = httpContent;
        var response = await Client.SendAsync(request);
        await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        var texture = JsonConvert.DeserializeObject<Texture>(result, SerializerSettings.Default);
        if (texture != null)
        {
            return texture;
        }
        throw new InvalidOperationException("The returned object is not of type Texture or is null.");
    }

    /// <inheritdoc />
    public async Task<Texture[]> ImportOrUpdate(MaterialDefinitionsRoomle materials, params FileReference[] fileReferences)
    {
        if (materials == null)
        {
            throw new ArgumentNullException(nameof(materials));
        }

        if (fileReferences == null)
        {
            throw new ArgumentNullException(nameof(fileReferences));
        }

        var missingFile = fileReferences.FirstOrDefault(f => !f.FileInfo.Exists);
        if (missingFile != null)
        {
            throw new FileNotFoundException($"File '{missingFile.FileInfo.FullName}' was not found.");
        }

        var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));
        if (missingReference != null)
        {
            throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{_BaseRoute}/import/batch", UriKind.Relative));
        using var httpContent = new MultipartFormDataContent();

        var json = JsonConvert.SerializeObject(materials, SerializerSettings.Default);
        httpContent.Add(new StringContent(json, Encoding.UTF8, "application/json"), "materials");

        foreach (var fileReference in fileReferences)
        {
            var fileStream = fileReference.FileInfo.OpenRead();
            HttpContent streamContent = new StreamContent(fileStream);
            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }

        request.Content = httpContent;
        var response = await Client.SendAsync(request);
        await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Texture[]>(result, SerializerSettings.Default) ?? [];
    }

    /// <inheritdoc />
    public async Task DeleteTexture(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}";
        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }
}
