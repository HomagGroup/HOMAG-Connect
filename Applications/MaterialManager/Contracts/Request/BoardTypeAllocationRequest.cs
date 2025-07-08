using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Request
{
    /// <summary>
    /// Request object to create an Allocation in materialManager.
    /// </summary>
    public class BoardTypeAllocationRequest
    {
        /// <summary>
        /// Gets or sets the allocation name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the allocation created by.
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        ///  Gets or sets source of the allocation.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        [Required]
        public string Workstation { get; set; }

        /// <summary>
        /// Gets or sets the boardType code.
        /// </summary>
        [Required]
        public string BoardTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
