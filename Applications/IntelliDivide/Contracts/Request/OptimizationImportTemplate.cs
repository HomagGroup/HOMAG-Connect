using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    [DebuggerDisplay("{Name}")]
    public class OptimizationImportTemplate
    {
        private string _FileExtension = string.Empty;

        /// <summary>
        /// Gets or sets the file extension (without leading dot)
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

        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public OptimizationType OptimizationType { get; set; }
    }
}