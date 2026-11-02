using System;
using System.Threading;
using System.Threading.Tasks;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
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
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    Task<PagedTextureResult> GetTextures(int pageSize = 100, string? continuationToken = null);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog using continuation token pagination.
    /// </summary>
    /// <param name="catalog">Optional catalog filter.</param>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    Task<PagedTextureResult> GetTextures(string? catalog, int pageSize = 100, string? continuationToken = null);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog and decor code using continuation token pagination.
    /// </summary>
    /// <param name="catalog">Optional catalog filter.</param>
    /// <param name="decorCode">Optional decor code filter.</param>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    [Obsolete("Use GetTextures(TextureFilter, ...) instead.")]
    Task<PagedTextureResult> GetTextures(string? catalog, string? decorCode, int pageSize = 100, string? continuationToken = null);

    /// <summary>
    /// Gets a paged list of textures with optional filtering using continuation token pagination.
    /// </summary>
    /// <param name="filter">
    /// Optional filter criteria. Pass <see langword="null" /> or an empty <see cref="TextureFilter" /> to return all textures.
    /// Any combination of filter properties is supported.
    /// </param>
    /// <param name="pageSize">The maximum number of items to return. Defaults to 100.</param>
    /// <param name="continuationToken">Optional continuation token for paged traversal.</param>
    /// <returns>A paged result containing textures and optional continuation token for the next page.</returns>
    Task<PagedTextureResult> GetTextures(TextureFilter? filter = null, int pageSize = 100, string? continuationToken = null);

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

    /// <summary>
    /// Searches for decors that match an image. The server downloads the image from
    /// <see cref="DecorImageSearchRequest.ImageUri" /> and returns the best matching decors.
    /// </summary>
    /// <param name="request">The request containing the public <c>https</c> URI of a PNG or JPEG image (max. 10 MB).</param>
    /// <param name="take">The maximum number of decors to return (1 to 100). Defaults to 10.</param>
    /// <param name="skip">The number of decors to skip, for paging through results. Must not be negative. Defaults to 0.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching decor candidates.</returns>
    /// <remarks>
    /// Calls <c>POST materialManager/catalogs/decors/search/image?take={take}&amp;skip={skip}</c>.
    /// The server responds with <c>200</c> and a <see cref="DecorMatchSearchResponse" />, <c>400</c> for a missing or invalid
    /// <c>imageUri</c>, an invalid image or invalid <c>take</c>/<c>skip</c>, and <c>401</c> for invalid Basic authentication.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="request" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException"><see cref="DecorImageSearchRequest.ImageUri" /> is missing, not absolute or not <c>https</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="take" /> is outside 1 to 100 or <paramref name="skip" /> is negative.</exception>
    Task<DecorMatchSearchResponse> SearchDecorMatchesByImageAsync(DecorImageSearchRequest request, int take = 10, int skip = 0, CancellationToken cancellationToken = default);
}