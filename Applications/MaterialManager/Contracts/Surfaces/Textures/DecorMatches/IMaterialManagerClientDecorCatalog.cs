using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Client interface for searching the decor catalog in MaterialManager.
/// </summary>
public interface IMaterialManagerClientDecorCatalog
{
    /// <summary>
    /// Searches for decor candidates matching a board type.
    /// </summary>
    /// <param name="request">The board decor match search request.</param>
    /// <returns>The matching decor candidates.</returns>
    Task<DecorMatchSearchResponse> SearchBoardDecors(BoardTypeDecorMatchSearchRequest request);

    /// <summary>
    /// Searches for decor candidates matching an edgeband type.
    /// </summary>
    /// <param name="request">The edgeband decor match search request.</param>
    /// <returns>The matching decor candidates.</returns>
    Task<DecorMatchSearchResponse> SearchEdgebandDecors(EdgebandTypeDecorMatchSearchRequest request);
}
