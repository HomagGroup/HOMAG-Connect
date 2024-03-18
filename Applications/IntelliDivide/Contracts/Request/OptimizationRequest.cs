using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Optimization request class to use to create requests on object model.
    /// </summary>
    [DebuggerDisplay("Name={Name}, Action={Action}")]
    public class OptimizationRequest : OptimizationRequestBase
    {
        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of parts to optimize.
        /// </summary>
        [MinLength(1)]
        public List<OptimizationRequestPart> Parts { get; set; } = new List<OptimizationRequestPart>();
    }
}