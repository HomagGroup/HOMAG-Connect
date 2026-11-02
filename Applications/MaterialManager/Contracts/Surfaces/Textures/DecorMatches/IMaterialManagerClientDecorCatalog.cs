using System;
using System.Threading;
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

    /// <summary>
    /// Searches for decor candidates matching a board type with details.
    /// </summary>
    /// <param name="request">The board details decor match search request.</param>
    /// <returns>The matching decor candidates.</returns>
    Task<DecorMatchSearchResponse> SearchBoardDetailsDecors(BoardTypeDetailsDecorMatchSearchRequest request);

    /// <summary>
    /// Searches for decor candidates matching an edgeband type with details.
    /// </summary>
    /// <param name="request">The edgeband details decor match search request.</param>
    /// <returns>The matching decor candidates.</returns>
    Task<DecorMatchSearchResponse> SearchEdgebandDetailsDecors(EdgebandTypeDetailsDecorMatchSearchRequest request);

    /// <summary>
    /// Searches for decors that match an image. The server downloads the image from
    /// <see cref="DecorImageSearchRequest.ImageUri" /> and returns the best matching decors.
    /// </summary>
    /// <param name="request">The request containing the public <c>https</c> URI of a PNG or JPEG image (max. 10 MB).</param>
    /// <param name="take">The maximum number of decors to return (1 to 100). Defaults to 10.</param>
    /// <param name="skip">The number of decors to skip, for paging through results. Must not be negative. Defaults to 0.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching decor candidates.</returns>
      Task<DecorMatchSearchResponse> SearchDecorMatchesByImageAsync(DecorImageSearchRequest request, int take = 10, int skip = 0, CancellationToken cancellationToken = default);
}
