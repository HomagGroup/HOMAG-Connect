using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

/// <summary>
/// Process.
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

///// <summary>
///// Process.
///// </summary>
//[ResourceManager(typeof(ProcessDisplayNames))]
//[JsonConverter(typeof(TolerantEnumConverter))]
//public enum Process
//{
//    // ReSharper disable InconsistentNaming
//    // ReSharper disable IdentifierTypo

//    /// <summary>
//    /// Hot-melt glue.
//    /// </summary>
//    [Display(Description = "Hot-melt glue")]
//    HotmeltGlue,

//    /// <summary>
//    /// Zero-joint.
//    /// </summary>
//    [Display(Description = "Zero-joint")]
//    Zerojoint,

//    /// <summary>
//    /// Other.
//    /// </summary>
//    [Display(Description = "Other")]
//    Other

//    // ReSharper restore InconsistentNaming
//    // ReSharper restore IdentifierTypo
//}