using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Optimization request class to use to create requests using a import template and file.
    /// </summary>
    [DebuggerDisplay("ImportTemplate={ImportTemplate}, {Action={Action}")]
    public class OptimizationRequestUsingTemplate : OptimizationRequestBase
    {
        /// <summary>
        /// Optional. Gets or sets the name of the import template.
        /// </summary>
        [Required]
        public string ImportTemplate { get; set; }
    }
}