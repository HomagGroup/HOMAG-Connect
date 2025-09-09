using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// Import template.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class ImportTemplate
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
        /// Ges or sets the TemplateId of the import template.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Ges or sets the name of the import template.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}