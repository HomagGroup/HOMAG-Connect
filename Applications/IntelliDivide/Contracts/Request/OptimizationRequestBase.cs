using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request;

/// <summary>
/// Base class for optimization requests. Provides shared properties for specifying the target machine,
/// parameter set, available boards, and the action to perform after submission.
/// </summary>
[DebuggerDisplay("Action={Action}")]
public class OptimizationRequestBase : IExtensibleDataObject
{
    private const int _MachineNameMaxLength = 100;
    private const int _ParameterNameMaxLength = 100;

    /// <summary>
    /// Gets or sets the action to perform after the request is submitted.
    /// Defaults to <c>ImportOnly</c>, which creates the optimization without starting it.
    /// </summary>
    /// <example>Optimize</example>
    [Required]
    [DefaultValue(OptimizationRequestAction.ImportOnly)]
    public OptimizationRequestAction Action { get; set; } = OptimizationRequestAction.ImportOnly;

    /// <summary>
    /// Gets or sets the boards available for the optimization. Optional — when no boards are provided,
    /// the required boards are retrieved automatically from materialManager.
    /// </summary>
    /// <example>
    /// [
    ///   { "materialCode": "P2_White_19.0", "boardCode": "P2_White_19.0_2800_2070", "length": 2800, "width": 2070, "thickness": 19.0, "grain": "None", "costs": 10, "quantity": 70 }
    /// ]
    /// </example>
    public Collection<OptimizationRequestBoard> Boards { get; set; } = [];

    /// <summary>
    /// Gets or sets the name of the target machine. Maximum length is 100 characters.
    /// </summary>
    /// <example>productionAssist Cutting</example>
    [StringLength(_MachineNameMaxLength, MinimumLength = 3)]
    [Required]
    public string Machine { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the parameter set to use. Maximum length is 100 characters.
    /// </summary>
    /// <example>Default</example>
    [StringLength(_ParameterNameMaxLength, MinimumLength = 3)]
    [Required]
    public string Parameters { get; set; } = string.Empty;

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public ExtensionDataObject ExtensionData { get; set; }
}