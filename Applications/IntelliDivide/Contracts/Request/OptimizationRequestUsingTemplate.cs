using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents an optimization request that uses an import template to map a structured file
    /// (e.g. Excel, CSV, PNX) to optimization parts. The file is submitted alongside this request.
    /// Machine, parameters, boards, and action are inherited from <see cref="OptimizationRequestBase" />.
    /// </summary>
    /// <example>
    /// {
    ///   "name": "Order_20250120",
    ///   "importTemplate": "Excel",
    ///   "machine": "productionAssist Cutting",
    ///   "parameters": "Default",
    ///   "action": "Optimize"
    /// }
    /// </example>
    [DebuggerDisplay("Name={Name}, Template={ImportTemplate}, {Action={Action}")]
    public class OptimizationRequestUsingTemplate : OptimizationRequestBase
    {
        /// <summary>
        /// Gets or sets the name of the import template configured in intelliDivide.
        /// The template defines how the submitted file is mapped to optimization parts.
        /// </summary>
        /// <example>Excel</example>
        [Required]
        public string ImportTemplate { get; set; }

        /// <summary>
        /// Gets or sets the name of the optimization. Optional — when not provided, the imported file name is used.
        /// Maximum length is 100 characters.
        /// </summary>
        /// <example>Order_20250120</example>
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
    }
}