using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <see cref="EdgebandTypeDetails" />.
/// </summary>
public class EdgebandTypeDetailsDecorMatchSearchRequest : DecorMatchSearchRequest
{
    /// <inheritdoc />
    public override DecorMatchMaterialType MaterialType => DecorMatchMaterialType.EdgebandDetails;

    /// <summary>
    /// Gets or sets the <see cref="EdgebandTypeDetails" /> payload used as the matching signal.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public EdgebandTypeDetails Material { get; set; } = null!;
}
