using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search the shared Texture Catalog for decor candidates that match a given
/// <see cref="EdgebandType" />.
/// </summary>
/// <example>
/// { "materialType": "Edgeband", "material": { "edgebandCode": "EDGE-001",
/// "height": 23.0, "thickness": 0.8, "defaultLength": 100.0, "materialCategory": "ABS",
/// "process": "HotmeltGlue", "unitSystem": "Metric" }, "searchTerm": "Oak",
/// "culture": "en-US", "skip": 0, "take": 50 }
/// </example>
public class EdgebandTypeDecorMatchSearchRequest : DecorMatchSearchRequest
{
    /// <summary>
    /// Edgeband material and dimensions used as the matching signal.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public EdgebandType Material { get; set; } = null!;

    /// <summary>
    /// Identifies this request as an edgeband search; its exchanged discriminator is always Edgeband.
    /// </summary>
    /// <example>Edgeband</example>
    public override DecorMatchMaterialType MaterialType => DecorMatchMaterialType.Edgeband;
}