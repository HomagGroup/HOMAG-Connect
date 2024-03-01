using System.Collections.Generic;
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
        /// Gets or sets the list of parts to optimize.
        /// </summary>
        public List<OptimizationRequestPart> Parts { get; set; } = new List<OptimizationRequestPart>();
    }
}