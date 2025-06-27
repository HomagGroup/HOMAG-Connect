using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Defines the grain of a part, board or offcut.
/// </summary>
[ResourceManager(typeof(GrainDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum Grain
{
    /// <summary>
    /// No grain.
    /// </summary>
    [Display(Description = "None")] 
    None = 0,

    /// <summary>
    /// Grain along the length.
    /// </summary>
    [Display(Description = "Lengthwise")] 
    Lengthwise = 1,

    /// <summary>
    /// Grain along the width.
    /// </summary>
    [Display(Description = "Crosswise")] 
    Crosswise = 2
}