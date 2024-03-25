using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Optimization parameter set.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class OptimizationParameter
    {
        /// <summary>
        /// Gets or sets the name of the parameter set.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="OptimizationType"/>.
        /// </summary>
        [Required]
        public OptimizationType OptimizationType { get; set; }
    }
}