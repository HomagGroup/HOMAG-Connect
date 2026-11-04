using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Selects how the height of boards cut together as a book is determined.
/// </summary>
[ResourceManager(typeof(BookHeightModeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum BookHeightMode
{
    /// <summary>
    /// Legacy projection-deduction strategy; use <see cref="MaximumBookHeightOfMachine" /> instead.
    /// </summary>
    [Obsolete("This mode is obsolete and was replaced with MaximumBookHeightOfMachine.")]
    MaximumSawBladeProjectionDeduction,

    /// <summary>
    /// Use the maximum book height supported by the machine.
    /// </summary>
    [Display(Description = "Maximum Book Height Of Machine")]
    MaximumBookHeightOfMachine,

    /// <summary>
    /// Cut one board at a time instead of a book of boards.
    /// </summary>
    [Display(Description = "Single Board")]
    SingleBoard,

    /// <summary>
    /// Legacy specific-height strategy; use <see cref="LimitedBookHeight" /> instead.
    /// </summary>
    [Obsolete("This mode is obsolete and was replaced with LimitedBookHeight.")]
    SpecificValue,

    /// <summary>
    /// Apply the material-specific limit provided in <see cref="BookHeightParameters.LimitedBookHeight" />.
    /// </summary>
    [Display(Description = "Limited Book Height")]
    LimitedBookHeight
}