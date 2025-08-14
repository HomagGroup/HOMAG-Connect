using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialAssist.Contracts.Request
{
    public class MaterialAssistRequestBase
    {
        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        [StringLength(300)]
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the custom id.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}