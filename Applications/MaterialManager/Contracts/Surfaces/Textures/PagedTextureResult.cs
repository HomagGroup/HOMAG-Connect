using System.Collections.Generic;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

/// <summary>
/// Result of a paged texture query using continuation token pagination.
/// </summary>
public class PagedTextureResult
{
    /// <summary>
    /// The textures in the current page.
    /// </summary>
    public IReadOnlyList<Texture> Textures { get; set; } = [];

    /// <summary>
    /// The continuation token for fetching the next page.
    /// Null when there are no more pages.
    /// </summary>
    public string? ContinuationToken { get; set; }
}
