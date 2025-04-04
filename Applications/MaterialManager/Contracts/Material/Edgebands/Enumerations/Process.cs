using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

/// <summary>
/// Process.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[Obsolete("Use EdgebandingProcess instead", true)]
public enum Process
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