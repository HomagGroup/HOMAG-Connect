using System.Collections.ObjectModel;
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
        private const int _NameMaxLength = 100;

        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        [Required]
        [StringLength(_NameMaxLength, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of parts to optimize.
        /// </summary>
        [MinLength(1)]
        public Collection<OptimizationRequestPart> Parts { get; set; } = new Collection<OptimizationRequestPart>();
    }
}