using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Optimization request base class.
    /// </summary>
    [DebuggerDisplay("Action={Action}")]
    public class OptimizationRequestBase: IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the <see cref="OptimizationRequestAction" />.
        /// </summary>
        [Required]
        [DefaultValue(OptimizationRequestAction.ImportOnly)]
        public OptimizationRequestAction Action { get; set; } = OptimizationRequestAction.ImportOnly;

        /// <summary>
        /// Optional. If no machine is provided the first cutting machine sorted by name is used.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Machine { get; set; } = string.Empty;

        /// <summary>
        /// Optional. If no name is provided it gets automatically generated.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional. If no parameter is provided the first parameter, sorted by name which fits the machine type is used.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        /// Optional. If no boards are provided the required boards are retrieved from materialManager.
        /// </summary>
        public List<OptimizationRequestBoard> Boards { get; set; } = new List<OptimizationRequestBoard>();

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}