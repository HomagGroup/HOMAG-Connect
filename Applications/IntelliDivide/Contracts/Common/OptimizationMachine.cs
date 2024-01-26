using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    [DebuggerDisplay("{Name}")]
    public class OptimizationMachine
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public OptimizationType OptimizationType { get; set; }

        public bool SupportsSending { get; set; }
    }
}