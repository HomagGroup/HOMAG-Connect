using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
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
    /// Gets a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    /// <returns>The texture.</returns>
    Task<Texture> GetTexture(string id);

    /// <summary>
    /// Gets a paged list of textures.
    /// </summary>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    Task<IEnumerable<Texture>?> GetTextures(int take, int skip = 0);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog.
    /// </summary>
    /// <param name="catalog">The catalog code.</param>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    Task<IEnumerable<Texture>?> GetTextures(string catalog, int take, int skip = 0);

    /// <summary>
    /// Gets a paged list of textures filtered by catalog and decor code.
    /// </summary>
    /// <param name="catalog">The catalog code.</param>
    /// <param name="decorCode">The decor code.</param>
    /// <param name="take">The maximum number of items to return.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <returns>A collection of textures or null if none found.</returns>
    Task<IEnumerable<Texture>?> GetTextures(string catalog, string decorCode, int take, int skip = 0);

    /// <summary>
    /// Imports or updates a single Roomle material definition with optional file references.
    /// </summary>
    /// <param name="material">The Roomle material definition.</param>
    /// <param name="fileReferences">Optional files to associate (e.g., thumbnails, textures).</param>
    /// <returns>The resulting texture.</returns>
    Task<Texture> ImportOrUpdate(MaterialDefinitionRoomle material, params FileReference[] fileReferences);

    /// <summary>
    /// Imports or updates multiple Roomle material definitions with optional file references.
    /// </summary>
    /// <param name="materials">The Roomle materials collection.</param>
    /// <param name="fileReferences">Optional files to associate (e.g., thumbnails, textures).</param>
    /// <returns>The resulting textures.</returns>
    Task<Texture[]> ImportOrUpdate(MaterialDefinitionsRoomle materials, params FileReference[] fileReferences);

    /// <summary>
    /// Deletes a texture by its identifier.
    /// </summary>
    /// <param name="id">The texture identifier.</param>
    Task DeleteTexture(string id);
}