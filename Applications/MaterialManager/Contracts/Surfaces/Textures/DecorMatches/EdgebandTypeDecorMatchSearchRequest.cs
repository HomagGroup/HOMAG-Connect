using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <see cref="EdgebandType" />.
/// </summary>
public class EdgebandTypeDecorMatchSearchRequest : DecorMatchSearchRequest
{
    /// <inheritdoc />
    public override DecorMatchMaterialType MaterialType => DecorMatchMaterialType.Edgeband;

    /// <summary>
    /// Gets or sets the <see cref="EdgebandType" /> payload used as the matching signal.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public EdgebandType Material { get; set; } = null!;
}
