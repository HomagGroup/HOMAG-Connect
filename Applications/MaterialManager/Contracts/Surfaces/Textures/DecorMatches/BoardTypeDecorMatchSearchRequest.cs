using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <see cref="BoardType" />.
/// </summary>
public class BoardTypeDecorMatchSearchRequest : DecorMatchSearchRequest
{
    /// <inheritdoc />
    public override DecorMatchMaterialType MaterialType => DecorMatchMaterialType.Board;

    /// <summary>
    /// Gets or sets the <see cref="BoardType" /> payload used as the matching signal.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public BoardType Material { get; set; } = null!;
}
