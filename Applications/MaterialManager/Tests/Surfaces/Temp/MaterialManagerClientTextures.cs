using System.Collections.Concurrent;

using HomagConnect.Base.Contracts;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// ReSharper disable LocalizableElement
// TODO: Remove this temporary test implementation when a real one is available

namespace HomagConnect.MaterialManager.Tests.Surfaces.Temp;

/// <summary>
/// Minimal in-memory implementation for <see cref="IMaterialManagerClientTextures" /> used in tests.
/// Stores textures in a local dictionary and supports basic filtering and paging.
/// </summary>
public class MaterialManagerClientTextures : IMaterialManagerClientTextures
{
    private readonly ConcurrentDictionary<string, Texture> _Store = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets the blob storage connection string.
    /// </summary>
    private string? BlobStorageConnectionString { get; }

    /// <summary>
    /// Gets the subscription ID for this client instance.
    /// </summary>
    private Guid SubscriptionId { get; }

    /// <summary>
    /// Gets a <see cref="TextureStorageManager"/> for this client instance.
    /// </summary>
    public TextureStorageManager TextureStorageManager
    {
        get
        {
            return new TextureStorageManager(
                new TextureStorageManager.StorageConfiguration
                {
                    BlobStorageConnectionString = BlobStorageConnectionString
                },
                SubscriptionId);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialManagerClientTextures"/> class.
    /// </summary>
    /// <param name="blobStorageConnectionString">The blob storage connection string.</param>
    /// <param name="subscriptionId">The subscription identifier.</param>
    public MaterialManagerClientTextures(string? blobStorageConnectionString, Guid subscriptionId)
    {
        BlobStorageConnectionString = blobStorageConnectionString;
        SubscriptionId = subscriptionId;
    }

    /// <summary>
    /// Deletes a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    public Task DeleteTexture(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        _Store.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    /// <returns>The texture if found; otherwise, throws <see cref="KeyNotFoundException"/>.</returns>
    public Task<Texture> GetTexture(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        if (_Store.TryGetValue(id, out var texture))
        {
            return Task.FromResult(texture);
        }

        throw new KeyNotFoundException($"Texture '{id}' was not found.");
    }

    /// <summary>
    /// Gets a paged list of all textures.
    /// </summary>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    public Task<IEnumerable<Texture>?> GetTextures(int take, int skip = 0)
    {
        ValidatePaging(take, skip);
        var items = _Store.Values
            .OrderBy(t => t.Id)
            .Skip(skip)
            .Take(take)
            .ToArray();

        return Task.FromResult(items.Length == 0 ? null : items.AsEnumerable());
    }

    /// <summary>
    /// Gets a paged list of textures filtered by catalog.
    /// </summary>
    /// <param name="catalog">The catalog code.</param>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    public Task<IEnumerable<Texture>?> GetTextures(string catalog, int take, int skip = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(catalog);
        ValidatePaging(take, skip);

        var items = _Store.Values
            .Where(t => string.Equals(t.Catalog, catalog, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.Id)
            .Skip(skip)
            .Take(take)
            .ToArray();

        return Task.FromResult(items.Length == 0 ? null : items.AsEnumerable());
    }

    /// <summary>
    /// Gets a paged list of textures filtered by catalog and decor code.
    /// </summary>
    /// <param name="catalog">The catalog code.</param>
    /// <param name="decorCode">The decor code.</param>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    public Task<IEnumerable<Texture>?> GetTextures(string catalog, string decorCode, int take, int skip = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(catalog);
        ArgumentException.ThrowIfNullOrWhiteSpace(decorCode);
        ValidatePaging(take, skip);

        var items = _Store.Values
            .Where(t => string.Equals(t.Catalog, catalog, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(t.DecorCode, decorCode, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.Id)
            .Skip(skip)
            .Take(take)
            .ToArray();

        return Task.FromResult(items.Length == 0 ? null : items.AsEnumerable());
    }

    /// <summary>
    /// Imports or updates a texture based on the provided Roomle material definition.
    /// </summary>
    /// <param name="materialDefinitionRoomle">The Roomle material definition.</param>
    /// <param name="fileReferences">Optional file references.</param>
    /// <returns>The resulting texture.</returns>
    public async Task<Texture> ImportOrUpdate(MaterialDefinitionRoomle materialDefinitionRoomle, params FileReference[] fileReferences)
    {
        var texture = await materialDefinitionRoomle.MapToTextureAndUploadReferenceFiles(this.TextureStorageManager, fileReferences);

        _Store[texture.Id] = texture;

        return texture;
    }

    /// <summary>
    /// Imports or updates multiple textures based on the provided Roomle material definitions.
    /// </summary>
    /// <param name="materials">The Roomle materials collection.</param>
    /// <param name="fileReferences">Optional file references.</param>
    /// <returns>An array of resulting textures.</returns>
    public async Task<Texture[]> ImportOrUpdate(MaterialDefinitionsRoomle materials, params FileReference[] fileReferences)
    {
        if (materials is null)
            throw new ArgumentNullException(nameof(materials));

        if (materials.Materials is null || materials.Materials.Count == 0)
            throw new ArgumentException("No materials provided.", nameof(materials));

        var tasks = materials.Materials.Select(material =>
        {
            var def = new MaterialDefinitionRoomle { Material = material };
            return ImportOrUpdate(def, fileReferences);
        });

        return await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <summary>
    /// Validates paging parameters.
    /// </summary>
    /// <param name="take">The number of items to take.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if take or skip are out of range.</exception>
    private static void ValidatePaging(int take, int skip)
    {
        if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take), take, "take must be > 0");
        if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip), skip, "skip must be >= 0");
    }
}