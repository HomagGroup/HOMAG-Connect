using System.Threading.Tasks;

using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Client interface for managing surface textures in MaterialManager.
/// Provides CRUD and import/update operations for textures and Roomle materials.
/// </summary>
public interface IMaterialManagerClientTextures
{
    /// <summary>
    /// Deletes a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    Task DeleteTexture(string id);

    /// <summary>
    /// Gets a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    /// <returns>The texture.</returns>
    Task<Texture> GetTexture(string id);

    /// <summary>
    /// Gets a paged list of textures using continuation token pagination.
    /// </summary>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    Task<PagedTextureResult> GetTextures(int pageSize, string? continuationToken = null);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog using continuation token pagination.
    /// </summary>
    /// <param name="catalog">Optional catalog filter.</param>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    Task<PagedTextureResult> GetTextures(string? catalog, int pageSize = 100, string? continuationToken = null);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog and decor code using continuation token pagination.
    /// </summary>
    /// <param name="catalog">Optional catalog filter.</param>
    /// <param name="decorCode">Optional decor code filter.</param>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    Task<PagedTextureResult> GetTextures(string? catalog, string? decorCode, int pageSize = 100, string? continuationToken = null);

    /// <summary>
    /// Imports or updates a single Roomle material definition with optional file references.
    /// </summary>
    /// <param name="material">The Roomle material definition.</param>
    /// <returns>The resulting texture.</returns>
    Task<Texture> ImportOrUpdate(MaterialDefinitionRoomle material);

    /// <summary>
    /// Imports or updates multiple Roomle material definitions with optional file references.
    /// </summary>
    /// <param name="materials">The Roomle materials collection.</param>
    /// <returns>The resulting textures.</returns>
    Task<Texture[]> ImportOrUpdate(MaterialDefinitionsRoomle materials);
}