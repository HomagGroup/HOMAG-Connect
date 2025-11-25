using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Machine for which the optimization is performed.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class OptimizationMachine
    {
        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="OptimizationType" />.
        /// </summary>
        [Required]
        public OptimizationType OptimizationType { get; set; }

        /// <summary>
        /// Gets or sets whether sending is supported. Only productionAssist Cutting / Nesting and machine connected to tapio are
        /// supported.
        /// </summary>
        public bool SupportsSending { get; set; }

        /// <summary>
        /// Gets or sets the mode used for transferring data to the machine.
        /// </summary>
        [Required]
        public ProgramTransferMode TransferMode { get; set; } = ProgramTransferMode.ManualDownload;
    }
}