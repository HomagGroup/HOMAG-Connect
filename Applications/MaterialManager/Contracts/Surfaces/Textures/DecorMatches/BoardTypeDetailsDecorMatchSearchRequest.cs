using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <see cref="BoardTypeDetails" />.
/// </summary>
public class BoardTypeDetailsDecorMatchSearchRequest : DecorMatchSearchRequest
{
    /// <inheritdoc />
    public override DecorMatchMaterialType MaterialType => DecorMatchMaterialType.BoardDetails;

    /// <summary>
    /// Gets or sets the <see cref="BoardTypeDetails" /> payload used as the matching signal.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public BoardTypeDetails Material { get; set; } = null!;
}
