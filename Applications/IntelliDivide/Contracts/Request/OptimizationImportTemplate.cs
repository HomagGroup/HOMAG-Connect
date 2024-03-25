using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Optimization import template.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class OptimizationImportTemplate
    {
        private string _FileExtension = string.Empty;

        /// <summary>
        /// Gets or sets the file extension (without leading dot) for which the template was created.
        /// </summary>
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
        /// Ges or sets the name of the import template.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="OptimizationType" /> for which the template is valid..
        /// </summary>
        [Required]
        public OptimizationType OptimizationType { get; set; }
    }
}