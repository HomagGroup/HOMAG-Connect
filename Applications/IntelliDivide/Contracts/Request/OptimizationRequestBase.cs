using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Optimization request base class.
    /// </summary>
    [DebuggerDisplay("Action={Action}")]
    public class OptimizationRequestBase : IExtensibleDataObject
    {
        private const int _MachineNameMaxLength = 100;
        private const int _ParameterNameMaxLength = 100;

        /// <summary>
        /// Gets or sets the <see cref="OptimizationRequestAction" />.
        /// </summary>
        [Required]
        [DefaultValue(OptimizationRequestAction.ImportOnly)]
        public OptimizationRequestAction Action { get; set; } = OptimizationRequestAction.ImportOnly;

        /// <summary>
        /// Optional. If no boards are provided the required boards are retrieved from materialManager.
        /// </summary>
        public Collection<OptimizationRequestBoard> Boards { get; set; } = new Collection<OptimizationRequestBoard>();

        /// <summary>
        /// Optional. If no machine is provided the first cutting machine sorted by name is used.
        /// </summary>
        [StringLength(_MachineNameMaxLength, MinimumLength = 3)]
        public string Machine { get; set; } = string.Empty;

        /// <summary>
        /// Optional. If no parameter is provided the first parameter, sorted by name which fits the machine type is used.
        /// </summary>
        [StringLength(_ParameterNameMaxLength, MinimumLength = 3)]
        public string Parameters { get; set; } = string.Empty;

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}