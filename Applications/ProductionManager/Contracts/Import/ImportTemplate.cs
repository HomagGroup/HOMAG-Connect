using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.ProductionManager.Contracts.Import
{
    /// <summary>
    /// Import template that can be used to import files of a given file type.
    /// </summary>
    /// <example>
    /// { "fileExtension": "csv", "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "name": "Default CSV" }
    /// </example>
    [DebuggerDisplay("{Name}")]
    public class ImportTemplate
    {
        private string _FileExtension = string.Empty;

        /// <summary>
        /// File extension (without leading dot) the template was created for. Required. Leading and trailing dots and spaces are removed.
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
        /// Unique identifier of the import template. Required.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the import template. Required.
        /// </summary>
        /// <example>Default CSV</example>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
