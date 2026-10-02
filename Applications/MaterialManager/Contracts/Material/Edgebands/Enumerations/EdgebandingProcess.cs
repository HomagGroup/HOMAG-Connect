using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

/// <summary>
/// The edgebanding process identifies how an edge band is joined to a board, for example with hot-melt glue or a zero-joint process.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
/// </summary>
[ResourceManager(typeof(EdgebandingProcessDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum EdgebandingProcess
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable IdentifierTypo

    /// <summary>
    /// Hot-melt glue.
    /// </summary>
    [Display(Description = "Hot-melt glue")]
    HotmeltGlue,

    /// <summary>
    /// Zero-joint.
    /// </summary>
    [Display(Description = "Zero-joint")]
    Zerojoint,

    /// <summary>
    /// Other.
    /// </summary>
    [Display(Description = "Other")]
    Other

    // ReSharper restore InconsistentNaming
    // ReSharper restore IdentifierTypo
}