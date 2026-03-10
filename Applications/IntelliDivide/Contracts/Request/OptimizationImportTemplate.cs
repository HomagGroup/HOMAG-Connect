using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents an import template configured in intelliDivide. Import templates define how structured files
    /// (e.g. CSV, Excel, PNX) are mapped to optimization parts during a template-based optimization request.
    /// </summary>
    /// <example>
    /// { "name": "HOMAG Connect", "fileExtension": "csv", "optimizationType": "Cutting" }
    /// </example>
    [DebuggerDisplay("{Name}")]
    public class OptimizationImportTemplate
    {
        private string _FileExtension = string.Empty;

        /// <summary>
        /// Gets or sets the file extension (without leading dot) for which the template was created.
        /// Leading dots and whitespace are trimmed automatically.
        /// </summary>
        /// <example>csv</example>
        [Required]
        public string FileExtension
        {
            get
            {
                return _FileExtension;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _FileExtension = value.Trim('.', ' ');
                }
                else
                {
                    _FileExtension = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the import template as configured in intelliDivide.
        /// </summary>
        /// <example>HOMAG Connect</example>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="OptimizationType" /> this template applies to.
        /// Supported values: <c>Cutting</c>, <c>Nesting</c>.
        /// </summary>
        /// <example>Cutting</example>
        [Required]
        public OptimizationType OptimizationType { get; set; }
    }
}