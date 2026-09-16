using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

/// <summary>
/// Discriminates the shape of the <see cref="DecorMatchSearchRequest.Material" /> payload.
/// </summary>
[ResourceManager(typeof(DecorMatchMaterialTypeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum DecorMatchMaterialType
{
    /// <summary>
    /// The material is a board type.
    /// </summary>
    [Display(Description = "Board")]
    Board,

    /// <summary>
    /// The material is an edgeband type.
    /// </summary>
    [Display(Description = "Edgeband")]
    Edgeband
}
